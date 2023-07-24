using System.Collections;
using Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Services.Coroutines;
using Unity.Netcode;
using UnityEngine.SceneManagement;

namespace Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Services.Scenes
{
  public class SceneLoader : ISceneLoader
  {
    private readonly ICoroutineRunner _coroutineRunner;
    private readonly NetworkManager _networkManager;

    private bool IsNetworkSceneManagementEnabled => _networkManager != null &&
                                                    _networkManager.SceneManager != null &&
                                                    _networkManager.NetworkConfig.EnableSceneManagement;

    public SceneLoader(ICoroutineRunner coroutineRunner, NetworkManager networkManager)
    {
      _coroutineRunner = coroutineRunner;
      _networkManager = networkManager;
    }

    public void LoadScene(string name, bool useNetwork, LoadSceneMode mode = LoadSceneMode.Single)
    {
      if (useNetwork)
      {
        if (!_networkManager.ShutdownInProgress)
          if (_networkManager.IsServer)
            // If is active server and NetworkManager uses scene management, load scene using NetworkManager's SceneManager
            _networkManager.SceneManager.LoadScene(name, mode);
      }
      else
      {
        // Load using SceneManager
        _coroutineRunner.StartCoroutine(LoadCoroutine(name, mode));
      }
    }

    public void UnloadScene(string name) =>
      SceneManager.UnloadSceneAsync(name, UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);

    public void AddOnSceneEventCallback()
    {
      if (IsNetworkSceneManagementEnabled)
        _networkManager.SceneManager.OnSceneEvent += OnSceneEvent;
    }

    private void OnSceneEvent(SceneEvent sceneEvent)
    {
      switch (sceneEvent.SceneEventType)
      {
        case SceneEventType.Load:
          if (_networkManager.IsClient)
            if (sceneEvent.LoadSceneMode == LoadSceneMode.Single)
            {
              /*m_ClientLoadingScreen.StartLoadingScreen(sceneEvent.SceneName);
              m_LoadingProgressManager.LocalLoadOperation = sceneEvent.AsyncOperation;*/
            }

          /*  m_ClientLoadingScreen.UpdateLoadingScreen(sceneEvent.SceneName);
              m_LoadingProgressManager.LocalLoadOperation = sceneEvent.AsyncOperation;*/
          break;
        case SceneEventType.LoadEventCompleted: // Server told client that all clients finished loading a scene
          // Only executes on client
          if (_networkManager.IsClient)
          {
            /*m_ClientLoadingScreen.StopLoadingScreen();
            m_LoadingProgressManager.ResetLocalProgress();*/
          }

          break;
        case SceneEventType.Synchronize: // Server told client to start synchronizing scenes
        {
          // todo: this is a workaround that could be removed once MTT-3363 is done
          // Only executes on client that is not the host
          if (_networkManager.IsClient && !_networkManager.IsHost)
          {
            /* // unload all currently loaded additive scenes so that if we connect to a server with the same
             // main scene we properly load and synchronize all appropriate scenes without loading a scene
             // that is already loaded.
             UnloadAdditiveScenes();*/
          }

          break;
        }
        case SceneEventType.SynchronizeComplete: // Client told server that they finished synchronizing
          // Only executes on server
          if (_networkManager.IsServer)
          {
            // Send client RPC to make sure the client stops the loading screen after the server handles what it needs to after the client finished synchronizing, for example character spawning done server side should still be hidden by loading screen.
            /* StopLoadingScreenClientRpc(new ClientRpcParams
               { Send = new ClientRpcSendParams { TargetClientIds = new[] { sceneEvent.ClientId } } });*/
          }

          break;
      }
    }

    private IEnumerator LoadCoroutine(string name, LoadSceneMode mode)
    {
      yield return SceneManager.LoadSceneAsync(name, mode);
    }
  }
}