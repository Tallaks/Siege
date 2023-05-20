using System.Collections.Generic;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Connection;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Connection.States;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Data;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Lobbies;
using Unity.Services.Core;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using Zenject;

namespace Kulinaria.Tools.BattleTrier.Runtime.UI.LobbyScene
{
  public class LobbyMediator : MonoBehaviour
  {
    [SerializeField] private LobbyList _lobbyList;
    [SerializeField] private LobbyUi _lobbyUi;
    private IConnectionService _connectionService;
    private IConnectionStateMachine _connectionStateMachine;
    private LobbyInfo _lobbyInfo;

    private LobbyServiceFacade _lobbyService;
    private UserProfile _localUser;

    [Inject]
    private void Construct(LobbyServiceFacade lobbyService, UserProfile userProfile, LobbyInfo lobbyInfo,
      IConnectionStateMachine connectionStateMachine, IConnectionService connectionService)
    {
      _connectionService = connectionService;
      _lobbyInfo = lobbyInfo;
      _localUser = userProfile;
      _lobbyService = lobbyService;
      _connectionStateMachine = connectionStateMachine;
    }

    public void Initialize()
    {
      _lobbyUi.Initialize();
      _lobbyList.Initialize();
    }

    public async void JoinLobbyRequest(LobbyInfo lobby)
    {
      BlockUIWhileLoadingIsInProgress();

      (bool Success, Lobby Lobby) result = await _lobbyService.TryJoinLobbyAsync(lobby.Id, lobby.Code);

      if (result.Success)
        OnJoinedLobby(result.Lobby);
      else
        UnblockUIAfterLoadingIsComplete();
    }

    public async void QueryLobbiesRequest(bool blockUI)
    {
      if (UnityServices.State != ServicesInitializationState.Initialized)
        return;

      if (blockUI)
        BlockUIWhileLoadingIsInProgress();

      List<Lobby> lobbies = await _lobbyService.RetrieveAndPublishLobbyListAsync();
      _lobbyList.UpdateList(lobbies);

      if (blockUI)
        UnblockUIAfterLoadingIsComplete();
    }

    public async void TryCreateLobby(string lobbyName)
    {
      (bool Success, Lobby Lobby) lobbyCreationAttempt = await _lobbyService.TryCreateLobby(lobbyName);
      if (lobbyCreationAttempt.Success)
      {
        _localUser.IsHost = true;
        _lobbyService.SetRemoteLobby(lobbyCreationAttempt.Lobby);

        Debug.Log($"Created lobby with ID: {_lobbyInfo.Id} and code {_lobbyInfo.Code}");
        _connectionStateMachine.Enter<StartingHostState, string>(_localUser.Name);
      }
    }


    private void OnJoinedLobby(Lobby remoteLobby)
    {
      _lobbyService.SetRemoteLobby(remoteLobby);

      Debug.Log(
        $"Joined lobby with code: {_lobbyInfo.Code}, Internal Relay Join Code{_lobbyInfo.RelayJoinCode}");

      _connectionStateMachine.Enter<ClientConnectingState, string>(_localUser.Name);
    }

    private void BlockUIWhileLoadingIsInProgress() =>
      _lobbyUi.Block();

    private void UnblockUIAfterLoadingIsComplete() =>
      _lobbyUi.Unblock();
  }
}