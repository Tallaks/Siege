using System.Collections.Generic;
using System.Threading.Tasks;
using Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Services.Coroutines;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Authentication;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Cooldown;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Data;
using Unity.Services.Authentication;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using Zenject;

namespace Kulinaria.Tools.BattleTrier.Runtime.Network.Lobbies
{
  public class LobbyServiceFacade
  {
    private readonly AuthenticationServiceFacade _authentication;
    private readonly UnityLobbyApi _lobbyApi;
    private readonly LobbyInfo _lobbyInfo;
    private readonly IUpdateRunner _updateRunner;
    private readonly UserProfile _localUser;
    [Inject]private LobbyHeartbeat _lobbyHeartbeat;

    private RateLimitCooldown _queryForLobbiesLimit;
    private bool _isTracking;
    private float _heartbeatTime;
    public Lobby CurrentLobby { get; set; }

    public LobbyServiceFacade(
      IUpdateRunner updateRunner,
      AuthenticationServiceFacade authentication,
      UnityLobbyApi lobbyApi,
      LobbyInfo lobbyInfo,
      UserProfile localUser)
    {
      _updateRunner = updateRunner;
      _localUser = localUser;
      _lobbyApi = lobbyApi;
      _lobbyInfo = lobbyInfo;
      _authentication = authentication;

      _queryForLobbiesLimit = new RateLimitCooldown(1f);
    }

    public async Task<(bool Success, Lobby Lobby)> TryCreateLobby(string lobbyName)
    {
      try
      {
        Lobby lobby = await _lobbyApi.CreateLobby(lobbyName);
        return (true, lobby);
      }
      catch (LobbyServiceException exception)
      {
        Debug.LogError(exception.Reason);
      }

      return (false, null);
    }

    public void SetRemoteLobby(Lobby lobby)
    {
      CurrentLobby = lobby;
      _lobbyInfo.ApplyRemoteData(lobby);
    }

    public void StartTracking()
    {
      if (!_isTracking)
      {
        _isTracking = true;
        _updateRunner.Subscribe(UpdateLobby, 2f);
        _lobbyHeartbeat.BeginTracking();
      }
    }

    public void DoLobbyHeartbeat(float deltaTime)
    {
      _heartbeatTime += deltaTime;
      if (_heartbeatTime > 8)
      {
        _heartbeatTime -= 8;
        try
        {
          _lobbyApi.SendHeartbeatPing(CurrentLobby.Id);
        }
        catch (LobbyServiceException e)
        {
          if (e.Reason != LobbyExceptionReason.LobbyNotFound && !_localUser.IsHost)
            Debug.LogError(e.Reason);
        }
      }
    }
    
    public async Task UpdateLobbyDataAsync(Dictionary<string, DataObject> dataObjects)
    {
      Dictionary<string, DataObject> dataCurr = CurrentLobby.Data ?? new Dictionary<string, DataObject>();

      foreach (var dataNew in dataObjects)
      {
        if (dataCurr.ContainsKey(dataNew.Key))
        {
          dataCurr[dataNew.Key] = dataNew.Value;
        }
        else
        {
          dataCurr.Add(dataNew.Key, dataNew.Value);
        }
      }

      //we would want to lock lobbies from appearing in queries if we're in relay mode and the relay isn't fully set up yet
      bool shouldLock = string.IsNullOrEmpty(_lobbyInfo.RelayJoinCode);

      try
      {
        Lobby result = await _lobbyApi.UpdateLobby(CurrentLobby.Id, dataCurr, shouldLock);

        if (result != null)
        {
          CurrentLobby = result;
        }
      }
      catch (LobbyServiceException e)
      {
        Debug.LogError(e.Reason);
      }
    }

    public async Task UpdatePlayerRelayInfoAsync(string allocationId, string connectionInfo)
    {
      try
      {
        await _lobbyApi.UpdatePlayer(CurrentLobby.Id, AuthenticationService.Instance.PlayerId,
          new Dictionary<string, PlayerDataObject>(), allocationId, connectionInfo);
      }
      catch (LobbyServiceException e)
      {
        Debug.LogError(e.Reason);
      }
    }

    public async Task<Lobby> ReconnectToLobbyAsync(string lobbyId)
    {
      try
      {
        return await _lobbyApi.ReconnectToLobby(lobbyId);
      }
      catch (LobbyServiceException e)
      {
        // If Lobby is not found and if we are not the host, it has already been deleted. No need to publish the error here.
        if (e.Reason != LobbyExceptionReason.LobbyNotFound && !_localUser.IsHost)
          Debug.LogError(e.Reason);
      }

      return null;
    }

    public async void RemovePlayerFromLobbyAsync(string playerId, string currentLobbyId)
    {
      if (_localUser.IsHost)
      {
        try
        {
          await _lobbyApi.RemovePlayerFromLobby(playerId, currentLobbyId);
        }
        catch (LobbyServiceException e)
        {
          Debug.LogError(e.Reason);
        }
      }
      else
      {
        Debug.LogError("Only the host can remove other players from the lobby.");
      }
    }

    public async Task UpdatePlayerDataAsync(Dictionary<string, PlayerDataObject> data)
    {
      if(!_queryForLobbiesLimit.CanCall)
        return;

      try
      {
        Lobby result = await _lobbyApi.UpdatePlayer(CurrentLobby.Id,
          _authentication.PlayerId, data, null, null);

        if (result != null)
        {
          CurrentLobby =
            result; // Store the most up-to-date lobby now since we have it, instead of waiting for the next heartbeat.
        }
      }
      catch (LobbyServiceException e)
      {
        if (e.Reason == LobbyExceptionReason.RateLimited)
          _queryForLobbiesLimit.PutOnCooldown();
        else if (e.Reason != LobbyExceptionReason.LobbyNotFound &&
                 !_localUser.
                   IsHost) // If Lobby is not found and if we are not the host, it has already been deleted. No need to publish the error here.
        {
          Debug.LogError(e.Reason);
        }
      }
    }

    private async void UpdateLobby()
    {
      if(!_queryForLobbiesLimit.CanCall)
        return;
      
      try
      {
        Lobby lobby = await _lobbyApi.GetLobby(_lobbyInfo.Id);
        CurrentLobby = lobby;
        _lobbyInfo.ApplyRemoteData(lobby);

        if (!_localUser.IsHost)
        {
          foreach (KeyValuePair<string, UserProfile> lobbyUser in _lobbyInfo.LobbyUsers)
          {
            if (lobbyUser.Value.IsHost)
              return;
          }

          Debug.LogWarning("Host left the lobby, disconnecting...");
          await EndTracking();
        }
      }
      catch (LobbyServiceException e)
      {
        Debug.LogError(e.Reason);
      }
    }

    private Task EndTracking()
    {
      var task = Task.CompletedTask;
      if (CurrentLobby != null)
      {
        CurrentLobby = null;

        string lobbyId = _lobbyInfo?.Id;

        if (!string.IsNullOrEmpty(lobbyId))
        {
          if (_localUser.IsHost)
            task = DeleteLobbyAsync(lobbyId);
          else
            task = LeaveLobbyAsync(lobbyId);
        }

        _localUser.ResetState();
        _lobbyInfo?.Reset(_localUser);
      }

      if (_isTracking)
      {
        _updateRunner.Unsubscribe(UpdateLobby);
        _isTracking = false;
        _lobbyHeartbeat.EndTracking();
      }

      return task;
    }

    private async Task LeaveLobbyAsync(string lobbyId)
    {
      string uasId = _authentication.PlayerId;
      try
      {
        await _lobbyApi.RemovePlayerFromLobby(uasId, lobbyId);
      }
      catch (LobbyServiceException e)
      {
        // If Lobby is not found and if we are not the host, it has already been deleted. No need to publish the error here.
        if (e.Reason != LobbyExceptionReason.LobbyNotFound && !_localUser.IsHost)
          Debug.LogError(e.Reason);
      }
    }

    private async Task DeleteLobbyAsync(string lobbyId)
    {
      if (_localUser.IsHost)
      {
        try
        {
          await _lobbyApi.DeleteLobby(lobbyId);
        }
        catch (LobbyServiceException e)
        {
          Debug.LogError(e.Reason);
        }
      }
    }

    public async Task<List<Lobby>> RetrieveAndPublishLobbyListAsync()
    {
      if (!_queryForLobbiesLimit.CanCall)
      {
        Debug.LogWarning("Retrieve Lobby list hit the rate limit. Will try again soon...");
        return new List<Lobby>();
      }

      try
      {
        QueryResponse response = await _lobbyApi.QueryAllLobbies();
        return response.Results;
      }
      catch (LobbyServiceException e)
      {
        if (e.Reason == LobbyExceptionReason.RateLimited)
          _queryForLobbiesLimit.PutOnCooldown();
        else
          Debug.LogError(e.Reason);
      }

      return null;
    }
  }
}