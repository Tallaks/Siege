using Kulinaria.Tools.BattleTrier.Infrastructure.Services.Inputs;
using UnityEngine;
using Zenject;

namespace Kulinaria.Tools.BattleTrier.Infrastructure
{
  public class ToolInstaller : MonoInstaller, ITickable
  {
    [SerializeField] private OldInputService _oldInputService;
    
    public override void InstallBindings()
    {
      Container.Bind<ITickable>().To<ToolInstaller>().FromInstance(this).AsSingle();
      Container.Bind<IInputService>().To<OldInputService>().FromInstance(_oldInputService).AsSingle();
    }

    public void Tick()
    {
      Debug.Log(Container.Resolve<IInputService>().MoveDirection);
    }
  }
}