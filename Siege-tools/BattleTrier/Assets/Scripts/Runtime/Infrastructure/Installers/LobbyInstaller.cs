using Kulinaria.Tools.BattleTrier.Runtime.UI.LobbyScene;
using UnityEngine;
using Zenject;

namespace Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Installers
{
  public class LobbyInstaller : MonoInstaller, IInitializable
  {
    [SerializeField] private LobbyMediator _lobbyMediator;
    
    public void Initialize() => Container.Resolve<LobbyMediator>().Initialize();

    public override void InstallBindings()
    {
      Container.Bind<IInitializable>().To<LobbyInstaller>().FromInstance(this).AsSingle();
      Container.Bind<LobbyMediator>().FromInstance(_lobbyMediator).AsSingle();
    }
  }
}