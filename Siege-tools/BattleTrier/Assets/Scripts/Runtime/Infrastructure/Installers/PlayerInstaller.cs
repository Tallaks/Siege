using Kulinaria.Tools.BattleTrier.Runtime.Level.UI;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Authentication;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Data;
using Unity.Netcode;
using UnityEngine;
using Zenject;

namespace Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Installers
{
  public class PlayerInstaller : MonoInstaller, IInitializable
  {
    [SerializeField] private PlayerService _playerServicePrefab;
    [SerializeField] private LevelMediator _levelMediator;

    public void Initialize()
    {
      if (NetworkManager.Singleton.IsHost)
      {
        PlayerService playerService = Instantiate(_playerServicePrefab);
        playerService.NetworkObject.Spawn();
        playerService.RegisterPlayerServerRpc(Role.FirstPlayer);
        _levelMediator.ShowMapChoice();
        _levelMediator.InitMaps();
        return;
      }

      _levelMediator.InitRoles();
      _levelMediator.ShowRoleChoice();
    }

    public override void InstallBindings()
    {
      Debug.Log("Binding");
      Container.Bind<IInitializable>().To<PlayerInstaller>().FromInstance(this).AsSingle();
      Container.Bind<LevelMediator>().FromInstance(_levelMediator).AsSingle();
    }
  }
}