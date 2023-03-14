using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.UI;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Gameplay;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Roles;
using Unity.Netcode;
using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.StateMachine
{
  public class MapSelectionState : ParameterlessState
  {
    private readonly StateMachine _stateMachine;
    private readonly NetworkManager _networkManager;
    private readonly GameplayMediator _mediator;
    private readonly MapSelectionBehaviour _mapSelectionBehaviour;

    public MapSelectionState(
      StateMachine stateMachine,
      NetworkManager networkManager,
      GameplayMediator mediator,
      MapSelectionBehaviour mapSelectionBehaviour)
    {
      _stateMachine = stateMachine;
      _networkManager = networkManager;
      _mediator = mediator;
      _mapSelectionBehaviour = mapSelectionBehaviour;
    }

    public override void Enter()
    {
      Debug.Log("Entering Map Selection State");
      _mediator.InitializeMapSelectionUi(_networkManager.LocalClient.PlayerObject.
        GetComponent<RoleBase>().State.Value, _mapSelectionBehaviour);
    }

    public override void Exit() => 
      _mediator.HideMapSelectionUi();
  }
}