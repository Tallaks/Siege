using System.Collections;
using Kulinaria.Siege.Runtime.Gameplay.Battle;
using Kulinaria.Siege.Runtime.Infrastructure.Constants;
using Kulinaria.Siege.Runtime.Infrastructure.Coroutines;
using Kulinaria.Siege.Runtime.Infrastructure.Inputs;
using Kulinaria.Siege.Runtime.Infrastructure.ZenjectInstallers;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using Zenject;

namespace Kulinaria.Siege.Tests.Gameplay
{
	[TestFixture]
	public class BattleCameraTests
	{
		[UnitySetUp]
		public IEnumerator SetUp()
		{
			yield return LoadBootScene();
			yield return new WaitForSeconds(0.5f);
			
			var resolvedInputService = Object.FindObjectOfType<Runtime.Infrastructure.Inputs.InputService>();
			Object.DontDestroyOnLoad(resolvedInputService.gameObject);
		}
		
		[UnityTest]
		public IEnumerator WhenBattleSceneLoaded_ThenCameraExists()
		{
			Assert.NotNull(Object.FindObjectOfType<Camera>());
			yield break;
		}

		[UnityTest]
		public IEnumerator WhenBattleSceneLoaded_ThenCameraMoverComponentExists()
		{
			yield return LoadBootScene();
			Object.FindObjectOfType<GameInstaller>()?.Initialize();
			yield return new WaitForSeconds(0.5f);

			Assert.NotNull(Object.FindObjectOfType<CameraMover>());
		}
		
		[UnityTest]
		public IEnumerator WhenMovementForwardCommandSent_ThenCameraMovesForward()
		{
			yield return LoadBootScene();
			Object.FindObjectOfType<GameInstaller>()?.Initialize();
			yield return new WaitForSeconds(0.5f);

			var camera = Object.FindObjectOfType<CameraMover>();
			float startPosZ = camera.transform.position.z;
			
			while (true)
			{
				yield return null;
				if(camera.transform.position.z > startPosZ)
					Assert.Pass();
			}
		}
		
		[UnityTest]
		public IEnumerator WhenMovementBackCommandSent_ThenCameraMovesBack()
		{
			yield return LoadBootScene();
			Object.FindObjectOfType<GameInstaller>()?.Initialize();
			yield return new WaitForSeconds(0.5f);

			var camera = Object.FindObjectOfType<CameraMover>();
			float startPosZ = camera.transform.position.z;
			
			while (true)
			{
				yield return null;
				if(camera.transform.position.z < startPosZ)
					Assert.Pass();
			}
		}
		
		[UnityTest]
		public IEnumerator WhenMovementLeftCommandSent_ThenCameraMovesLeft()
		{
			yield return LoadBootScene();
			Object.FindObjectOfType<GameInstaller>()?.Initialize();
			yield return new WaitForSeconds(0.5f);

			var camera = Object.FindObjectOfType<CameraMover>();
			float startPosX = camera.transform.position.x;
			
			while (true)
			{
				yield return null;
				if(camera.transform.position.x < startPosX)
					Assert.Pass();
			}
		}
		
		[UnityTest]
		public IEnumerator WhenMovementRightCommandSent_ThenCameraMovesRight()
		{
			yield return LoadBootScene();
			Object.FindObjectOfType<GameInstaller>()?.Initialize();
			yield return new WaitForSeconds(0.5f);

			var camera = Object.FindObjectOfType<CameraMover>();
			float startPosX = camera.transform.position.x;
			
			while (true)
			{
				yield return null;
				if(camera.transform.position.x > startPosX)
					Assert.Pass();
			}
		}

		[UnityTearDown]
		public IEnumerator TearDown()
		{
			var resolvedRunner = Object.FindObjectOfType<ProjectContext>().Container.Resolve<ICoroutineRunner>();
			var resolvedInputService = Object.FindObjectOfType<ProjectContext>().Container.Resolve<IInputService>();

			foreach (CoroutineRunner runner in Object.FindObjectsOfType<CoroutineRunner>())
			{
				if(runner == resolvedRunner)
					continue;
				
				Object.Destroy(runner.gameObject);
			}

			foreach (Runtime.Infrastructure.Inputs.InputService inputService in Object.FindObjectsOfType<Runtime.Infrastructure.Inputs.InputService>())
			{
				if(inputService == resolvedInputService)
					continue;
				
				Object.Destroy(inputService.gameObject);
			}
			
			yield break;
		}

		private AsyncOperation LoadBootScene() => 
			SceneManager.LoadSceneAsync(SceneNames.BootScene, LoadSceneMode.Single);
	}
}