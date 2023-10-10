using Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Services.Coroutines;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Data;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Session;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Utilities;
using Unity.Netcode;
using UnityEngine;
using Zenject;

namespace Kulinaria.Tools.BattleTrier.Runtime.Network.Roles
{
  public class RoleSelectionServer : MonoBehaviour
  {
    [SerializeField] private NetCodeHook _hook;
    private ICoroutineRunner _coroutineRunner;
    private NetworkManager _networkManager;
    private RoleSelectionService _roleSelectionService;
    private Session<SessionPlayerData> _session;

    private Coroutine _waitToEndLobbyCoroutine;

    [Inject]
    public void Construct(
      ICoroutineRunner coroutineRunner,
      NetworkManager networkManager,
      Session<SessionPlayerData> session,
      RoleSelectionService roleSelectionService)
    {
      _coroutineRunner = coroutineRunner;
      _networkManager = networkManager;
      _session = session;
      _roleSelectionService = roleSelectionService;
    }

    private void Awake()
    {
      if (_networkManager.IsServer)
      {
        Debug.Log("Role Selection Server Awake", _hook);
        _hook.OnNetworkSpawnHook += OnNetworkSpawn;
        _hook.OnNetworkDeSpawnHook += OnNetworkDespawn;
      }
    }

    private void OnDestroy()
    {
      _hook.OnNetworkSpawnHook -= OnNetworkSpawn;
      _hook.OnNetworkDeSpawnHook -= OnNetworkDespawn;
    }

    private void OnNetworkSpawn()
    {
      Debug.Log("Network spawn: server");
      _networkManager.OnClientDisconnectCallback += OnClientDisconnectCallback;
      _networkManager.SceneManager.OnSceneEvent += OnSceneEvent;
    }

    private void OnNetworkDespawn()
    {
      Debug.Log("Network despawn: server");
      _networkManager.OnClientDisconnectCallback -= OnClientDisconnectCallback;
      _networkManager.SceneManager.OnSceneEvent -= OnSceneEvent;
    }

    private void OnSceneEvent(SceneEvent sceneEvent)
    {
      Debug.Log("Server sceneEvent: " + sceneEvent.SceneEventType);
      if (sceneEvent.SceneEventType != SceneEventType.LoadComplete || sceneEvent.SceneName != "RoleSelection") return;
      SeatNewPlayer(sceneEvent.ClientId);
    }

    private void OnClientDisconnectCallback(ulong clientId)
    {
      for (var i = 0; i < _roleSelectionService.PlayerRoles.Count; ++i)
        if (_roleSelectionService.PlayerRoles[i].ClientId == clientId)
        {
          _roleSelectionService.PlayerRoles.RemoveAt(i);
          break;
        }
    }

    private void SeatNewPlayer(ulong clientId)
    {
      Debug.Log($"Seat new player {clientId}");
      if (_roleSelectionService.LobbyIsClosed.Value)
        CancelCloseLobby();

      SessionPlayerData? sessionPlayerData = _session.GetPlayerData(clientId);
      if (sessionPlayerData.HasValue)
      {
        SessionPlayerData playerData = sessionPlayerData.Value;

        if (_roleSelectionService.PlayerRoles == null)
          _roleSelectionService.PlayerRoles = new NetworkList<PlayerRoleState>();
        _roleSelectionService.PlayerRoles.Add(new PlayerRoleState(clientId, RoleState.NotChosen));
        _session.SetPlayerData(clientId, playerData);
      }
    }

    private void CancelCloseLobby()
    {
      if (_waitToEndLobbyCoroutine != null)
        _coroutineRunner.StopCoroutine(_waitToEndLobbyCoroutine);

      _roleSelectionService.LobbyIsClosed.Value = false;
    }
  }
}