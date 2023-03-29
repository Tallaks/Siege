using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Registry;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.StateMachine;
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
        Bind<ICharacterRegistry>().
        To<LocalCharacterRegistry>().
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
    }
  }
}