using Kulinaria.Tools.BattleTrier.Runtime.Network.Data;
using Unity.Netcode;
using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Runtime.Network.Connection.States
{
  public class ClientConnectedState : ParameterConnectionState<ulong, ConnectionState>, IOnlineState, IClientDisconnect
  {
    private readonly IConnectionStateMachine _connectionStateMachine;
    private readonly NetworkManager _networkManager;
    private UserProfile _localUser;

    public ClientConnectedState(
      IConnectionStateMachine connectionStateMachine,
      NetworkManager networkManager,
      UserProfile localUser)
    {
      _connectionStateMachine = connectionStateMachine;
      _networkManager = networkManager;
      _localUser = localUser;
    }

    public override void Enter<T1, T2>(T1 clientId, T2 previousState)
    {
      if (previousState is HostingState)
        _connectionStateMachine.Enter<HostingState, bool>(false);
      
      Debug.Log($"Client with id {clientId} connected");
    }

    public void OnTransportFailure() => 
      _connectionStateMachine.Enter<OfflineState>();

    public void OnUserRequestedShutdown() =>
      _connectionStateMachine.Enter<OfflineState>();

    public void ReactToClientDisconnect(ulong clientId)
    {
      var disconnectReason = _networkManager.DisconnectReason;
      if (string.IsNullOrEmpty(disconnectReason))
        _connectionStateMachine.Enter<ClientReconnectingState, string>(_localUser.Name);
      else
        _connectionStateMachine.Enter<OfflineState>();
    }

    public override void Exit()
    {
    }
  }
}