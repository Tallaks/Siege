namespace Kulinaria.Tools.BattleTrier.Runtime.Network.Connection.States
{
  public abstract class ParameterConnectionState<T> : ConnectionState
  {
    public abstract void Enter<T>(T param);
  }
}