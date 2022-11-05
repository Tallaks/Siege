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
			if (!Testing)
				Container.Resolve<ISceneLoader>().LoadSceneAsync(SceneNames.BattleScene);
		}

		public override void InstallBindings()
		{
			Container.Bind<IInitializable>().To<GameInstaller>().FromInstance(this).AsSingle();

			Container.Bind<ICoroutineRunner>().To<CoroutineRunner>().
				FromMethod(() => new GameObject().AddComponent<CoroutineRunner>()).AsSingle();

			Container.Bind<Camera>().FromInstance(Camera.main).AsSingle();

			Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();

			Container.Bind<IInputService>().To<InputService>().FromComponentOn(gameObject).AsSingle();
		}
	}
}