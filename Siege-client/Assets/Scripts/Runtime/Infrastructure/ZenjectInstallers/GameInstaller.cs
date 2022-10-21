using Kulinaria.Siege.Runtime.Infrastructure.Coroutines;
using Kulinaria.Siege.Runtime.Infrastructure.Inputs;
using Kulinaria.Siege.Runtime.Infrastructure.Scenes;
using Zenject;

namespace Kulinaria.Siege.Runtime.Infrastructure.ZenjectInstallers
{
	public class GameInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
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
	}
}