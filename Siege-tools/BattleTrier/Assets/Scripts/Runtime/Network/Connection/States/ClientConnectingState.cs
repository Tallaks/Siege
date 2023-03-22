using Kulinaria.Tools.BattleTrier.Runtime.Network.Data;
using Unity.Netcode;
using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Runtime.Network.Connection.States
{
  public class ClientConnectingState : ParameterConnectionState<string>, IOnlineState, IClientDisconnect
  {
    private readonly IConnectionStateMachine _connectionStateMachine;
    private readonly IConnectionService _connectionService;
    private readonly NetworkManager _networkManager;

    public ClientConnectingState(
      NetworkManager networkManager,
      IConnectionStateMachine connectionStateMachine,
      IConnectionService connectionService)
    {
      _networkManager = networkManager;
      _connectionStateMachine = connectionStateMachine;
      _connectionService = connectionService;
    }

    public override void Enter(string displayName) =>
      _connectionService.ConnectClientAsync(displayName);

    public void OnTransportFailure() =>
      _connectionStateMachine.Enter<OfflineState>();

    public void OnUserRequestedShutdown() =>
      _connectionStateMachine.Enter<OfflineState>();

    public void ReactToClientDisconnect(ulong clientId)
    {
      var disconnectReason = _networkManager.DisconnectReason;
      if (string.IsNullOrEmpty(disconnectReason))
        Debug.LogError($"{ConnectStatus.StartClientFailed}");
      else
      {
        var connectStatus = JsonUtility.FromJson<ConnectStatus>(disconnectReason);
        Debug.LogError(connectStatus);
      }
      _connectionStateMachine.Enter<OfflineState>();
    }

    public override void Exit()
    {
    }
  }
}