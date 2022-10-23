using System.Collections;
using Kulinaria.Siege.Runtime.Gameplay.Battle;
using Kulinaria.Siege.Runtime.Infrastructure.Constants;
using Kulinaria.Siege.Runtime.Infrastructure.ZenjectInstallers;
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

		[UnityTest]
		public IEnumerator WhenBattleSceneLoaded_ThenCameraMoverComponentExists()
		{
			yield return LoadBootScene();
			Object.FindObjectOfType<GameInstaller>()?.Initialize();
			yield return new WaitForSeconds(0.5f);

			Assert.NotNull(Object.FindObjectOfType<CameraMover>());
		}
		
		private AsyncOperation LoadBootScene() => 
			SceneManager.LoadSceneAsync(SceneNames.BootScene, LoadSceneMode.Single);
	}
}