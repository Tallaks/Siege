namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.States
{
  public abstract class ParameterlessState : IExitState
  {
    public abstract void Enter();
    public abstract void Exit();
  }
}