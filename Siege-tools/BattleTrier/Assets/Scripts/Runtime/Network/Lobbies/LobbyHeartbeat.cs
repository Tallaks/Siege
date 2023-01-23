using Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Services.Coroutines;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Data;

namespace Kulinaria.Tools.BattleTrier.Runtime.Network.Lobbies
{
  public class LobbyHeartbeat
  {
    private readonly IUpdateRunner _updateRunner;
    private readonly LobbyInfo _localLobby;
    private readonly UserProfile _localUser;
    private readonly LobbyServiceFacade _lobbyService;

    bool _shouldPushData;
    int _awaitingQueryCount;

    public LobbyHeartbeat(IUpdateRunner updateRunner, LobbyInfo localLobby, UserProfile userProfile,
      LobbyServiceFacade lobbyService)
    {
      _updateRunner = updateRunner;
      _localLobby = localLobby;
      _localUser = userProfile;
      _lobbyService = lobbyService;
    }
    
    public void BeginTracking()
    {
      _updateRunner.Subscribe(OnUpdate, 1.5f);
      _localLobby.OnLobbyChanged += OnLocalLobbyChanged;
      _shouldPushData = true; // Ensure the initial presence of a new player is pushed to the lobby; otherwise, when a non-host joins, the LocalLobby never receives their data until they push something new.
    }

    public void EndTracking()
    {
      _shouldPushData = false;
      _updateRunner.Unsubscribe(OnUpdate);
      _localLobby.OnLobbyChanged -= OnLocalLobbyChanged;
    }

    private void OnLocalLobbyChanged(LobbyInfo lobby)
    {
      if (string.IsNullOrEmpty(lobby.Id)) // When the player leaves, their LocalLobby is cleared out but maintained.
        EndTracking();

      _shouldPushData = true;
    }

    /// <summary>
    /// If there have been any data changes since the last update, push them to Lobby.
    /// (Unless we're already awaiting a query, in which case continue waiting.)
    /// </summary>
    private async void OnUpdate()
    {
      if (_awaitingQueryCount > 0)
        return;

      if (_localUser.IsHost)
        _lobbyService.DoLobbyHeartbeat(1.5f);

      if (_shouldPushData)
      {
        _shouldPushData = false;

        if (_localUser.IsHost)
        {
          _awaitingQueryCount++;
          await _lobbyService.UpdateLobbyDataAsync(_localLobby.GetDataForUnityServices());
          _awaitingQueryCount--;
        }

        _awaitingQueryCount++;
        await _lobbyService.UpdatePlayerDataAsync(_localUser.GetDataForUnityServices());
        _awaitingQueryCount--;
      }
    }
  }
}