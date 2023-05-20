using System;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Connection.States;
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
using Zenject;
using Task = System.Threading.Tasks.Task;

namespace Kulinaria.Tools.BattleTrier.Runtime.Network.Connection
{
  public class RelayConnectionService : IConnectionService
  {
    private readonly LobbyInfo _currentLobby;
    private readonly LobbyServiceFacade _lobbyService;
    private readonly NetworkManager _networkManager;

    [Inject] private IConnectionStateMachine _connectionStateMachine;

    public RelayConnectionService(
      NetworkManager networkManager,
      LobbyServiceFacade lobbyService,
      LobbyInfo currentLobby)
    {
      _networkManager = networkManager;
      _currentLobby = currentLobby;
      _lobbyService = lobbyService;
    }

    public async Task SetupHostConnectionAsync(string userName)
    {
      Debug.Log("Setting up Unity Relay host");

      SetConnectionPayload(GetPlayerId(),
        userName); // Need to set connection payload for host as well, as host is a client too

      // Create relay allocation
      Allocation hostAllocation = await RelayService.Instance.CreateAllocationAsync(8);
      string joinCode = await RelayService.Instance.GetJoinCodeAsync(hostAllocation.AllocationId);

      Debug.Log($"server: connection data: {hostAllocation.ConnectionData[0]} {hostAllocation.ConnectionData[1]}, " +
                $"allocation ID:{hostAllocation.AllocationId}, region:{hostAllocation.Region}");

      _currentLobby.RelayJoinCode = joinCode;

      //next line enable lobby and relay services integration
      await _lobbyService.UpdateLobbyDataAsync(_currentLobby.GetDataForUnityServices());
      await _lobbyService.UpdatePlayerRelayInfoAsync(hostAllocation.AllocationIdBytes.ToString(), joinCode);

      // Setup UTP with relay connection info
      var utp = (UnityTransport)_networkManager.NetworkConfig.NetworkTransport;
      utp.SetRelayServerData(new RelayServerData(hostAllocation,
        "dtls")); // This is with DTLS enabled for a secure connection
    }

    public async Task ConnectClientAsync(string userName)
    {
      try
      {
        // Setup NGO with current connection method
        await SetupClientConnectionAsync(userName);

        // NGO's StartClient launches everything
        if (!_networkManager.StartClient())
          throw new Exception("NetworkManager StartClient failed");
      }
      catch (Exception e)
      {
        Debug.LogError("Error connecting client, see following exception");
        Debug.LogException(e);
        StartingClientFailedAsync();
        throw;
      }
    }

    private async Task SetupClientConnectionAsync(string playerName)
    {
      Debug.Log("Setting up Unity Relay client");

      SetConnectionPayload(GetPlayerId(), playerName);

      if (_lobbyService.CurrentLobby == null)
        throw new Exception("Trying to start relay while Lobby isn't set");

      Debug.Log($"Setting Unity Relay client with join code {_currentLobby.RelayJoinCode}");

      // Create client joining allocation from join code
      JoinAllocation joinedAllocation = await RelayService.Instance.JoinAllocationAsync(_currentLobby.RelayJoinCode);
      Debug.Log($"client: {joinedAllocation.ConnectionData[0]} {joinedAllocation.ConnectionData[1]}, " +
                $"host: {joinedAllocation.HostConnectionData[0]} {joinedAllocation.HostConnectionData[1]}, " +
                $"client: {joinedAllocation.AllocationId}");

      await _lobbyService.UpdatePlayerRelayInfoAsync(joinedAllocation.AllocationId.ToString(),
        _currentLobby.RelayJoinCode);

      // Configure UTP with allocation
      var utp = (UnityTransport)_networkManager.NetworkConfig.NetworkTransport;
      utp.SetRelayServerData(new RelayServerData(joinedAllocation, "dtls"));
    }

    private void SetConnectionPayload(string playerId, string userName)
    {
      string payload = JsonUtility.ToJson(new ConnectionPayload
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

    private void StartingClientFailedAsync()
    {
      string disconnectReason = _networkManager.DisconnectReason;
      if (string.IsNullOrEmpty(disconnectReason))
      {
        Debug.Log("Connection Failed!!!");
      }
      else
      {
        var connectStatus = JsonUtility.FromJson<ConnectStatus>(disconnectReason);
        Debug.Log(connectStatus);
      }

      _connectionStateMachine.Enter<OfflineState>();
    }
  }
}