using Kulinaria.Tools.BattleTrier.Infrastructure.Services.Coroutines;
using Kulinaria.Tools.BattleTrier.Infrastructure.Services.Inputs;
using Unity.Services.Core;
using UnityEngine;
using Zenject;

namespace Kulinaria.Tools.BattleTrier.Infrastructure.Installers
{
  public class ToolInstaller : MonoInstaller, IInitializable
  {
    [SerializeField] private OldInputService _oldInputService;
    [SerializeField] private CoroutineRunner _coroutineRunner;
    
    public override void InstallBindings()
    {
      Container.BindInterfacesTo<ToolInstaller>().FromInstance(this).AsSingle();
      Container.Bind<IInputService>().To<OldInputService>().FromInstance(_oldInputService).AsSingle();
      Container.Bind<ICoroutineRunner>().To<CoroutineRunner>().FromInstance(_coroutineRunner).AsSingle();
    }

    public async void Initialize() => 
      await UnityServices.InitializeAsync();
  }
}