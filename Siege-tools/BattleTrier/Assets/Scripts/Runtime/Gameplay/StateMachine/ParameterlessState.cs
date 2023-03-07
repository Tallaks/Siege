namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.StateMachine
{
  public abstract class ParameterlessState : IExitState
  {
    public abstract void Enter();
    public abstract void Exit();
  }
}