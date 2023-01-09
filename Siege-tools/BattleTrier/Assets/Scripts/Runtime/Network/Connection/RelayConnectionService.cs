using System;
using System.Collections.Generic;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Connection.States;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Data;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Lobbies;
using Unity.Netcode;
using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Runtime.Network.Connection
{
  public class RelayConnectionService : IConnectionService
  {
    private Dictionary<Type, ConnectionState> _connections;

    private NetworkManager _networkManager;
    private readonly LobbyInfo _lobbyInfo;
    private readonly LobbyServiceFacade _lobbyService;

    public ConnectionState CurrentState { get; private set; }

    public RelayConnectionService(NetworkManager networkManager,  LobbyInfo lobbyInfo, LobbyServiceFacade lobbyService)
    {
      _networkManager = networkManager;
      _lobbyInfo = lobbyInfo;
      _lobbyService = lobbyService;
    }

    public void Initialize()
    {
      _connections = new()
      {
        [typeof(OfflineState)] = new OfflineState(_networkManager),
        [typeof(ClientReconnectingState)] = new ClientReconnectingState(),
        [typeof(ClientConnectingState)] = new ClientConnectingState(),
        [typeof(ClientConnectedState)] = new ClientConnectedState(),
        [typeof(HostingState)] = new HostingState(),
        [typeof(StartingHostState)] = new StartingHostState(this, _networkManager, _lobbyInfo, _lobbyService)
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