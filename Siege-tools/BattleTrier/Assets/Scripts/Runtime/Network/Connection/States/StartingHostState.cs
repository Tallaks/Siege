using System;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Data;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Session;
using Unity.Netcode;
using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Runtime.Network.Connection.States
{
  public class StartingHostState : ParameterConnectionState<string>, IApprovalCheck, IOnlineState, IClientDisconnect
  {
    private readonly IConnectionService _connectionService;
    private readonly IConnectionStateMachine _connectionStateMachine;
    private readonly LobbyInfo _lobbyInfo;
    private readonly NetworkManager _networkManager;
    private readonly Session<SessionPlayerData> _session;

    public StartingHostState(
      IConnectionStateMachine connectionStateMachine,
      IConnectionService connectionService,
      NetworkManager networkManager,
      Session<SessionPlayerData> session,
      LobbyInfo lobbyInfo)
    {
      _connectionService = connectionService;
      _connectionStateMachine = connectionStateMachine;
      _lobbyInfo = lobbyInfo;
      _session = session;
      _networkManager = networkManager;
    }

    public void ApprovalCheck(NetworkManager.ConnectionApprovalRequest request,
      NetworkManager.ConnectionApprovalResponse response)
    {
      Debug.Log("Starting hosting state approval check");
      byte[] connectionData = request.Payload;
      ulong clientId = request.ClientNetworkId;
      if (clientId == _networkManager.LocalClientId)
      {
        string payload = System.Text.Encoding.UTF8.GetString(connectionData);
        var connectionPayload =
          JsonUtility.FromJson<ConnectionPayload>(
            payload); // https://docs.unity3d.com/2020.2/Documentation/Manual/JSONSerialization.html

        _session.SetupConnectingPlayerSessionData(clientId, connectionPayload.PlayerId,
          new SessionPlayerData(clientId, connectionPayload.PlayerName, new NetworkGuid(), 0, true));

        response.Approved = true;
        response.CreatePlayerObject = true;
      }
    }

    public void ReactToClientDisconnect(ulong clientId)
    {
      if (_networkManager.LocalClientId == clientId)
        _connectionStateMachine.Enter<OfflineState>();
    }

    public void OnUserRequestedShutdown() =>
      _connectionStateMachine.Enter<OfflineState>();

    public void OnTransportFailure() =>
      _connectionStateMachine.Enter<OfflineState>();

    public override async void Enter(string userName)
    {
      try
      {
        await _connectionService.SetupHostConnectionAsync(userName);
        Debug.Log($"Created relay allocation with join code {_lobbyInfo.RelayJoinCode}");
        Debug.Log(_networkManager);
        // NGO's StartHost launches everything
        if (!_networkManager.StartHost())
          OnClientDisconnect(_networkManager.LocalClientId);
      }
      catch (Exception e)
      {
        Debug.LogError(e);
        StartHostFailed();
        throw;
      }
    }

    public override void Exit()
    {
    }

    private void OnClientDisconnect(ulong clientId)
    {
      if (clientId == _networkManager.LocalClientId)
        StartHostFailed();
    }

    private void StartHostFailed()
    {
      Debug.LogError("Start Hosting failed!!");
      _connectionStateMachine.Enter<OfflineState>();
    }
  }
}