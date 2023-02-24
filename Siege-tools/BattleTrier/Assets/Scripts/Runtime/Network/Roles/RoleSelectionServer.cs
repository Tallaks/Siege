using System;
using System.Collections;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay;
using Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Services.Coroutines;
using Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Services.Scenes;
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

    private Coroutine _waitToEndLobbyCoroutine;

    public RoleSelectionServer(
      ICoroutineRunner coroutineRunner,
      ISceneLoader sceneLoader,
      NetworkManager networkManager,
      RoleSelectionService roleSelectionService,
      NetCodeHook hook)
    {
      _roleSelectionService = roleSelectionService;
      _sceneLoader = sceneLoader;
      _coroutineRunner = coroutineRunner;
      _networkManager = networkManager;
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

      _networkManager.OnClientConnectedCallback += OnClientDisconnectCallback;
      _roleSelectionService.OnClientChangedRole += OnClientChangedRole;
      _networkManager.SceneManager.OnSceneEvent += OnSceneEvent;
    }

    private void OnNetworkDespawn()
    {
      Debug.Log("Network despawn: server");
      
      _networkManager.OnClientConnectedCallback -= OnClientDisconnectCallback;
      _roleSelectionService.OnClientChangedRole -= OnClientChangedRole;
      _networkManager.SceneManager.OnSceneEvent -= OnSceneEvent;
    }

    private void OnClientChangedRole(ulong clientId, int newRoleId, bool lockedIn)
    {
      var idx = FindLobbyPlayerIdx(clientId);
      if (_roleSelectionService.LobbyIsClosed.Value)
        return;

      if (newRoleId == 0)
        lockedIn = false;
      else
      {
        foreach (PlayerRoleState playerInfo in _roleSelectionService.PlayerRoles)
        {
          if (playerInfo.ClientId != clientId && playerInfo.RoleId == newRoleId &&
              playerInfo.State == RoleState.Chosen)
          {
            _roleSelectionService.PlayerRoles[idx] = new PlayerRoleState(clientId, RoleState.Inactive);
            return;
          }
        }
      }

      _roleSelectionService.PlayerRoles[idx] = new PlayerRoleState(
        clientId,
        lockedIn ? RoleState.Chosen : RoleState.Active,
        newRoleId,
        Time.time);

      if (lockedIn)
      {
        for (int i = 0; i < _roleSelectionService.PlayerRoles.Count; ++i)
        {
          if (_roleSelectionService.PlayerRoles[i].RoleId == newRoleId && i != idx)
          {
            _roleSelectionService.PlayerRoles[i] = new PlayerRoleState(
              _roleSelectionService.PlayerRoles[i].ClientId,
              RoleState.Inactive);
          }
        }
      }

      CloseLobbyIfReady();
    }

    private int FindLobbyPlayerIdx(ulong clientId)
    {
      for (int i = 0; i < _roleSelectionService.PlayerRoles.Count; ++i)
      {
        if (_roleSelectionService.PlayerRoles[i].ClientId == clientId)
          return i;
      }

      return -1;
    }

    private void OnSceneEvent(SceneEvent sceneEvent)
    {
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
  }
}