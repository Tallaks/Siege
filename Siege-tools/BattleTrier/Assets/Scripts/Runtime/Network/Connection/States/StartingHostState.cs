using System;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Data;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Lobbies;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Networking.Transport.Relay;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using UnityEngine;
using Task = System.Threading.Tasks.Task;

namespace Kulinaria.Tools.BattleTrier.Runtime.Network.Connection.States
{
  public class StartingHostState : ParameterConnectionState<string>
  {
    private NetworkManager _networkManager;
    private LobbyInfo _lobbyInfo;
    private LobbyServiceFacade _lobbyService;
    private IConnectionService _connectionService;

    public StartingHostState(IConnectionService connectionService, NetworkManager networkManager, LobbyInfo lobbyInfo, LobbyServiceFacade lobbyService)
    {
      _connectionService = connectionService;
      _lobbyService = lobbyService;
      _lobbyInfo = lobbyInfo;
      _networkManager = networkManager;
    }

    public override async void Enter<T>(T userName)
    {
      try
      {
        await SetupHostConnectionAsync(userName.ToString());
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
      _connectionService.Enter<OfflineState>();
    }

    private async Task SetupHostConnectionAsync(string userName)
    {
      Debug.Log("Setting up Unity Relay host");

      SetConnectionPayload(GetPlayerId(), userName); // Need to set connection payload for host as well, as host is a client too

      // Create relay allocation
      Allocation hostAllocation = await RelayService.Instance.CreateAllocationAsync(8, region: null);
      var joinCode = await RelayService.Instance.GetJoinCodeAsync(hostAllocation.AllocationId);

      Debug.Log($"server: connection data: {hostAllocation.ConnectionData[0]} {hostAllocation.ConnectionData[1]}, " +
                $"allocation ID:{hostAllocation.AllocationId}, region:{hostAllocation.Region}");

      _lobbyInfo.RelayJoinCode = joinCode;

      //next line enable lobby and relay services integration
      await _lobbyService.UpdateLobbyDataAsync(_lobbyInfo.GetDataForUnityServices());
      await _lobbyService.UpdatePlayerRelayInfoAsync(hostAllocation.AllocationIdBytes.ToString(), joinCode);

      // Setup UTP with relay connection info
      var utp = (UnityTransport)_networkManager.NetworkConfig.NetworkTransport;
      utp.SetRelayServerData(new RelayServerData(hostAllocation, "dtls")); // This is with DTLS enabled for a secure connection
    }

    private void SetConnectionPayload(string playerId, string userName)
    {
      string payload = JsonUtility.ToJson(new ConnectionPayload()
      {
        PlayerId = playerId,
        PlayerName = userName,
        IsDebug = Debug.isDebugBuild
      });

      byte[] payloadBytes = System.Text.Encoding.UTF8.GetBytes(payload);

      _networkManager.NetworkConfig.ConnectionData = payloadBytes;
    }

    private string GetPlayerId()
    {
      if (UnityServices.State != ServicesInitializationState.Initialized)
        return Guid.NewGuid().ToString();

      return AuthenticationService.Instance.PlayerId;
    }
  }
}