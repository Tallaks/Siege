using System.Collections;
using System.Linq;
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
	public class BootSceneTests
	{
		private GameInstaller GameInstaller => Object.FindObjectOfType<GameInstaller>();
		private ProjectContext ProjectContext => Object.FindObjectOfType<ProjectContext>();

		[UnityTest]
		public IEnumerator WhenBootSceneLoadCalled_ThenItLoads()
		{
			// Arrange
			// Act
			AsyncOperation asyncOperation = LoadBootScene();
			yield return asyncOperation;
			// Assert
			Assert.IsTrue(asyncOperation.isDone);
			Assert.IsTrue(SceneManager.GetActiveScene().name == SceneNames.BootScene);
		}

		[Test]
		public void WhenProjectContextResourceLoadingCalled_ThenProjectContextLoads()
		{
			// Arrange
			// Act
			var projectContextPrefab = Resources.Load<ProjectContext>("ProjectContext");
			// Assert
			Assert.IsNotNull(projectContextPrefab);
		}

		[UnityTest]
		public IEnumerator WhenBootSceneLoaded_ThenProjectContextAndGameInstallerExist()
		{
			// Arrange
			yield return LoadBootSceneAndWait();

			// Assert
			Assert.IsNotNull(ProjectContext);
			Assert.IsNotNull(GameInstaller);
		}

		[UnityTest]
		public IEnumerator WhenBootSceneLoaded_ThenProjectContextContainsGameInstallerInMonoInstallers()
		{
			// Arrange
			yield return LoadBootSceneAndWait();

			// Assert
			Assert.IsTrue(ProjectContext.Installers.Contains(GameInstaller));
		}

		[UnityTest]
		public IEnumerator WhenBootSceneLoaded_ThenThereIsOnlyOneGameInstaller()
		{
			// Arrange
			yield return LoadBootSceneAndWait();

			// Assert
			Assert.AreEqual(1, Object.FindObjectsOfType<GameInstaller>().Length);
		}

		[UnityTest]
		public IEnumerator WhenBootSceneLoaded_ThenCoroutineRunnerExists()
		{
			yield return LoadBootScene();
			Assert.NotNull(Object.FindObjectOfType<CoroutineRunner>());
		}

		[UnityTearDown]
		public IEnumerator TearDown()
		{
			ICoroutineRunner resolvedRunner = null;
			if(Object.FindObjectOfType<ProjectContext>() != null)
			{
				resolvedRunner = Object.FindObjectOfType<ProjectContext>().Container.Resolve<ICoroutineRunner>();
			}

			foreach (CoroutineRunner runner in Object.FindObjectsOfType<CoroutineRunner>())
			{
				if (runner == resolvedRunner)
					continue;

				Object.Destroy(runner.gameObject);
			}

			yield break;
		}

		private IEnumerator LoadBootSceneAndWait()
		{
			yield return LoadBootScene();
			yield return new WaitForSeconds(0.5f);
		}

		private AsyncOperation LoadBootScene() => SceneManager.LoadSceneAsync(SceneNames.BootScene, LoadSceneMode.Single);
	}
}