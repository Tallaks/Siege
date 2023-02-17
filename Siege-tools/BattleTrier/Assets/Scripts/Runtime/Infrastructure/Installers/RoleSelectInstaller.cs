using Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Services.Coroutines;
using Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Services.Scenes;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Roles;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Roles.UI;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Utilities;
using Unity.Netcode;
using UnityEngine;
using Zenject;

namespace Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Installers
{
  public class RoleSelectInstaller : MonoInstaller, IInitializable
  {
    [SerializeField] private NetCodeHook _hook;
    [SerializeField] private RoleMediator _mediator;
    [SerializeField] private RoleSelectionService _selectionService;

    [Inject] private NetworkManager _networkManager;
    [Inject] private ICoroutineRunner _coroutineRunner;
    [Inject] private ISceneLoader _sceneLoader;

    public override void InstallBindings()
    {
      Container.Bind<IInitializable>().To<RoleSelectInstaller>().FromInstance(this).AsSingle();
      Container.Bind<NetCodeHook>().FromInstance(_hook).AsSingle();
      Container.Bind<RoleMediator>().FromInstance(_mediator).AsSingle();
      Container.Bind<RoleSelectionService>().FromInstance(_selectionService).AsSingle();
      if (_networkManager.IsServer)
        Container.Bind<RoleSelectionServer>().FromNew().AsSingle();

      Container.Bind<RoleSelectionClient>().FromNew().AsSingle();
    }

    public void Initialize()
    {
      if (_networkManager.IsServer)
        Container.Resolve<RoleSelectionServer>().Initialize();

      Container.Resolve<RoleSelectionClient>().Initialize();
    }
  }
}