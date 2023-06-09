using Kulinaria.Tools.BattleTrier.Runtime.Network.Roles;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Roles.UI;
using UnityEngine;
using Zenject;

namespace Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Installers
{
  public class RoleSelectInstaller : StaticInstaller, IInitializable
  {
    [SerializeField] private RoleSelectionClient _client;
    [SerializeField] private RoleMediator _mediator;
    [SerializeField] private RoleSelectionService _selectionService;
    [SerializeField] private RoleSelectionServer _server;

    public void Initialize() =>
      Container.Resolve<RoleMediator>().Initialize();

    public override void InstallBindings()
    {
      Container.Bind<IInitializable>().To<RoleSelectInstaller>().FromInstance(this).AsSingle();
      Container.Bind<RoleMediator>().FromInstance(_mediator).AsSingle();
      Container.Bind<RoleSelectionService>().FromInstance(_selectionService).AsSingle();
      Container.Bind<RoleSelectionClient>().FromInstance(_client).AsSingle();
      Container.Bind<RoleSelectionServer>().FromInstance(_server).AsSingle();
    }
  }
}