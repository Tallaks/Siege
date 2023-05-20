namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.States
{
  public abstract class ParameterlessState : IExitState
  {
    public abstract void Exit();
    public abstract void Enter();
  }
}