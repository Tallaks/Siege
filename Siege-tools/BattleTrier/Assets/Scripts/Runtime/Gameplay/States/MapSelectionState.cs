using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.UI;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Roles;
using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.States
{
  public class MapSelectionState : ParameterlessState
  {
    private readonly GameplayMediator _mediator;
    private readonly RoleBase _role;

    public MapSelectionState(
      RoleBase role,
      GameplayMediator mediator)
    {
      _role = role;
      _mediator = mediator;
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