using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.StateMachine
{
  public class CharacterSelectionState : ParameterlessState
  {
    public override void Enter() => 
      Debug.Log("Entering character selection state");

    public override void Exit()
    {
    }
  }
}