using System.Collections;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Grid;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Tiles;
using Kulinaria.Siege.Runtime.Infrastructure.Constants;
using Kulinaria.Siege.Runtime.Infrastructure.Coroutines;
using Kulinaria.Siege.Runtime.Infrastructure.ZenjectInstallers;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using Zenject;

namespace Kulinaria.Siege.Tests.GridMap
{
	[TestFixture]
	public class GridGeneratorTest
	{
		private SceneContext Context => Object.FindObjectOfType<SceneContext>();
		private ApplicationInstaller ApplicationInstaller => Object.FindObjectOfType<ApplicationInstaller>();

		[UnitySetUp]
		public IEnumerator SetUp()
		{
			ApplicationInstaller.Testing = false;
			yield return LoadBootScene();
			ApplicationInstaller.Initialize();
			yield return new WaitForSeconds(2);
		}

		[UnityTest]
		public IEnumerator WhenBootSceneLoaded_ThenGridGeneratorResolved()
		{
			var gridMap = Context.Container.Resolve<IGridMap>();
			Assert.NotNull(gridMap);
			yield break;
		}

		[UnityTest]
		public IEnumerator WhenBattleSceneLoaded_ThenCustomTilesAreInstantiated()
		{
			CustomTile[] tiles = Object.FindObjectsOfType<CustomTile>(includeInactive: true);
			Assert.NotZero(tiles.Length);
			yield break;
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

			ApplicationInstaller.Testing = true;
			yield break;
		}

		private AsyncOperation LoadBootScene() => 
			SceneManager.LoadSceneAsync(SceneNames.BootScene, LoadSceneMode.Single);
	}
}