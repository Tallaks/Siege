using Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Services.Scenes;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Lobbies;
using Unity.Netcode;
using UnityEngine.SceneManagement;

namespace Kulinaria.Tools.BattleTrier.Runtime.Network.Connection.States
{
  public class OfflineState : NonParameterConnectionState
  {
    private readonly NetworkManager _networkManager;
    private readonly ISceneLoader _sceneLoader;
    private readonly LobbyServiceFacade _lobbyServiceFacade;

    public OfflineState(NetworkManager networkManager, ISceneLoader sceneLoader, LobbyServiceFacade lobbyServiceFacade)
    {
      _networkManager = networkManager;
      _sceneLoader = sceneLoader;
      _lobbyServiceFacade = lobbyServiceFacade;
    }

    public override void Enter()
    {
      _lobbyServiceFacade.EndTracking();
      _networkManager.Shutdown();
      if(SceneManager.GetActiveScene().name == "RoleSelection")
        _sceneLoader.LoadScene("Lobby", false, LoadSceneMode.Single);
    }

    public override void Exit()
    {
    }
  }
}