using Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Services.Coroutines;
using Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Services.Scenes;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Utilities;
using Unity.Netcode;

namespace Kulinaria.Tools.BattleTrier.Runtime.Roles
{
  public class RoleSelectionFactory
  {
    private readonly NetworkManager _networkManager;
    private readonly NetCodeHook _hook;
    private readonly ICoroutineRunner _coroutineRunner;
    private readonly ISceneLoader _sceneLoader;

    public RoleSelectionFactory(ICoroutineRunner coroutineRunner, ISceneLoader sceneLoader, NetworkManager networkManager, NetCodeHook hook)
    {
      _sceneLoader = sceneLoader;
      _coroutineRunner = coroutineRunner;
      _hook = hook;
      _networkManager = networkManager;
    }

    public RoleSelectionBase Create()
    {
      if (_networkManager.IsServer)
        return new RoleSelectionServer(_coroutineRunner, _sceneLoader, _networkManager, _hook);
      else
        return new RoleSelectionClient();
    }
  }
}