namespace Kulinaria.Tools.BattleTrier.Runtime.Network.Connection.States
{
  public interface IOnlineState : IRequestShutdown
  {
    void OnTransportFailure();
  }
}