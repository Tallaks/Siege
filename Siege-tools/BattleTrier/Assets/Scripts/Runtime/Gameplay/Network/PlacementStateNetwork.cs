using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.States;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Roles;
using Unity.Netcode;
using UnityEngine;
using Zenject;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Network
{
  public class PlacementStateNetwork : NetworkBehaviour
  {
    private StateMachine _stateMachine;

    [Inject]
    private void Construct(StateMachine stateMachine) =>
      _stateMachine = stateMachine;

    [ClientRpc]
    public void ChangeActivePlayerFromClientRpc(RoleState role)
    {
      Debug.Log("ChangeActivePlayerFromClientRpc");
      if (role == RoleState.ChosenFirst)
        _stateMachine.Enter<PlacingSecondPlayerCharactersState>();
      else if (role == RoleState.ChosenSecond)
        _stateMachine.Enter<BattleInitializationState>();
    }

    [ServerRpc(RequireOwnership = false)]
    public void ChangeActivePlayerFromServerRpc(RoleState role)
    {
      Debug.Log("ChangeActivePlayerFromServerRpc");
      ChangeActivePlayerFromClientRpc(role);
    }
  }
}