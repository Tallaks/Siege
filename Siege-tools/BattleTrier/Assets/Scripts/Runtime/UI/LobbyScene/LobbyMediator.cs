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

    [Inject]
    private void Construct(LobbyServiceFacade lobbyService) =>
      _lobbyService = lobbyService;

    public void Initialize() => _lobbyUi.Initialize();

    public async void TryCreateLobby(string lobbyName)
    {
      (bool Success, Lobby lobby) lobbyCreationAttempt = await _lobbyService.TryCreateLobby(lobbyName);
    }
  }
}