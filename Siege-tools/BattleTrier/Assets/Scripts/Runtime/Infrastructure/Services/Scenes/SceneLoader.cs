using System.Collections;
using Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Services.Coroutines;
using Unity.Netcode;
using UnityEngine.SceneManagement;

namespace Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Services.Scenes
{
  public class SceneLoader : ISceneLoader
  {
    private NetworkManager _networkManager;
    private ICoroutineRunner _coroutineRunner;

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
        {
          if (_networkManager.IsServer)
          {
            // If is active server and NetworkManager uses scene management, load scene using NetworkManager's SceneManager
            _networkManager.SceneManager.LoadScene(name, mode);
          }
        }
      }
      else
      {
        // Load using SceneManager
        if (mode == LoadSceneMode.Single)
          _coroutineRunner.StartCoroutine(LoadCoroutine(name, mode));
      }

    }

    private IEnumerator LoadCoroutine(string name, LoadSceneMode mode)
    {
      yield return SceneManager.LoadSceneAsync(name, mode);
    }
  }
}