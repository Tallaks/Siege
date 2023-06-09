using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Factory;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Placer;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Registry;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Selection.Placement;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.States;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.UI;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Installers
{
  public class OfflineGameplayInstaller : StaticInstaller, IInitializable
  {
    [SerializeField, Required] private GameplayMediator _mediator;

    public void Initialize()
    {
      Container.Resolve<StateMachine>().Initialize();
      Container.Resolve<StateMachine>().Enter<MapSelectionState>();
    }

    public override void InstallBindings()
    {
      Container.Bind<IInitializable>().To<OfflineGameplayInstaller>().FromInstance(this).AsSingle();
      Container.Bind<ICharacterRegistry>().To<CharacterRegistryLocal>().FromNew().AsSingle();
      Container.Bind<GameplayMediator>().FromInstance(_mediator).AsSingle();
      Container.Bind<StateMachine>().FromNew().AsSingle();
      Container.Bind<ICharacterFactory>().To<CharacterFactory>().FromNew().AsSingle();
      Container.Bind<IPlacementSelection>().To<PlacementSelection>().FromNew().AsSingle();
      Container.Bind<ICharacterPlacer>().To<CharacterPlacer>().FromNew().AsSingle();
    }
  }
}