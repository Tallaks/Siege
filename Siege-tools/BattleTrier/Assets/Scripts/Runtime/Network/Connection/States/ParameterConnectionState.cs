namespace Kulinaria.Tools.BattleTrier.Runtime.Network.Connection.States
{
  public abstract class ParameterConnectionState<TSource> : ConnectionState
  {
    public abstract void Enter(TSource param);
  }

  public abstract class ParameterConnectionState<T1, T2> : ConnectionState
  {
    public abstract void Enter<T1, T2>(T1 param1, T2 param2);
  }
}