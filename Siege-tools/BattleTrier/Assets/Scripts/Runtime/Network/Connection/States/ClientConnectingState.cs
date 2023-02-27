namespace Kulinaria.Tools.BattleTrier.Runtime.Network.Connection.States
{
  public class ClientConnectingState : ParameterConnectionState<string>, IOnlineState
  {
    private readonly IConnectionStateMachine _connectionStateMachine;
    private readonly IConnectionService _connectionService;

    public ClientConnectingState(
      IConnectionStateMachine connectionStateMachine,
      IConnectionService connectionService)
    {
      _connectionStateMachine = connectionStateMachine;
      _connectionService = connectionService;
    }

    public override void Enter<T>(T displayName) => 
      _connectionService.ConnectClientAsync(displayName.ToString());

    public override void Exit()
    {
    }

    public void OnTransportFailure() =>
      _connectionStateMachine.Enter<OfflineState>();
  }
}