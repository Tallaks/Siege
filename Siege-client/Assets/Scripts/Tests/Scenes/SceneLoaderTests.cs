using System.Collections;
using Kulinaria.Siege.Runtime.Infrastructure.Constants;
using Kulinaria.Siege.Runtime.Infrastructure.Coroutines;
using Kulinaria.Siege.Runtime.Infrastructure.Scenes;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using Zenject;

namespace Kulinaria.Siege.Tests.Scenes
{
	[TestFixture]
	public class SceneLoaderTests : ZenjectIntegrationTestFixture
	{
		[UnityTest]
		public IEnumerator WhenSceneLoaderBound_ThenItIsNotNull()
		{
			var coroutineRunner = new GameObject().AddComponent<CoroutineRunner>();
			
			PreInstall();

			Container
				.Bind<ICoroutineRunner>()
				.To<CoroutineRunner>()
				.FromInstance(coroutineRunner)
				.AsSingle();
			
			Container
				.Bind<ISceneLoader>()
				.To<SceneLoader>()
				.AsSingle();
			
			PostInstall();

			Assert.NotNull(Container.Resolve<ICoroutineRunner>());
			Assert.NotNull(Container.Resolve<ISceneLoader>());
			
			yield break;
		}
		
		[UnityTest]
		public IEnumerator WhenBootSceneLoadedWithSceneLoader_ThenItLoads()
		{
			var coroutineRunner = new GameObject().AddComponent<CoroutineRunner>();
			
			PreInstall();

			Container
				.Bind<ICoroutineRunner>()
				.To<CoroutineRunner>()
				.FromInstance(coroutineRunner)
				.AsSingle();
			
			Container
				.Bind<ISceneLoader>()
				.To<SceneLoader>()
				.AsSingle();
			
			PostInstall();

			Container.Resolve<ISceneLoader>().LoadSceneAsync(SceneNames.BattleScene, () => 
				Assert.IsTrue(SceneManager.GetActiveScene().name == SceneNames.BattleScene));

			yield return new WaitForSeconds(2);
		}
	}
}