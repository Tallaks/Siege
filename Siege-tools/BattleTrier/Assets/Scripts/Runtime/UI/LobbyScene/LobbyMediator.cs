using Kulinaria.Tools.BattleTrier.Runtime.Network.Connection;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Connection.States;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Data;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Lobbies;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using Zenject;

namespace Kulinaria.Tools.BattleTrier.Runtime.UI.LobbyScene
{
  public class LobbyMediator : MonoBehaviour
  {
    [SerializeField] private LobbyUi _lobbyUi;

    private LobbyServiceFacade _lobbyService;
    private UserProfile _localUser;
    private LobbyInfo _lobbyInfo;
    private IConnectionStateMachine _connectionStateMachine;
    private IConnectionService _connectionService;

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

    public void Initialize() => _lobbyUi.Initialize();

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

    public void OnJoinedLobby(Lobby remoteLobby)
    {
      _lobbyService.SetRemoteLobby(remoteLobby);

      Debug.Log(
        $"Joined lobby with code: {_lobbyInfo.Code}, Internal Relay Join Code{_lobbyInfo.RelayJoinCode}");

      _connectionStateMachine.Enter<ClientReconnectingState, string>(_localUser.Name);
    }
  }
}