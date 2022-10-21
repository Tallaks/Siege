using System.Collections;
using Kulinaria.Siege.Runtime.Infrastructure.Constants;
using Kulinaria.Siege.Runtime.Infrastructure.ZenjectInstallers;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Kulinaria.Siege.Tests.Scenes
{
	[TestFixture]
	public class BattleSceneTests
	{
		[UnitySetUp]
		public IEnumerator SetUp()
		{
			var gameInstaller = Object.FindObjectOfType<GameInstaller>();
			gameInstaller?.Initialize();
			yield break;
		}

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
			
			yield return LoadBootScene();
			Object.FindObjectOfType<GameInstaller>()?.Initialize();
			yield return new WaitForSeconds(1);
			
			Assert.IsTrue(SceneManager.GetActiveScene().name == SceneNames.BattleScene);
			Assert.IsTrue(SceneManager.GetActiveScene().name != SceneNames.BootScene);
		}

		private AsyncOperation LoadBootScene() => 
			SceneManager.LoadSceneAsync(SceneNames.BootScene, LoadSceneMode.Single);

		private AsyncOperation LoadBattleScene() => 
			SceneManager.LoadSceneAsync(SceneNames.BattleScene);
	}
}