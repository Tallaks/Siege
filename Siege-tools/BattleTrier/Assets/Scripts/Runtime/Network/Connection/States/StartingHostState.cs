using System;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Data;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Session;
using Unity.Netcode;
using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Runtime.Network.Connection.States
{
  public class StartingHostState : ParameterConnectionState<string>, IApprovalCheck, IOnlineState
  {
    private NetworkManager _networkManager;
    private LobbyInfo _lobbyInfo;
    private IConnectionStateMachine _connectionStateMachine;
    private IConnectionService _connectionService;

    public StartingHostState(IConnectionStateMachine connectionStateMachine, IConnectionService connectionService, NetworkManager networkManager, LobbyInfo lobbyInfo)
    {
      _connectionService = connectionService;
      _connectionStateMachine = connectionStateMachine;
      _lobbyInfo = lobbyInfo;
      _networkManager = networkManager;
    }

    public override async void Enter<T>(T userName)
    {
      try
      {
        await _connectionService.SetupHostConnectionAsync(userName.ToString());
        Debug.Log($"Created relay allocation with join code {_lobbyInfo.RelayJoinCode}");

        // NGO's StartHost launches everything
        if (!_networkManager.StartHost())
          OnClientDisconnect(_networkManager.LocalClientId);
      }
      catch (Exception)
      {
        StartHostFailed();
        throw;
      }
    }

    public override void ReactToClientDisconnect(ulong clientId)
    {
      if (_networkManager.LocalClientId == clientId)
        _connectionStateMachine.Enter<OfflineState>();
    }

    public void ApprovalCheck(NetworkManager.ConnectionApprovalRequest request, NetworkManager.ConnectionApprovalResponse response)
    {
      byte[] connectionData = request.Payload;
      ulong clientId = request.ClientNetworkId;
      // This happens when starting as a host, before the end of the StartHost call. In that case, we simply approve ourselves.
      if (clientId == _networkManager.LocalClientId)
      {
        var payload = System.Text.Encoding.UTF8.GetString(connectionData);
        var connectionPayload = JsonUtility.FromJson<ConnectionPayload>(payload); // https://docs.unity3d.com/2020.2/Documentation/Manual/JSONSerialization.html

        SessionService<SessionPlayerData>.Instance.SetupConnectingPlayerSessionData(clientId, connectionPayload.PlayerId,
          new SessionPlayerData(clientId, connectionPayload.PlayerName, new NetworkGuid(), 0, true));

        // connection approval will create a player object for you
        response.Approved = true;
        response.CreatePlayerObject = true;
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

    public void OnTransportFailure() => 
      _connectionStateMachine.Enter<OfflineState>();
  }
}