using System.Collections.Generic;
using System.Threading.Tasks;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Authentication;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Data;
using Unity.Services.Authentication;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Runtime.Network.Lobbies
{
  public class LobbyServiceFacade
  {
    private readonly AuthenticationServiceFacade _authentication;
    private readonly UnityLobbyApi _lobbyApi;
    private readonly LobbyInfo _lobbyInfo;
    private UserProfile _localUser;

    public Lobby CurrentLobby { get; set; }
    
    public LobbyServiceFacade(
      AuthenticationServiceFacade authentication,
      UnityLobbyApi lobbyApi,
      LobbyInfo lobbyInfo,
      UserProfile localUser)
    {
      _localUser = localUser;
      _lobbyApi = lobbyApi;
      _lobbyInfo = lobbyInfo;
      _authentication = authentication;
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

    public async Task UpdateLobbyDataAsync(Dictionary<string,DataObject> dataObjects)
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
        await _lobbyApi.UpdatePlayer(CurrentLobby.Id, AuthenticationService.Instance.PlayerId, new Dictionary<string, PlayerDataObject>(), allocationId, connectionInfo);
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
  }
}