using Kulinaria.Tools.BattleTrier.Runtime.Level.UI;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Authentication;
using Unity.Netcode;
using UnityEngine;
using Zenject;

namespace Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Installers
{
  public class PlayerInstaller : MonoInstaller, IInitializable
  {
    [SerializeField] private PlayerService _playerService;
    [SerializeField] private LevelMediator _levelMediator;

    public void Initialize()
    {
      if (NetworkManager.Singleton.IsHost)
      {
        _playerService.RegisterFirstPlayer();
        _levelMediator.ShowMapChoice();
        _levelMediator.InitMaps();
        return;
      }

      _levelMediator.ShowRoleChoice();
    }

    public override void InstallBindings()
    {
      Container.Bind<IInitializable>().To<PlayerInstaller>().FromInstance(this).AsSingle();
    }
  }
}