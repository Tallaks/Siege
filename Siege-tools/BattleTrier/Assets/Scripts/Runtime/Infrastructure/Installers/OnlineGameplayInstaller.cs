using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Network;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Maps;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Maps.Selection.Network;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Network;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Roles;
using Sirenix.OdinInspector;
using Unity.Netcode;
using UnityEngine;
using Zenject;

namespace Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Installers
{
  public class OnlineGameplayInstaller : StaticInstaller
  {
    [SerializeField] [Required] [SceneObjectsOnly]
    private CharacterRegistryNetwork _characterRegistryNetwork;

    [SerializeField] [Required] [SceneObjectsOnly]
    private CharacterSelectionNetwork _characterSelectionNetwork;

    [SerializeField] [Required] [SceneObjectsOnly]
    private MapSelectionNetwork _mapSelectionNetwork;

    [SerializeField] [Required] [SceneObjectsOnly]
    private PlacementStateNetwork _placementStateNetwork;

    [SerializeField] [Required] [SceneObjectsOnly]
    private MapNetwork _mapNetwork;

    [Inject] private NetworkManager _networkManager;

    public override void InstallBindings()
    {
      Container.Bind<MapSelectionNetwork>().FromInstance(_mapSelectionNetwork).AsSingle();
      Container.Bind<CharacterSelectionNetwork>().FromInstance(_characterSelectionNetwork).AsSingle();
      Container.Bind<CharacterRegistryNetwork>().FromInstance(_characterRegistryNetwork).AsSingle();
      Container.Bind<RoleBase>().FromInstance(_networkManager.LocalClient.PlayerObject.GetComponent<RoleBase>())
        .AsSingle();
      Container.Bind<PlacementStateNetwork>().FromInstance(_placementStateNetwork).AsSingle();
      Container.Bind<MapNetwork>().FromInstance(_mapNetwork).AsSingle();
    }
  }
}