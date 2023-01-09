using System.Threading.Tasks;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Authentication;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Runtime.Network.Lobbies
{
  public class LobbyServiceFacade
  {
    private readonly AuthenticationServiceFacade _authentication;
    private readonly UnityLobbyApi _lobbyApi;

    public Lobby CurrentLobby { get; set; }
    
    public LobbyServiceFacade(AuthenticationServiceFacade authentication, UnityLobbyApi lobbyApi)
    {
      _lobbyApi = lobbyApi;
      _authentication = authentication;
    }

    public async Task<(bool Success, Lobby lobby)> TryCreateLobby()
    {
      try
      {
        Lobby lobby = await _lobbyApi.CreateLobby();
        return (true, lobby);
      }
      catch (LobbyServiceException exception)
      {
        Debug.LogError(exception.Reason);
      }

      return (false, null);
    }
  }
}