using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.UI;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Roles;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.States
{
  public class PlacingCharactersState : ParameterlessState
  {
    private GameplayMediator _mediator;
    private RoleBase _roleBase;

    public PlacingCharactersState(GameplayMediator mediator, RoleBase roleBase)
    {
      _mediator = mediator;
      _roleBase = roleBase;
    }

    public override void Enter()
    {
      switch (_roleBase.State.Value)
      {
        case RoleState.ChosenFirst:
          _mediator.ShowPlacementActivePlayerUi();
          break;
        case RoleState.ChosenSecond:
          _mediator.ShowPlacementWaitingPlayerUi();
          break;
        default:
          _mediator.ShowPlacementSpectatorUi();
          break;
      }
    }

    public override void Exit()
    {
    }
  }
}