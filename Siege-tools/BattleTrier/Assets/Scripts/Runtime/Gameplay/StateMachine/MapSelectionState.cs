using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.UI;
using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.StateMachine
{
  public class MapSelectionState : ParameterlessState
  {
    private GameplayMediator _mediator;

    public MapSelectionState(GameplayMediator mediator) => 
      _mediator = mediator;

    public override void Enter()
    {
      Debug.Log("Entering Map Selection State");
      _mediator.InitializeMapSelectionUi();
    }

    public override void Exit()
    {
    }
  }
}