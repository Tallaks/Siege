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
  }
}