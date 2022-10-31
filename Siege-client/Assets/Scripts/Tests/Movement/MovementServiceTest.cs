using System.Collections;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Movement;
using Kulinaria.Siege.Runtime.Infrastructure.Constants;
using Kulinaria.Siege.Runtime.Infrastructure.Coroutines;
using Kulinaria.Siege.Runtime.Infrastructure.ZenjectInstallers;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using Zenject;

namespace Kulinaria.Siege.Tests.Movement
{
	[TestFixture]
	public class MovementServiceTest
	{
		private SceneContext Context => Object.FindObjectOfType<SceneContext>();
		private GameInstaller GameInstaller => Object.FindObjectOfType<GameInstaller>();

		[UnitySetUp]
		public IEnumerator SetUp()
		{
			yield return LoadBootScene();
			GameInstaller.Initialize();
			yield return new WaitForSeconds(2);
		}
		
		[UnityTest]
		public IEnumerator WhenBootSceneLoaded_ThenMovementServicesResolved()
		{
			var movementService = Context.Container.Resolve<IMovementService>();
			var tileFactory = Context.Container.Resolve<TilemapFactory>();

			Assert.NotNull(movementService);
			Assert.NotNull(tileFactory);
			yield break;
		}

		[UnityTest]
		public IEnumerator WhenBattleSceneLoaded_ThenCustomTilesAreInstantiated()
		{
			CustomTile[] tiles = Object.FindObjectsOfType<CustomTile>();
			Assert.NotZero(tiles.Length);
			yield break;
		}

		[UnityTearDown]
		public IEnumerator TearDown()
		{
			var resolvedRunner = Object.FindObjectOfType<ProjectContext>().Container.Resolve<ICoroutineRunner>();

			foreach (CoroutineRunner runner in Object.FindObjectsOfType<CoroutineRunner>())
			{
				if(ReferenceEquals(runner, resolvedRunner))
					continue;
				
				Object.Destroy(runner.gameObject);
			}
			
			yield break;
		}

		private AsyncOperation LoadBootScene() =>
			SceneManager.LoadSceneAsync(SceneNames.BootScene, LoadSceneMode.Single);
	}
}