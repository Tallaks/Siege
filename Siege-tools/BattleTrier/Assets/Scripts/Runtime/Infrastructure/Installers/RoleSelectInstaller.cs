using Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Services.Coroutines;
using Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Services.Scenes;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Utilities;
using Kulinaria.Tools.BattleTrier.Runtime.Roles;
using Unity.Netcode;
using UnityEngine;
using Zenject;

namespace Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Installers
{
  public class RoleSelectInstaller : MonoInstaller, IInitializable
  {
    [SerializeField] private NetCodeHook _hook;

    [Inject] private NetworkManager _networkManager;
    [Inject] private ICoroutineRunner _coroutineRunner;
    [Inject] private ISceneLoader _sceneLoader;
    
    public override void InstallBindings()
    {
      Container.Bind<IInitializable>().To<RoleSelectInstaller>().FromInstance(this).AsSingle();
      Container.Bind<NetCodeHook>().FromInstance(_hook).AsSingle();
      Container.Bind<RoleSelectionBase>().FromMethod(() => 
        new RoleSelectionFactory(_coroutineRunner, _sceneLoader, _networkManager, _hook).Create()).AsSingle();
      Container.Bind<NetworkRoleSelection>().FromNew().AsSingle();
    }

    public void Initialize()
    {
      Container.Resolve<RoleSelectionBase>().Initialize();
    }
  }
}