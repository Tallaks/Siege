using System.Collections;
using System.Linq;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Grid;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Tiles;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Tiles.Rendering;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Prototype;
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
			PreInstall();

			Container.BindFactory<CustomTile, TilemapFactory>().AsSingle();
			Container.Bind<IGridMap>().To<ArrayGridMap>().FromNew().AsSingle();

			PostInstall();

			Assert.NotNull(Container.Resolve<TilemapFactory>());
			Assert.NotNull(Container.Resolve<IGridMap>());
			yield break;
		}

		[UnityTest]
		public IEnumerator WhenTilemapInitializedWith2DArray_ThenItGeneratesGridWithFourTilesAsSquare2x2()
		{
			ArrayGridMap.GridArray = new[,] { { 1, 1 }, { 1, 1 } };

			PreInstall();

			Container.BindFactory<CustomTile, TilemapFactory>().AsSingle();
			Container.Bind<IGridMap>().To<ArrayGridMap>().FromNew().AsSingle();
			Container.Bind<ITilesRenderingAggregator>().To<TilesRenderingAggregator>().FromNew().AsSingle();

			PostInstall();

			Container.Resolve<IGridMap>().GenerateMap();

			Assert.NotZero(Object.FindObjectsOfType<CustomTile>(includeInactive: true).Length);
			Assert.AreEqual(4, Object.FindObjectsOfType<CustomTile>(includeInactive: true).Length);
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

			PreInstall();

			Container.BindFactory<CustomTile, TilemapFactory>().AsSingle();
			Container.Bind<IGridMap>().To<ArrayGridMap>().FromNew().AsSingle();
			Container.Bind<ITilesRenderingAggregator>().To<TilesRenderingAggregator>().FromNew().AsSingle();

			PostInstall();

			Container.Resolve<IGridMap>().GenerateMap();

			CustomTile[] customTiles = Object.FindObjectsOfType<CustomTile>(includeInactive: true);
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

			PreInstall();

			Container.BindFactory<CustomTile, TilemapFactory>().AsSingle();
			Container.Bind<IGridMap>().To<ArrayGridMap>().FromNew().AsSingle();
			Container.Bind<ITilesRenderingAggregator>().To<TilesRenderingAggregator>().FromNew().AsSingle();

			PostInstall();

			Container.Resolve<IGridMap>().GenerateMap();

			CustomTile[] customTiles = Object.FindObjectsOfType<CustomTile>(includeInactive: true);
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

			PreInstall();

			Container.BindFactory<CustomTile, TilemapFactory>().AsSingle();
			Container.Bind<IGridMap>().To<ArrayGridMap>().FromNew().AsSingle();
			Container.Bind<ITilesRenderingAggregator>().To<TilesRenderingAggregator>().FromNew().AsSingle();

			PostInstall();

			Container.Resolve<IGridMap>().GenerateMap();

			CustomTile[] customTiles = Object.FindObjectsOfType<CustomTile>(includeInactive: true);
			Assert.IsFalse(customTiles[0].CellPosition == customTiles[1].CellPosition);
			Assert.IsFalse(customTiles[0].CellPosition == customTiles[2].CellPosition);
			Assert.IsFalse(customTiles[0].CellPosition == customTiles[3].CellPosition);
			Assert.IsFalse(customTiles[1].CellPosition == customTiles[2].CellPosition);
			Assert.IsFalse(customTiles[1].CellPosition == customTiles[3].CellPosition);
			Assert.IsFalse(customTiles[2].CellPosition == customTiles[3].CellPosition);

			yield break;
		}
	}
}