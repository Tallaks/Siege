using System.Collections;
using Kulinaria.Siege.Runtime.Infrastructure.Constants;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

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
			
			Debug.Log(SceneManager.GetActiveScene().name);
			
			// Assert
			Assert.IsTrue(asyncOperation.isDone);
			Assert.IsTrue(SceneManager.GetActiveScene().name == SceneNames.BattleScene);
			Assert.IsTrue(SceneManager.GetActiveScene().name != SceneNames.BootScene);
		}

		[UnityTest]
		public IEnumerator WhenBootSceneLoaded_ThenBattleSceneLoadedAfterIt()
		{
			yield return LoadBootScene();
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