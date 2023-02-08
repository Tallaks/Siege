using System.Collections;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay;
using Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Services.Coroutines;
using Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Services.Scenes;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Utilities;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Kulinaria.Tools.BattleTrier.Runtime.Roles
{
  public class RoleSelectionServer : RoleSelectionBase
  {
    private readonly NetCodeHook _hook;
    private readonly NetworkManager _networkManager;
    private readonly ICoroutineRunner _coroutineRunner;
    private readonly ISceneLoader _sceneLoader;

    private NetworkRoleSelection _networkRoleSelection;
    private Coroutine _waitToEndLobbyCoroutine;

    public RoleSelectionServer(ICoroutineRunner coroutineRunner, ISceneLoader sceneLoader, NetworkManager networkManager, NetCodeHook hook)
    {
      _sceneLoader = sceneLoader;
      _coroutineRunner = coroutineRunner;
      _networkManager = networkManager;
      _hook = hook;
    }

    public override void Initialize()
    {
      _hook.OnNetworkSpawnHook += OnNetworkSpawn;
      _hook.OnNetworkDeSpawnHook += OnNetworkDespawn;
    }

    private void OnNetworkSpawn()
    {
      _networkManager.OnClientConnectedCallback += OnClientDisconnectCallback;
      _networkManager.SceneManager.OnSceneEvent += OnSceneEvent;
    }

    private void OnNetworkDespawn()
    {
      _networkManager.OnClientConnectedCallback -= OnClientDisconnectCallback;
      _networkManager.SceneManager.OnSceneEvent -= OnSceneEvent;
    }

    private void OnSceneEvent(SceneEvent sceneEvent)
    {
    }

    private void OnClientDisconnectCallback(ulong clientId)
    {
      for (var i = 0; i < _networkRoleSelection.PlayerRoles.Count; ++i)
      {
        if (_networkRoleSelection.PlayerRoles[i].ClientId == clientId)
        {
          _networkRoleSelection.PlayerRoles.RemoveAt(i);
          break;
        }
      }

      if (!_networkRoleSelection.IsLobbyClosed.Value)
        CloseLobbyIfReady();
    }

    private void CloseLobbyIfReady()
    {
      foreach (PlayerRole playerInfo in _networkRoleSelection.PlayerRoles)
      {
        if (playerInfo.SeatState != SeatState.LockedIn)
          return;
      }

      _networkRoleSelection.IsLobbyClosed.Value = true;

      SaveLobbyResults();

      _waitToEndLobbyCoroutine = _coroutineRunner.StartCoroutine(WaitToEndLobby());
    }

    private void SaveLobbyResults()
    {
      foreach (PlayerRole playerInfo in _networkRoleSelection.PlayerRoles)
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