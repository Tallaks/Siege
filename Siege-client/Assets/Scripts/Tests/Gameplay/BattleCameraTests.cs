using System.Collections;
using Kulinaria.Siege.Runtime.Infrastructure.Constants;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Kulinaria.Siege.Tests.Gameplay
{
	[TestFixture]
	public class BattleCameraTests
	{
		[UnityTest]
		public IEnumerator WhenBattleSceneLoaded_ThenCameraExists()
		{
			yield return LoadBootScene();
			yield return new WaitForSeconds(0.5f);

			Assert.NotNull(Object.FindObjectOfType<Camera>());
		}
		
		private AsyncOperation LoadBootScene() => 
			SceneManager.LoadSceneAsync(SceneNames.BootScene, LoadSceneMode.Single);
	}
}