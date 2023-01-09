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
  }
}