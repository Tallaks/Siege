using Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Services.Applications;
using Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Services.Coroutines;
using Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Services.Inputs;
using Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Services.Scenes;
using UnityEngine.SceneManagement;
using Zenject;

namespace Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Installers
{
  public class ToolInstaller : MonoInstaller, IInitializable
  {
    public override void InstallBindings()
    {
      Container.BindInterfacesTo<ToolInstaller>().FromInstance(this).AsSingle();
      Container.Bind<IInputService>().To<OldInputService>().FromInstance(FindObjectOfType<OldInputService>()).
        AsSingle();
      Container.Bind(typeof(ICoroutineRunner), typeof(IUpdateRunner)).To<AggregateRunner>().FromInstance(FindObjectOfType<AggregateRunner>()).
        AsSingle();
      Container.Bind<ISceneLoader>().To<SceneLoader>().FromNew().AsSingle();
      Container.Bind<IApplicationService>().To<ApplicationService>().FromNew().AsSingle();
    }

    public void Initialize()
    {
      Container.Resolve<IApplicationService>().Initialize();
      SceneManager.LoadSceneAsync("MainMenu");
    }
  }
}