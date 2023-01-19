using System;
using System.Collections.Generic;
using Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Services.Coroutines;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Data;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Lobbies;
using Unity.Netcode;
using UnityEngine;
using Zenject;

namespace Kulinaria.Tools.BattleTrier.Runtime.Network.Connection.States
{
  public class ConnectionStateMachine : IConnectionStateMachine
  {
    private Dictionary<Type, ConnectionState> _connections;

    private NetworkManager _networkManager;
    private readonly LobbyInfo _lobbyInfo;
    private readonly LobbyServiceFacade _lobbyService;
    private ICoroutineRunner _coroutineRunner;
    [Inject] private IConnectionService _connectionService;

    public ConnectionState CurrentState { get; private set; }

    public ConnectionStateMachine(
      ICoroutineRunner coroutineRunner,
      NetworkManager networkManager,
      LobbyInfo lobbyInfo,
      LobbyServiceFacade lobbyService)
    {
      _coroutineRunner = coroutineRunner;
      _networkManager = networkManager;
      _lobbyInfo = lobbyInfo;
      _lobbyService = lobbyService;
    }

    public void Initialize()
    {
      _connections = new()
      {
        [typeof(OfflineState)] = new OfflineState(_networkManager),
        [typeof(ClientReconnectingState)] = new ClientReconnectingState(this, _coroutineRunner, _connectionService, _networkManager, _lobbyService, _lobbyInfo),
        [typeof(ClientConnectingState)] = new ClientConnectingState(this),
        [typeof(ClientConnectedState)] = new ClientConnectedState(this),
        [typeof(HostingState)] = new HostingState(this, _networkManager, _lobbyService),
        [typeof(StartingHostState)] = new StartingHostState(this, _connectionService, _networkManager, _lobbyInfo)
      };
    }

    public void Enter<TState>() where TState : NonParameterConnectionState
    {
      var state = ChangeState<TState>();
      state.Enter();
    }

    public void Enter<TState, TPayload>(TPayload payload) where TState : ParameterConnectionState<TPayload>
    {
      var state = ChangeState<TState>();
      state.Enter(payload);
    }

    public void Enter<TState, TPayload1, TPayLoad2>(TPayload1 payload1, TPayLoad2 payLoad2) where TState : ParameterConnectionState<TPayload1, TPayLoad2>
    {
      var state = ChangeState<TState>();
      state.Enter(payload1, payLoad2);
    }

    private TState ChangeState<TState>() where TState : ConnectionState
    {
      CurrentState?.Exit();

      var state = GetState<TState>();

      Debug.Log($"Changed connection state from {CurrentState?.GetType().Name} to {state.GetType().Name}.");
      CurrentState = state;

      return state;
    }

    private TState GetState<TState>() where TState : ConnectionState =>
      _connections[typeof(TState)] as TState;
  }
}