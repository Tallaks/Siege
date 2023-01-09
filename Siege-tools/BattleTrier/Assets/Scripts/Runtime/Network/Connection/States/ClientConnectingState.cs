namespace Kulinaria.Tools.BattleTrier.Runtime.Network.Connection.States
{
  public class ClientConnectingState : NonParameterConnectionState, IOnlineState
  {
    private readonly IConnectionStateMachine _connectionStateMachine;

    public ClientConnectingState(IConnectionStateMachine connectionStateMachine) => 
      _connectionStateMachine = connectionStateMachine;

    public override void Enter()
    {
      
    }

    public void OnTransportFailure() => 
      _connectionStateMachine.Enter<OfflineState>();

    public override void Exit()
    {
      
    }
  }
}