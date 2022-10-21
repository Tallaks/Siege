using Kulinaria.Siege.Runtime.Infrastructure.Constants;
using Kulinaria.Siege.Runtime.Infrastructure.Coroutines;
using Kulinaria.Siege.Runtime.Infrastructure.Inputs;
using Kulinaria.Siege.Runtime.Infrastructure.Scenes;
using Zenject;

namespace Kulinaria.Siege.Runtime.Infrastructure.ZenjectInstallers
{
	public class GameInstaller : MonoInstaller, IInitializable
	{
		public override void InstallBindings()
		{
			Container
				.Bind<IInitializable>()
				.To<GameInstaller>()
				.FromInstance(this)
				.AsSingle();
			
			Container
				.Bind<ICoroutineRunner>()
				.To<CoroutineRunner>()
				.FromMethod(FindObjectOfType<CoroutineRunner>)
				.AsSingle();
				
			Container
				.Bind<ISceneLoader>()
				.To<SceneLoader>()
				.AsSingle();
			
			Container
				.Bind<IInputService>()
				.To<InputService>()
				.FromNewComponentOnRoot()
				.AsSingle();
		}

		public void Initialize() => 
			Container.Resolve<ISceneLoader>().LoadSceneAsync(SceneNames.BattleScene);
	}
}