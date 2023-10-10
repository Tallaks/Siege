using System.Collections;
using System.Linq;
using Kulinaria.Siege.Runtime.Gameplay.Battle;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Grid;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Path;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Selection;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Tiles;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Tiles.Rendering;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Prototype;
using Kulinaria.Siege.Tests.TestInfrastructure.Installers;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Zenject;

namespace Kulinaria.Siege.Tests.GridMap
{
	public class GridGeneratorZenjectTests : ZenjectIntegrationTestFixture
	{
		[UnityTest]
		public IEnumerator WhenTileFactoryAndGridGeneratorBound_ThenTheyCanBeResolved()
		{
			PrepareTiles();

			Assert.NotNull(Container.Resolve<TilemapFactory>());
			Assert.NotNull(Container.Resolve<IGridMap>());
			Assert.NotNull(Container.Resolve<IPathFinder>());
			Assert.NotNull(Container.Resolve<ITilesRenderingAggregator>());
			Assert.NotNull(Container.Resolve<IClickInteractor>());
			Assert.NotNull(Container.Resolve<CameraMover>());
			yield break;
		}

		[UnityTest]
		public IEnumerator WhenTilemapInitializedWith2DArray_ThenItGeneratesGridWithFourTilesAsSquare2x2()
		{
			ArrayGridMap.GridArray = new[,] { { 1, 1 }, { 1, 1 } };

			PrepareTiles();
			Container.Resolve<IGridMap>().GenerateMap();

			Assert.NotZero(Object.FindObjectsOfType<CustomTile>(true).Length);
			Assert.AreEqual(4, Object.FindObjectsOfType<CustomTile>(true).Length);
			Assert.AreEqual(4, Container.Resolve<IGridMap>().AllTiles.Count());
			yield break;
		}

		[UnityTest]
		public IEnumerator WhenTilemapInitializedWith2DArray_ThenItGeneratesGridWithFourTilesAsDiagonal()
		{
			ArrayGridMap.GridArray = new[,]
			{
				{ 0, 1 },
				{ 1, 0 }
			};

			PrepareTiles();
			Container.Resolve<IGridMap>().GenerateMap();

			CustomTile[] customTiles = Object.FindObjectsOfType<CustomTile>(true);
			Assert.NotZero(customTiles.Length);
			Assert.AreEqual(2, customTiles.Length);
			Assert.AreEqual(
				new Vector3(0.5f, 0.1f, 0.5f),
				customTiles[0].transform.position);
			Assert.AreEqual(2, Container.Resolve<IGridMap>().AllTiles.Count());
			yield break;
		}

		[UnityTest]
		public IEnumerator WhenTilemapInitializedWith2DArray_ThenItGeneratesGridWithOneLineOf5()
		{
			ArrayGridMap.GridArray = new[,]
			{
				{ 1, 1, 1, 1, 0 },
				{ 0, 0, 0, 0, 1 }
			};

			PrepareTiles();
			Container.Resolve<IGridMap>().GenerateMap();

			CustomTile[] customTiles = Object.FindObjectsOfType<CustomTile>(true);
			Assert.NotZero(customTiles.Length);
			Assert.AreEqual(5, customTiles.Length);
			Assert.AreEqual(
				new Vector3(0.5f, 0.1f, 1.5f),
				customTiles[4].transform.position);
			Assert.AreEqual(
				new Vector3(4.5f, 0.1f, 0.5f),
				customTiles[0].transform.position);
			Assert.AreEqual(5, Container.Resolve<IGridMap>().AllTiles.Count());

			yield break;
		}

		[UnityTest]
		public IEnumerator WhenTTilemapInitialized_ThenAllTilesHaveRightCellPositions()
		{
			ArrayGridMap.GridArray = new[,] { { 1, 1 }, { 1, 1 } };
			PrepareTiles();
			Container.Resolve<IGridMap>().GenerateMap();

			CustomTile[] customTiles = Object.FindObjectsOfType<CustomTile>(true);
			Assert.IsFalse(customTiles[0].CellPosition == customTiles[1].CellPosition);
			Assert.IsFalse(customTiles[0].CellPosition == customTiles[2].CellPosition);
			Assert.IsFalse(customTiles[0].CellPosition == customTiles[3].CellPosition);
			Assert.IsFalse(customTiles[1].CellPosition == customTiles[2].CellPosition);
			Assert.IsFalse(customTiles[1].CellPosition == customTiles[3].CellPosition);
			Assert.IsFalse(customTiles[2].CellPosition == customTiles[3].CellPosition);

			yield break;
		}

		private void PrepareTiles()
		{
			var tilemapInstaller = new TilemapInstaller();
			tilemapInstaller.PreInstall();

			var charactersInstaller = new CharactersInstaller();
			charactersInstaller.PreInstall();

			var gameplayInstaller = new GameplayInstaller();
			gameplayInstaller.PreInstall();

			var poolsInstaller = new PoolsInstaller();
			poolsInstaller.PreInstall();

			PreInstall();

			tilemapInstaller.Install(Container);
			charactersInstaller.Install(Container);
			gameplayInstaller.Install(Container);
			poolsInstaller.Install(Container);

			PostInstall();
		}
	}
}