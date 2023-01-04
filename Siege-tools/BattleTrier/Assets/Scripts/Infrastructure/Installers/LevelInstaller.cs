using Kulinaria.Tools.BattleTrier.Level.Board;
using UnityEngine;
using Zenject;

namespace Kulinaria.Tools.BattleTrier.Infrastructure.Installers
{
  public class LevelInstaller : MonoInstaller, IInitializable
  {
    [SerializeField] private BoardSpawner _boardSpawner;

    public override void InstallBindings()
    {
      Debug.Log("<color=blue>Join Code :</color>" + LobbyInstaller.JoinCode);
      Container.Bind<IInitializable>().To<LevelInstaller>().FromInstance(this).AsSingle();
      Container.Bind<BoardSpawner>().FromInstance(_boardSpawner).AsSingle();
    }

    public void Initialize()
    {
      //Container.Resolve<BoardSpawner>().SpawnBoard();
    }
  }
}