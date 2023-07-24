using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.States;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Roles.UI;
using Unity.Netcode;
using UnityEngine;
using Zenject;

namespace Kulinaria.Tools.BattleTrier.Runtime.Network.Roles
{
  public class RoleSelectionService : NetworkBehaviour
  {
    public NetworkVariable<bool> LobbyIsClosed { get; } = new();
    private RoleMediator _mediator;

    private NetworkManager _networkManager;
    private StateMachine _stateMachine;
    public NetworkList<PlayerRoleState> PlayerRoles;

    [Inject]
    private void Construct(NetworkManager networkManager, StateMachine stateMachine, RoleMediator mediator)
    {
      _networkManager = networkManager;
      _stateMachine = stateMachine;
      _mediator = mediator;
    }

    private void Awake() =>
      PlayerRoles = new NetworkList<PlayerRoleState>();

    [ServerRpc(RequireOwnership = false)]
    public void ChangeSeatServerRpc(ulong clientId, int roleButtonId)
    {
      Debug.Log("ChangeSeatServerRpc");
      for (var i = 0; i < PlayerRoles.Count; i++)
        if (PlayerRoles[i].ClientId == clientId &&
            PlayerRoles[i].State != (RoleState)roleButtonId)
        {
          PlayerRoles[i] = new PlayerRoleState(
            clientId,
            (RoleState)roleButtonId,
            Time.time
          );

          if (PlayerRoles[i].ClientId != clientId && PlayerRoles[i].State == (RoleState)roleButtonId)
            PlayerRoles[i] = new PlayerRoleState(
              clientId,
              (RoleState)roleButtonId,
              Time.time
            );
        }

      UpdateLobby();
    }

    [ClientRpc]
    private void CloseLobbyClientRpc()
    {
      Debug.Log("CloseLobbyClientRpc");
      _stateMachine.Enter<MapSelectionState>();
    }

    [ClientRpc]
    private void UpdateLobbyClientRpc()
    {
      Debug.Log("UpdateLobbyClientRpc");
      _mediator.UpdateLobbyUi();
    }

    private void UpdateLobby()
    {
      var first = false;
      var second = false;
      foreach (PlayerRoleState playerRole in PlayerRoles)
      {
        if (playerRole.State == RoleState.ChosenFirst)
          first = true;

        if (playerRole.State == RoleState.ChosenSecond)
          second = true;
      }

      SavePlayerData();
      UpdateLobbyClientRpc();
      if (first && second)
        CloseLobbyClientRpc();
    }

    private void SavePlayerData()
    {
      foreach (NetworkClient client in _networkManager.ConnectedClientsList)
      {
        var player = client.PlayerObject.GetComponent<NetworkPlayerObject>();
        for (var index = 0; index < PlayerRoles.Count; index++)
        {
          PlayerRoleState playerRole = PlayerRoles[index];
          if (playerRole.ClientId == client.ClientId)
          {
            player.State.Value = playerRole.State;
            break;
          }
        }
      }
    }
  }
}