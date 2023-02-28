using System;
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

namespace Kulinaria.Tools.BattleTrier.Runtime.Network.Roles
{
  public class RoleSelectionServer : IDisposable
  {
    private readonly ICoroutineRunner _coroutineRunner;
    private readonly ISceneLoader _sceneLoader;
    private readonly NetworkManager _networkManager;
    private readonly RoleSelectionService _roleSelectionService;
    private readonly NetCodeHook _hook;
    private readonly Session<SessionPlayerData> _session;

    private Coroutine _waitToEndLobbyCoroutine;

    public RoleSelectionServer(
      ICoroutineRunner coroutineRunner,
      ISceneLoader sceneLoader,
      NetworkManager networkManager,
      Session<SessionPlayerData> session,
      RoleSelectionService roleSelectionService,
      NetCodeHook hook)
    {
      _coroutineRunner = coroutineRunner;
      _sceneLoader = sceneLoader;
      _networkManager = networkManager;
      _session = session;
      _roleSelectionService = roleSelectionService;
      _hook = hook;
    }

    public void Initialize()
    {
      Debug.Log("Role Selection Server Initialization", _hook);
      _hook.OnNetworkSpawnHook += OnNetworkSpawn;
      _hook.OnNetworkDeSpawnHook += OnNetworkDespawn;
    }

    public void Dispose()
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
      
      SeatNewPlayer(_networkManager.LocalClientId);
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
      int indexOfPlayerStatesOfClientId = FindLobbyPlayerIdx(clientId);
      if (_roleSelectionService.LobbyIsClosed.Value == true)
        return;

      _roleSelectionService.PlayerRoles[indexOfPlayerStatesOfClientId] = new PlayerRoleState(
        clientId,
        RoleState.Chosen,
        roleButtonId,
        Time.time
      );

      CloseLobbyIfReady();
    }

    private void OnSceneEvent(SceneEvent sceneEvent)
    {
      Debug.Log("Server sceneEvent: " + sceneEvent.SceneEventType);
      if(sceneEvent.SceneEventType != SceneEventType.LoadComplete) return;
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
      foreach (var playerInfo in _roleSelectionService.PlayerRoles)
      {
        if (playerInfo.State != RoleState.Chosen)
          return;
      }

      _roleSelectionService.LobbyIsClosed.Value = true;

      SaveLobbyResults();

      _waitToEndLobbyCoroutine = _coroutineRunner.StartCoroutine(WaitToEndLobby());
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

    private int FindLobbyPlayerIdx(ulong clientId)
    {
      for (var i = 0; i < _roleSelectionService.PlayerRoles.Count; ++i)
      {
        if (_roleSelectionService.PlayerRoles[i].ClientId == clientId)
          return i;
      }

      return -1;
    }

    private void SeatNewPlayer(ulong clientId)
    {
      if (_roleSelectionService.LobbyIsClosed.Value == true)
        CancelCloseLobby();
      
      SessionPlayerData? sessionPlayerData = _session.GetPlayerData(clientId);
      if (sessionPlayerData.HasValue)
      {
        SessionPlayerData playerData = sessionPlayerData.Value;

        _roleSelectionService.PlayerRoles.Add(new PlayerRoleState(clientId, RoleState.Inactive));
        _session.SetPlayerData(clientId, playerData);
      }
    }

    private void CancelCloseLobby()
    {
      if(_waitToEndLobbyCoroutine != null)
        _coroutineRunner.StopCoroutine(_waitToEndLobbyCoroutine);

      _roleSelectionService.LobbyIsClosed.Value = false;
    }
  }
}