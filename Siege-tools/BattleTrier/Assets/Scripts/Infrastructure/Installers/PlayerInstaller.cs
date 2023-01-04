using Kulinaria.Tools.BattleTrier.Level.UI;
using Kulinaria.Tools.BattleTrier.Network.Authentication;
using Unity.Netcode;
using UnityEngine;
using Zenject;

namespace Kulinaria.Tools.BattleTrier.Infrastructure.Installers
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
        return;
      }
    }

    public override void InstallBindings()
    {
      Container.Bind<IInitializable>().To<PlayerInstaller>().FromInstance(this).AsSingle();
    }
  }
}