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
			// Assert
			Assert.IsTrue(asyncOperation.isDone);
			Assert.IsTrue(SceneManager.GetActiveScene().name == SceneNames.BattleScene);
			Assert.IsTrue(SceneManager.GetActiveScene().name != SceneNames.BootScene);
		}

		private AsyncOperation LoadBattleScene() => 
			SceneManager.LoadSceneAsync(SceneNames.BattleScene);
	}
}