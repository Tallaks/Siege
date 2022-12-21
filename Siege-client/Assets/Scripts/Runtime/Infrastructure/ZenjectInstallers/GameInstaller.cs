using Kulinaria.Siege.Runtime.Debugging.Logging;
using Kulinaria.Siege.Runtime.Infrastructure.Assets;
using Kulinaria.Siege.Runtime.Infrastructure.Constants;
using Kulinaria.Siege.Runtime.Infrastructure.Coroutines;
using Kulinaria.Siege.Runtime.Infrastructure.Inputs;
using Kulinaria.Siege.Runtime.Infrastructure.Scenes;
using UnityEngine;
using Zenject;

namespace Kulinaria.Siege.Runtime.Infrastructure.ZenjectInstallers
{
	public class GameInstaller : MonoInstaller, IInitializable
	{
		public static bool Testing = false;

		public void Initialize()
		{
			UnityLoggerService.DefaultPriority = 10;
			Container.Resolve<ILoggerService>().Log("Game Services Initialized", LoggerLevel.Application);
			if (!Testing)
				Container.Resolve<ISceneLoader>().LoadSceneAsync(SceneNames.BattleScene, () =>
					Container.Resolve<ILoggerService>().Log("Battle scene loaded", LoggerLevel.Application));
		}

		public override void InstallBindings()
		{
			Container
				.Bind<IInitializable>()
				.To<GameInstaller>()
				.FromInstance(this)
				.AsSingle();

			Container
				.Bind<ILoggerService>()
				.To<UnityLoggerService>()
				.FromNew()
				.AsSingle();

			Container
				.Bind<ICoroutineRunner>()
				.To<CoroutineRunner>()
				.FromMethod(() => new GameObject().AddComponent<CoroutineRunner>())
				.AsSingle();

			Container
				.Bind<IAssetsProvider>()
				.To<ResourcesAssetsProvider>()
				.FromNew()
				.AsSingle();

			Container
				.Bind<ISceneLoader>()
				.To<SceneLoader>()
				.AsSingle();

			Container
				.Bind<IInputService>()
				.To<InputService>()
				.FromComponentOn(gameObject)
				.AsSingle();
		}
	}
}