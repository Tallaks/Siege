using System;
using Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Services.Applications;
using Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Services.Coroutines;
using Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Services.Data;
using Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Services.Inputs;
using Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Services.Scenes;
using QFSW.QC;
using UnityEngine.SceneManagement;
using Zenject;

namespace Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Installers
{
  public class ToolInstaller : MonoInstaller, IInitializable
  {
    public void Initialize()
    {
      SceneManager.LoadSceneAsync("MainMenu");
      Container.Resolve<QuantumConsole>().Deactivate();
      Container.Resolve<IInputService>().OnConsoleCalled += () => Container.Resolve<QuantumConsole>().Toggle();
    }

    public override void InstallBindings()
    {
      Container
        .BindInterfacesTo<ToolInstaller>()
        .FromInstance(this)
        .AsSingle();

      Container
        .Bind<IInputService>()
        .To<OldInputService>()
        .FromInstance(FindObjectOfType<OldInputService>())
        .AsSingle();

      Container
        .Bind(typeof(ICoroutineRunner), typeof(IUpdateRunner))
        .To<AggregateRunner>()
        .FromInstance(FindObjectOfType<AggregateRunner>())
        .AsSingle();

      Container
        .Bind<QuantumConsole>()
        .FromInstance(FindObjectOfType<QuantumConsole>())
        .AsSingle();

      Container
        .Bind<ISceneLoader>()
        .To<SceneLoader>()
        .FromNew()
        .AsSingle();

      Container
        .Bind(typeof(IApplicationService), typeof(IInitializable), typeof(IDisposable))
        .To<ApplicationService>()
        .FromNew()
        .AsSingle();

      Container
        .Bind(typeof(IStaticDataProvider), typeof(IInitializable))
        .To<StaticDataProvider>()
        .FromNew()
        .AsSingle();
    }
  }
}