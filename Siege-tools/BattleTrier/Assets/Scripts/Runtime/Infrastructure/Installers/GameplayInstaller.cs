using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.StateMachine;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.UI;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Gameplay;
using UnityEngine;
using Zenject;

namespace Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Installers
{
  public class GameplayInstaller : MonoInstaller, IInitializable
  {
    [SerializeField] private GameplayMediator _mediator;
    [SerializeField] private MapSelectionBehaviour _mapSelectionNetwork;

    public void Initialize()
    {
      Container.Resolve<StateMachine>().Initialize();
      Container.Resolve<StateMachine>().Enter<MapSelectionState>();
    }

    public override void InstallBindings()
    {
      Container.Bind<IInitializable>().To<GameplayInstaller>().FromInstance(this).AsSingle();
      Container.Bind<GameplayMediator>().FromInstance(_mediator).AsSingle();
      Container.Bind<MapSelectionBehaviour>().FromInstance(_mapSelectionNetwork).AsSingle();
      Container.Bind<StateMachine>().FromNew().AsSingle();
    }
  }
}