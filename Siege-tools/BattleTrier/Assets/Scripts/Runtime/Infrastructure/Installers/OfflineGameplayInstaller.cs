using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Factory;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Selection;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.States;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.UI;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Installers
{
  public class OfflineGameplayInstaller : MonoInstaller, IInitializable
  {
    [SerializeField, Required] private GameplayMediator _mediator;

    public void Initialize()
    {
      Container.Resolve<StateMachine>().Initialize();
      Container.Resolve<StateMachine>().Enter<MapSelectionState>();
    }

    public override void InstallBindings()
    {
      Container.
        Bind<IInitializable>().
        To<OfflineGameplayInstaller>().
        FromInstance(this).
        AsSingle();

      Container.
        Bind<ICharacterSelection>().
        To<LocalCharacterSelection>().
        FromNew().
        AsSingle();

      Container.
        Bind<GameplayMediator>().
        FromInstance(_mediator).
        AsSingle();

      Container.
        Bind<StateMachine>().
        FromNew().
        AsSingle();

      Container.
        Bind<ICharacterFactory>().
        To<CharacterFactory>().
        FromNew().
        AsSingle();
    }
  }
}