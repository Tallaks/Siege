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
  public class ClientReconnectingState : ParameterConnectionState<string>, IOnlineState, IClientConnect,
    IClientDisconnect
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

    public void OnClientConnect(ulong clientId) =>
      _connectionStateMachine.Enter<ClientConnectedState, ulong, ConnectionState>(clientId,
        _connectionStateMachine.CurrentState);

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
          Debug.Log(ConnectStatus.GenericDisconnect);
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
      _userName = userName;
      _attempts = 0;
      _lobbyCode = _lobbyService.CurrentLobby != null ? _lobbyService.CurrentLobby.LobbyCode : "";
      _reconnectCoroutine = _coroutineRunner.StartCoroutine(ReconnectCoroutine());
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
      if (_attempts > 0)
        yield return new WaitForSeconds(5);

      Debug.Log("Lost connection to host, trying to reconnect...");

      _networkManager.Shutdown();

      yield return new WaitWhile(() => _networkManager.ShutdownInProgress);
      Debug.Log($"Reconnecting attempt {_attempts + 1}/{2}...");
      _attempts++;
      if (!string.IsNullOrEmpty(_lobbyCode))
      {
        Task<Lobby> reconnectingToLobby = _lobbyService.ReconnectToLobbyAsync(_lobbyInfo?.Id);
        yield return new WaitUntil(() => reconnectingToLobby.IsCompleted);

        if (!reconnectingToLobby.IsFaulted && reconnectingToLobby.Result != null)
        {
          Task connectingToRelay = _connectionService.ConnectClientAsync(_userName);
          yield return new WaitUntil(() => connectingToRelay.IsCompleted);
        }
        else
        {
          Debug.Log("Failed reconnecting to lobby.");
          ReactToClientDisconnect(0);
        }
      }
      else
      {
        Task connectingClient = _connectionService.ConnectClientAsync(_userName);
        yield return new WaitUntil(() => connectingClient.IsCompleted);
      }
    }
  }
}