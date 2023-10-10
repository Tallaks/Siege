using System.Collections;
using System.Linq;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Grid;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Path;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Selection;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Tiles;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Tiles.Rendering;
using Kulinaria.Siege.Runtime.Infrastructure.Coroutines;
using Kulinaria.Siege.Runtime.Infrastructure.ZenjectInstallers;
using Kulinaria.Siege.Tests.TestInfrastructure.Installers;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Zenject;
using PoolsInstaller = Kulinaria.Siege.Tests.TestInfrastructure.Installers.PoolsInstaller;
using TilemapInstaller = Kulinaria.Siege.Tests.TestInfrastructure.Installers.TilemapInstaller;

namespace Kulinaria.Siege.Tests.Tiles
{
	public partial class TileRenderingTests : ZenjectIntegrationTestFixture
	{
		private IGridMap _gridMap;
		private IPathFinder _pathFinder;

		[UnityTest]
		public IEnumerator WhenTilesGenerated_ThenTheyHaveRenderingComponent()
		{
			Runtime.Gameplay.Battle.Prototype.ArrayGridMap.GridArray = new[,]
			{
				{ 1, 1, 1, 1, 0 },
				{ 0, 0, 0, 0, 1 },
				{ 1, 1, 1, 1, 1 },
				{ 0, 0, 1, 0, 1 },
				{ 1, 0, 1, 1, 0 }
			};

			PrepareTiles();
			_gridMap.GenerateMap();

			TileRenderer[] tiles = Object.FindObjectsOfType<TileRenderer>(true);
			string shaderName = tiles[0].GetComponent<MeshRenderer>().material.shader.name;

			Assert.AreEqual("Shader Graphs/Tile", shaderName);
			Assert.NotZero(tiles.Length);

			yield break;
		}

		[UnityTest]
		public IEnumerator WhenTilesGeneratedAndIsUnselected_ThenTilesAreInActive()
		{
			Runtime.Gameplay.Battle.Prototype.ArrayGridMap.GridArray = new[,]
			{
				{ 1, 1, 1, 1, 0 },
				{ 0, 0, 0, 0, 1 },
				{ 1, 1, 1, 1, 1 },
				{ 0, 0, 1, 0, 1 },
				{ 1, 0, 1, 1, 0 }
			};

			PrepareTiles();
			_gridMap.GenerateMap();

			CustomTile[] tiles = Object.FindObjectsOfType<CustomTile>(true);

			Assert.IsTrue(tiles.All(k => !k.Active));

			yield break;
		}

		private IEnumerator AssertTileTextureAndAngleFor(
			int[,] gridArray, float angle, Texture2D targetTexture)
		{
			Runtime.Gameplay.Battle.Prototype.ArrayGridMap.GridArray = gridArray;

			_gridMap.GenerateMap();

			CustomTile targetTile = _gridMap.GetTile(1, 1);
			_pathFinder.FindDistancesToAllTilesFrom(targetTile);

			foreach (CustomTile tile in _gridMap.AllTiles)
				tile.Active = true;
			foreach (CustomTile tile in _gridMap.AllTiles)
				tile.Renderer.Repaint();

			yield return new WaitForSeconds(0.01f);

			var tileRenderer = targetTile.GetComponent<TileRenderer>();

			Assert.AreEqual(targetTexture, tileRenderer.CurrentTexture);
			Assert.AreEqual(angle, tileRenderer.TextureAngle);

			_gridMap.Clear();
		}

		private IEnumerator AssertTileTextureFor(
			int[,] gridArray, Texture2D targetTexture)
		{
			Runtime.Gameplay.Battle.Prototype.ArrayGridMap.GridArray = gridArray;

			_gridMap.GenerateMap();

			CustomTile targetTile = _gridMap.GetTile(1, 1);
			_pathFinder.FindDistancesToAllTilesFrom(targetTile);

			foreach (CustomTile tile in _gridMap.AllTiles)
				tile.Active = true;
			foreach (CustomTile tile in _gridMap.AllTiles)
				tile.Renderer.Repaint();

			yield return new WaitForSeconds(0.01f);

			var tileRenderer = targetTile.GetComponent<TileRenderer>();

			Assert.AreEqual(targetTexture, tileRenderer.CurrentTexture);

			_gridMap.Clear();
		}

		private void PrepareTiles()
		{
			ApplicationInstaller.Testing = true;

			var tilemapInstaller = new TilemapInstaller();
			tilemapInstaller.PreInstall();

			var charactersInstaller = new CharactersInstaller();
			charactersInstaller.PreInstall();

			var gameplayInstaller = new TestInfrastructure.Installers.GameplayInstaller();
			gameplayInstaller.PreInstall();

			var poolsInstaller = new PoolsInstaller();
			poolsInstaller.PreInstall();

			PreInstall();

			tilemapInstaller.Install(Container);
			charactersInstaller.Install(Container);
			gameplayInstaller.Install(Container);
			poolsInstaller.Install(Container);

			PostInstall();

			Debug.Log(Container.Resolve<ICoroutineRunner>());
			_gridMap = Container.Resolve<IGridMap>();
			Container.Resolve<IClickInteractor>();
			_pathFinder = Container.Resolve<IPathFinder>();
		}
	}
}