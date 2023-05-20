using Kulinaria.Tools.BattleTrier.Runtime.Network.Roles.UI;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Utilities;
using Unity.Netcode;
using UnityEngine;
using Zenject;

namespace Kulinaria.Tools.BattleTrier.Runtime.Network.Roles
{
  public class RoleSelectionClient : MonoBehaviour
  {
    [SerializeField] private NetCodeHook _hook;
    private readonly int _lastRoleSelected = -1;
    private bool _hasLocalPlayerLockedIn = false;
    private RoleMediator _mediator;

    private NetworkManager _networkManager;
    private RoleSelectionService _roleSelectionService;

    [Inject]
    private void Construct(
      NetworkManager networkManager,
      RoleSelectionService roleSelectionService,
      RoleMediator mediator)
    {
      _networkManager = networkManager;
      _roleSelectionService = roleSelectionService;
      _mediator = mediator;
    }

    private void Awake()
    {
      if (_networkManager.IsClient)
      {
        Debug.Log("Role Selection Client Awake", _mediator);
        _hook.OnNetworkSpawnHook += OnNetworkSpawn;
        _hook.OnNetworkDeSpawnHook += OnNetworkDeSpawn;
      }

      _mediator.ConfigureUIForLobbyMode(RoleUiMode.ChooseSeat);
    }

    private void OnDestroy()
    {
      _hook.OnNetworkSpawnHook -= OnNetworkSpawn;
      _hook.OnNetworkDeSpawnHook -= OnNetworkDeSpawn;
    }

    public void OnPlayerChosenRole(RoleState buttonIndex)
    {
      if (_roleSelectionService.IsSpawned)
        _roleSelectionService.ChangeSeatServerRpc(_networkManager.LocalClientId, (int)buttonIndex);
    }

    private void OnNetworkSpawn()
    {
      Debug.Log("Network spawn: client");
      _roleSelectionService.LobbyIsClosed.OnValueChanged += OnLobbyClosedChanged;
      _roleSelectionService.PlayerRoles.OnListChanged += OnLobbyPlayerStateChanged;
    }

    private void OnNetworkDeSpawn()
    {
      Debug.Log("Network despawn: client");
      _roleSelectionService.LobbyIsClosed.OnValueChanged -= OnLobbyClosedChanged;
      _roleSelectionService.PlayerRoles.OnListChanged -= OnLobbyPlayerStateChanged;
    }

    private void OnLobbyPlayerStateChanged(NetworkListEvent<PlayerRoleState> changeEvent)
    {
      Debug.Log("Player states list changed");
      UpdatePlayerCount();

      for (var i = 0; i < _roleSelectionService.PlayerRoles.Count; i++)
        Debug.Log(_roleSelectionService.PlayerRoles[i].ClientId + " " +
                  _roleSelectionService.PlayerRoles[i].State);
    }

    private void OnLobbyClosedChanged(bool wasLobbyClosed, bool isLobbyClosed)
    {
      if (isLobbyClosed)
        _mediator.ConfigureUIForLobbyMode(RoleUiMode.LobbyEnding);
      else
        _mediator.ConfigureUIForLobbyMode(_lastRoleSelected == -1 ? RoleUiMode.ChooseSeat : RoleUiMode.SeatChosen);
    }

    private void UpdatePlayerCount()
    {
      int count = _roleSelectionService.PlayerRoles.Count;
      _mediator.UpdatePlayerCount(count);
    }
  }
}