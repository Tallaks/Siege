using Kulinaria.Tools.BattleTrier.Runtime.Network.Connection.States;

namespace Kulinaria.Tools.BattleTrier.Runtime.Network.Connection
{
  public interface IConnectionService
  {
    ConnectionState CurrentState { get; }
    void Enter<TState>() where TState : NonParameterConnectionState;
    void Enter<TState, TPayload>(TPayload payload) where TState : ParameterConnectionState<TPayload>;
    void Initialize();
  }
}