using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Maps.Selection.Network;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.UI;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Roles;
using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.States
{
  public class MapSelectionState : ParameterlessState
  {
    private readonly StateMachine _stateMachine;
    private readonly RoleBase _role;
    private readonly GameplayMediator _mediator;
    private readonly MapSelectionNetwork _mapSelectionNetwork;

    public MapSelectionState(
      StateMachine stateMachine,
      RoleBase role,
      GameplayMediator mediator,
      MapSelectionNetwork mapSelectionNetwork)
    {
      _stateMachine = stateMachine;
      _role = role;
      _mediator = mediator;
      _mapSelectionNetwork = mapSelectionNetwork;
    }

    public override void Enter()
    {
      Debug.Log("Entering Map Selection State");
      _mediator.InitializeMapSelectionUi(_role.State.Value);
    }

    public override void Exit() => 
      _mediator.HideMapSelectionUi();
  }
}