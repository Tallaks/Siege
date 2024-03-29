using System.Collections;
using Kulinaria.Siege.Runtime.Infrastructure.Constants;
using Kulinaria.Siege.Runtime.Infrastructure.Coroutines;
using Kulinaria.Siege.Runtime.Infrastructure.ZenjectInstallers;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using Zenject;

namespace Kulinaria.Siege.Tests.Scenes
{
	[TestFixture]
	public class BattleSceneTests
	{
		[UnityTest]
		public IEnumerator WhenBattleSceneLoadCalled_ThenItLoads()
		{
			// Arrange
			// Act
			AsyncOperation asyncOperation = LoadBattleScene();
			yield return asyncOperation;

			// Assert
			Assert.IsTrue(asyncOperation.isDone);
			Assert.IsTrue(SceneManager.GetActiveScene().name == SceneNames.BattleScene);
			Assert.IsTrue(SceneManager.GetActiveScene().name != SceneNames.BootScene);
		}

		[UnityTest]
		public IEnumerator WhenBootSceneLoaded_ThenBattleSceneLoadedAfterIt()
		{
			ApplicationInstaller.Testing = false;
			yield return LoadBootScene();
			Object.FindObjectOfType<ApplicationInstaller>()?.Initialize();
			yield return new WaitForSeconds(1);

			Assert.IsTrue(SceneManager.GetActiveScene().name == SceneNames.BattleScene);
			Assert.IsTrue(SceneManager.GetActiveScene().name != SceneNames.BootScene);
			ApplicationInstaller.Testing = true;
		}

		[UnityTearDown]
		public IEnumerator TearDown()
		{
			var resolvedRunner = Object.FindObjectOfType<ProjectContext>().Container.Resolve<ICoroutineRunner>();

			foreach (CoroutineRunner runner in Object.FindObjectsOfType<CoroutineRunner>())
			{
				if (ReferenceEquals(runner, resolvedRunner))
					continue;

				Object.Destroy(runner.gameObject);
			}

			yield break;
		}

		private AsyncOperation LoadBootScene()
		{
			return SceneManager.LoadSceneAsync(SceneNames.BootScene, LoadSceneMode.Single);
		}

		private AsyncOperation LoadBattleScene()
		{
			return SceneManager.LoadSceneAsync(SceneNames.BattleScene);
		}
	}
}