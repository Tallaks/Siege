using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;

namespace Kulinaria.Tools.BattleTrier.Runtime.Network.Lobbies
{
  public class UnityLobbyApi
  {
    public const int MaxPlayers = 8;
    
    public async Task<Lobby> CreateLobby(string lobbyName)
    {
      var options = new CreateLobbyOptions()
      {
        Player = new Player(AuthenticationService.Instance.PlayerId, data: new Dictionary<string, PlayerDataObject>
        {
          { "DisplayName", new PlayerDataObject(PlayerDataObject.VisibilityOptions.Member, "Player1") },
        })
      };

      return await LobbyService.Instance.CreateLobbyAsync(lobbyName, MaxPlayers, options);
    }

    public async Task<Lobby> UpdateLobby(string currentLobbyId, Dictionary<string,DataObject> data, bool shouldLock)
    {
      var updateOptions = new UpdateLobbyOptions { Data = data, IsLocked = shouldLock };
      return await LobbyService.Instance.UpdateLobbyAsync(currentLobbyId, updateOptions);
    }

    public async Task<Lobby> UpdatePlayer(string currentLobbyId, string playerId, Dictionary<string,PlayerDataObject> data, string allocationId, string connectionInfo)
    {
      UpdatePlayerOptions updateOptions = new UpdatePlayerOptions
      {
        Data = data,
        AllocationId = allocationId,
        ConnectionInfo = connectionInfo
      };
      return await LobbyService.Instance.UpdatePlayerAsync(currentLobbyId, playerId, updateOptions);
    }

    public async Task<Lobby> ReconnectToLobby(string lobbyId) => 
      await LobbyService.Instance.ReconnectToLobbyAsync(lobbyId);

    public async Task RemovePlayerFromLobby(string playerId, string currentLobbyId)
    {
      try
      {
        await LobbyService.Instance.RemovePlayerAsync(currentLobbyId, playerId);
      }
      catch (LobbyServiceException e)
        when (e is { Reason: LobbyExceptionReason.PlayerNotFound })
      {
        // If Player is not found, they have already left the lobby or have been kicked out. No need to throw here
      }
    }

    public async Task<Lobby> GetLobby(string lobbyId) => 
      await LobbyService.Instance.GetLobbyAsync(lobbyId);

    public async Task DeleteLobby(string lobbyId) => 
      await LobbyService.Instance.DeleteLobbyAsync(lobbyId);
    
    public async void SendHeartbeatPing(string lobbyId) => 
      await LobbyService.Instance.SendHeartbeatPingAsync(lobbyId);
  }
}