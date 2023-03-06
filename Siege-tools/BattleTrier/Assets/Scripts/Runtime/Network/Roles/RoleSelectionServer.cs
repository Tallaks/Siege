using System.Collections;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay;
using Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Services.Coroutines;
using Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Services.Scenes;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Data;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Session;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Utilities;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Kulinaria.Tools.BattleTrier.Runtime.Network.Roles
{
  public class RoleSelectionServer : MonoBehaviour
  {
    [SerializeField] private NetCodeHook _hook;
    private ICoroutineRunner _coroutineRunner;
    private ISceneLoader _sceneLoader;
    private NetworkManager _networkManager;
    private RoleSelectionService _roleSelectionService;
    private Session<SessionPlayerData> _session;

    private Coroutine _waitToEndLobbyCoroutine;

    public bool IsAwaken = false;

    [Inject]
    public void Construct(
      ICoroutineRunner coroutineRunner,
      ISceneLoader sceneLoader,
      NetworkManager networkManager,
      Session<SessionPlayerData> session,
      RoleSelectionService roleSelectionService)
    {
      _coroutineRunner = coroutineRunner;
      _sceneLoader = sceneLoader;
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
        IsAwaken = true;
      }
    }

    private void OnDestroy()
    {
      Debug.Log("Role Selection Server Initialization");
      _hook.OnNetworkSpawnHook -= OnNetworkSpawn;
      _hook.OnNetworkDeSpawnHook -= OnNetworkDespawn;
    }

    private void OnNetworkSpawn()
    {
      Debug.Log("Network spawn: server");

      _networkManager.OnClientDisconnectCallback += OnClientDisconnectCallback;
      _roleSelectionService.OnClientChoseRole += OnClientChoseRole;
      _networkManager.SceneManager.OnSceneEvent += OnSceneEvent;
    }

    private void OnNetworkDespawn()
    {
      Debug.Log("Network despawn: server");

      _networkManager.OnClientDisconnectCallback -= OnClientDisconnectCallback;
      _roleSelectionService.OnClientChoseRole -= OnClientChoseRole;
      _networkManager.SceneManager.OnSceneEvent -= OnSceneEvent;
    }

    private void OnClientChoseRole(ulong clientId, int roleButtonId)
    {
      for (int i = 0; i < _roleSelectionService.PlayerRoles.Count; i++)
      {
        if (_roleSelectionService.PlayerRoles[i].ClientId == clientId &&
            _roleSelectionService.PlayerRoles[i].State != (RoleState)roleButtonId)
        {
          _roleSelectionService.PlayerRoles[i] = new PlayerRoleState(
            clientId,
            (RoleState)roleButtonId,
            Time.time
          );

          if (_roleSelectionService.PlayerRoles[i].ClientId != clientId &&
              _roleSelectionService.PlayerRoles[i].State == (RoleState)roleButtonId)
          {
            _roleSelectionService.PlayerRoles[i] = new PlayerRoleState(
              clientId,
              (RoleState)roleButtonId,
              Time.time
            );
          }
        }
      }

      CloseLobbyIfReady();
    }

    private void OnSceneEvent(SceneEvent sceneEvent)
    {
      Debug.Log("Server sceneEvent: " + sceneEvent.SceneEventType);
      if (sceneEvent.SceneEventType != SceneEventType.LoadComplete) return;
      SeatNewPlayer(sceneEvent.ClientId);
    }

    private void OnClientDisconnectCallback(ulong clientId)
    {
      for (var i = 0; i < _roleSelectionService.PlayerRoles.Count; ++i)
      {
        if (_roleSelectionService.PlayerRoles[i].ClientId == clientId)
        {
          _roleSelectionService.PlayerRoles.RemoveAt(i);
          break;
        }
      }

      if (!_roleSelectionService.LobbyIsClosed.Value)
        CloseLobbyIfReady();
    }

    private void CloseLobbyIfReady()
    {
      
    }

    private void SaveLobbyResults()
    {
      foreach (PlayerRoleState playerInfo in _roleSelectionService.PlayerRoles)
      {
        NetworkObject playerNetworkObject = _networkManager.SpawnManager.GetPlayerNetworkObject(playerInfo.ClientId);

        if (playerNetworkObject && playerNetworkObject.TryGetComponent(out Player persistentPlayer))
        {
        }
      }
    }

    private IEnumerator WaitToEndLobby()
    {
      yield return new WaitForSeconds(3);
      _sceneLoader.LoadScene("BossRoom", true, LoadSceneMode.Single);
    }

    private void SeatNewPlayer(ulong clientId)
    {
      Debug.Log($"Seat new player {clientId}");
      if (_roleSelectionService.LobbyIsClosed.Value == true)
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