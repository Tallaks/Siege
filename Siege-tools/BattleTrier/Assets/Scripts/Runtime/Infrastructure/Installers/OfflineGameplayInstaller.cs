using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Factory;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Placer;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Registry;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Selection.Placement;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.States;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.UI;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Data;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Roles;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Session;
using Sirenix.OdinInspector;
using Unity.Netcode;
using UnityEngine;
using Zenject;

namespace Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Installers
{
  public class OfflineGameplayInstaller : StaticInstaller, IInitializable
  {
    [SerializeField] [Required]
    private GameplayMediator _mediator;

    [Inject] private NetworkManager _networkManager;
    [Inject] private Session<SessionPlayerData> _session;

    public void Initialize()
    {
      Container.Resolve<StateMachine>().Initialize();
      if (_session.GetPlayerData(_networkManager.LocalClientId).HasValue)
      {
        if (_session.GetPlayerData(_networkManager.LocalClientId).Value.RoleState is RoleState.NotChosen
            or RoleState.None)
          Container.Resolve<StateMachine>().Enter<RoleSelectionState>();
        else
          Container.Resolve<StateMachine>().Enter<MapSelectionState>();
      }
      else
      {
        Container.Resolve<StateMachine>().Enter<RoleSelectionState>();
      }
    }

    public override void InstallBindings()
    {
      Container.Bind<IInitializable>().To<OfflineGameplayInstaller>().FromInstance(this).AsSingle();
      Container.Bind<ICharacterRegistry>().To<CharacterRegistryLocal>().FromNew().AsSingle();
      Container.Bind<GameplayMediator>().FromInstance(_mediator).AsSingle();
      Container.Bind<StateMachine>().FromNew().AsSingle();
      Container.Bind<ICharacterFactory>().To<CharacterFactory>().FromNew().AsSingle();
      Container.Bind<IEnemyFactory>().To<EnemyFactory>().FromNew().AsSingle();
      Container.Bind<IPlacementSelection>().To<PlacementSelection>().FromNew().AsSingle();
      Container.Bind<ICharacterPlacer>().To<CharacterPlacer>().FromNew().AsSingle();
    }
  }
}