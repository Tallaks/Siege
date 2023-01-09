namespace Kulinaria.Tools.BattleTrier.Runtime.Network.Connection.States
{
  public interface IConnectionStateMachine
  {
    ConnectionState CurrentState { get; }
    void Enter<TState>() where TState : NonParameterConnectionState;
    void Enter<TState, TPayload>(TPayload payload) where TState : ParameterConnectionState<TPayload>;
    void Enter<TState, TPayload1, TPayLoad2>(TPayload1 payload1, TPayLoad2 payLoad2) where TState : ParameterConnectionState<TPayload1, TPayLoad2>;
    void Initialize();
  }
}