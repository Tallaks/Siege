using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Registry;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.StateMachine;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.UI;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Gameplay;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Roles;
using Sirenix.OdinInspector;
using Unity.Netcode;
using UnityEngine;
using Zenject;

namespace Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Installers
{
  public class GameplayInstaller : MonoInstaller, IInitializable
  {
    [SerializeField, Required] private GameplayMediator _mediator;
    [SerializeField, Required] private MapSelectionNetwork _mapSelectionNetwork;
    [SerializeField, Required] private CharacterSelectionNetwork _characterSelectionNetwork;
    [SerializeField, Required] private NetworkCharacterRegistry _characterRegistry;

    [Inject] private NetworkManager _networkManager;

    public void Initialize()
    {
      Container.Resolve<StateMachine>().Initialize();
      Container.Resolve<StateMachine>().Enter<MapSelectionState>();
    }

    public override void InstallBindings()
    {
      Container.
        Bind<IInitializable>().
        To<GameplayInstaller>().
        FromInstance(this).
        AsSingle();

      Container.
        Bind<ICharacterRegistry>().
        To<NetworkCharacterRegistry>().
        FromInstance(_characterRegistry).
        AsSingle();

      Container.
        Bind<GameplayMediator>().
        FromInstance(_mediator).
        AsSingle();

      Container.
        Bind<MapSelectionNetwork>().
        FromInstance(_mapSelectionNetwork).
        AsSingle();

      Container.
        Bind<CharacterSelectionNetwork>().
        FromInstance(_characterSelectionNetwork).
        AsSingle();

      Container.
        Bind<StateMachine>().
        FromNew().
        AsSingle();

      Container.
        Bind<RoleBase>().
        FromInstance(_networkManager.LocalClient.PlayerObject.GetComponent<RoleBase>()).
        AsSingle();
    }
  }
}