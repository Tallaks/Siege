using UnityEngine;
using Zenject;

namespace Kulinaria.Tools.BattleTrier.Infrastructure.Installers
{
  public class LevelInstaller : MonoInstaller, ITickable
  {
    public override void InstallBindings()
    {
      Debug.Log(LobbyInstaller.JoinCode);
      Container.Bind<ITickable>().To<LevelInstaller>().FromInstance(this).AsSingle();
    }

    public void Tick()
    {
    }
  }
}