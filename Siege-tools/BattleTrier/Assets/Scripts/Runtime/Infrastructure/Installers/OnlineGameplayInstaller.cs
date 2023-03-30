using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Network;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Maps.Selection.Network;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Roles;
using Sirenix.OdinInspector;
using Unity.Netcode;
using UnityEngine;
using Zenject;

namespace Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Installers
{
  public class OnlineGameplayInstaller : MonoInstaller
  {
    [SerializeField, Required] private MapSelectionNetwork _mapSelectionNetwork;
    [SerializeField, Required] private CharacterSelectionNetwork _characterSelectionNetwork;

    [Inject] private NetworkManager _networkManager;

    public override void InstallBindings()
    {
      Container.
        Bind<MapSelectionNetwork>().
        FromInstance(_mapSelectionNetwork).
        AsSingle();

      Container.
        Bind<CharacterSelectionNetwork>().
        FromInstance(_characterSelectionNetwork).
        AsSingle();

      Container.
        Bind<RoleBase>().
        FromInstance(_networkManager.LocalClient.PlayerObject.GetComponent<RoleBase>()).
        AsSingle();
    }
  }
}