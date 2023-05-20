using System.Collections;
using System.Threading.Tasks;
using Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Services.Coroutines;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Data;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Lobbies;
using Unity.Netcode;
using Unity.Services.Lobbies.Models;
using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Runtime.Network.Connection.States
{
  public class ClientReconnectingState : ParameterConnectionState<string>, IOnlineState, IClientDisconnect
  {
    private readonly IConnectionService _connectionService;
    private readonly IConnectionStateMachine _connectionStateMachine;
    private readonly ICoroutineRunner _coroutineRunner;
    private readonly LobbyInfo _lobbyInfo;
    private readonly LobbyServiceFacade _lobbyService;
    private readonly NetworkManager _networkManager;

    private int _attempts;
    private string _lobbyCode;
    private Coroutine _reconnectCoroutine;

    private string _userName;

    public ClientReconnectingState(
      IConnectionStateMachine connectionStateMachine,
      ICoroutineRunner coroutineRunner,
      IConnectionService connectionService,
      NetworkManager networkManager,
      LobbyServiceFacade lobbyService,
      LobbyInfo lobbyInfo)
    {
      _lobbyInfo = lobbyInfo;
      _lobbyService = lobbyService;
      _connectionStateMachine = connectionStateMachine;
      _coroutineRunner = coroutineRunner;
      _connectionService = connectionService;
      _networkManager = networkManager;
    }

    public void ReactToClientDisconnect(ulong clientId)
    {
      string disconnectReason = _networkManager.DisconnectReason;

      if (_attempts < 2)
      {
        if (string.IsNullOrEmpty(disconnectReason))
        {
          _reconnectCoroutine = _coroutineRunner.StartCoroutine(ReconnectCoroutine());
        }
        else
        {
          var connectStatus = JsonUtility.FromJson<ConnectStatus>(disconnectReason);
          switch (connectStatus)
          {
            case ConnectStatus.UserRequestedDisconnect:
            case ConnectStatus.HostEndedSession:
            case ConnectStatus.ServerFull:
            case ConnectStatus.IncompatibleBuildType:
              _connectionStateMachine.Enter<OfflineState>();
              break;
            default:
              _reconnectCoroutine = _coroutineRunner.StartCoroutine(ReconnectCoroutine());
              break;
          }
        }
      }
      else
      {
        if (string.IsNullOrEmpty(disconnectReason))
        {
        }
        else
        {
          var connectStatus = JsonUtility.FromJson<ConnectStatus>(disconnectReason);
          Debug.LogWarning($"{connectStatus}");
        }

        _connectionStateMachine.Enter<OfflineState>();
      }
    }

    public void OnTransportFailure() =>
      _connectionStateMachine.Enter<OfflineState>();

    public void OnUserRequestedShutdown() =>
      _connectionStateMachine.Enter<OfflineState>();

    public override void Enter(string userName)
    {
      _attempts = 0;
      _lobbyCode = _lobbyService.CurrentLobby != null ? _lobbyService.CurrentLobby.LobbyCode : "";
      _reconnectCoroutine = _coroutineRunner.StartCoroutine(ReconnectCoroutine());
      _userName = userName;
    }

    public override void Exit()
    {
      if (_reconnectCoroutine != null)
      {
        _coroutineRunner.StopCoroutine(_reconnectCoroutine);
        _reconnectCoroutine = null;
      }
    }

    private IEnumerator ReconnectCoroutine()
    {
      // If not on first attempt, wait some time before trying again, so that if the issue causing the disconnect
      // is temporary, it has time to fix itself before we try again. Here we are using a simple fixed cooldown
      // but we could want to use exponential backoff instead, to wait a longer time between each failed attempt.
      // See https://en.wikipedia.org/wiki/Exponential_backoff
      if (_attempts > 0)
        yield return new WaitForSeconds(5);

      Debug.Log("Lost connection to host, trying to reconnect...");

      _networkManager.Shutdown();

      yield return
        new WaitWhile(() => _networkManager.ShutdownInProgress); // wait until NetworkManager completes shutting down
      Debug.Log($"Reconnecting attempt {_attempts + 1}/{2}...");
      _attempts++;
      if (!string.IsNullOrEmpty(_lobbyCode)) // Attempting to reconnect to lobby.
      {
        // When using Lobby with Relay, if a user is disconnected from the Relay server, the server will notify
        // the Lobby service and mark the user as disconnected, but will not remove them from the lobby. They
        // then have some time to attempt to reconnect (defined by the "Disconnect removal time" parameter on
        // the dashboard), after which they will be removed from the lobby completely.
        // See https://docs.unity.com/lobby/reconnect-to-lobby.html
        Task<Lobby> reconnectingToLobby = _lobbyService.ReconnectToLobbyAsync(_lobbyInfo?.Id);
        yield return new WaitUntil(() => reconnectingToLobby.IsCompleted);

        // If succeeded, attempt to connect to Relay
        if (!reconnectingToLobby.IsFaulted && reconnectingToLobby.Result != null)
        {
          // If this fails, the OnClientDisconnect callback will be invoked by Netcode
          Task connectingToRelay = _connectionService.ConnectClientAsync(_userName);
          yield return new WaitUntil(() => connectingToRelay.IsCompleted);
        }
        else
        {
          Debug.Log("Failed reconnecting to lobby.");
          // Calling OnClientDisconnect to mark this attempt as failed and either start a new one or give up
          // and return to the Offline state
          ReactToClientDisconnect(0);
        }
      }
      else // If not using Lobby, simply try to reconnect to the server directly
      {
        // If this fails, the OnClientDisconnect callback will be invoked by Netcode
        Task connectingClient = _connectionService.ConnectClientAsync(_userName);
        yield return new WaitUntil(() => connectingClient.IsCompleted);
      }
    }
  }
}