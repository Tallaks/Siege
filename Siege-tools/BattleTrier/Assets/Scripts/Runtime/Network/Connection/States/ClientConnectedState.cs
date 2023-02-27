using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Runtime.Network.Connection.States
{
  public class ClientConnectedState : ParameterConnectionState<ulong, ConnectionState>, IOnlineState
  {
    private IConnectionStateMachine _connectionStateMachine;

    public ClientConnectedState(IConnectionStateMachine connectionStateMachine) => 
      _connectionStateMachine = connectionStateMachine;

    public override void Enter<T1, T2>(T1 clientId, T2 previousState)
    {
      if (previousState is HostingState)
        _connectionStateMachine.Enter<HostingState, bool>(false);
      
      Debug.Log($"Client with id {clientId} connected");
    }

    public void OnTransportFailure() => 
      _connectionStateMachine.Enter<OfflineState>();

    public override void Exit()
    {
    }
  }
}