using System.Collections;
using System.Linq;
using Kulinaria.Siege.Runtime.Gameplay.Battle;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Grid;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Path;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Selection;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Tiles;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Tiles.Rendering;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Prototype;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Utilities;
using NUnit.Framework;
using UnityEditor;
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
			var cameraMover = AssetDatabase.LoadAssetAtPath<CameraMover>("Assets/Prefabs/Battle/CameraMover.prefab");
			var lineRendererPrefab = AssetDatabase.LoadAssetAtPath<LineRenderer>("Assets/Prefabs/Battle/Path.prefab");

			PreInstall();

			Container.BindFactory<CustomTile, TilemapFactory>().AsSingle();
			Container.Bind<IGridMap>().To<ArrayGridMap>().FromNew().AsSingle();
			Container.Bind<IPathFinder>().To<BellmanFordPathFinder>().FromNew().AsSingle();
			Container.Bind<ITilesRenderingAggregator>().To<TilesRenderingAggregator>().FromNew().AsSingle();
			Container.BindInterfacesTo<PathLineRenderer>().FromNew().AsSingle();
			Container.BindInterfacesTo<PathSelector>().FromNew().AsSingle();
			Container.BindInterfacesTo<GridmapInteractor>().FromNew().AsSingle();
			Container.Bind<CameraMover>().FromComponentInNewPrefab(cameraMover).AsSingle();
			Container.Bind<Pool<LineRenderer>>().
				FromMethod(_ => new Pool<LineRenderer>(Container, lineRendererPrefab.gameObject, 5)).AsSingle();

			PostInstall();

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

			PrepareTiles();

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

			PrepareTiles();

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
			PrepareTiles();

			CustomTile[] customTiles = Object.FindObjectsOfType<CustomTile>(includeInactive: true);
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
			var cameraMover = AssetDatabase.LoadAssetAtPath<CameraMover>("Assets/Prefabs/Battle/CameraMover.prefab");
			var lineRendererPrefab = AssetDatabase.LoadAssetAtPath<LineRenderer>("Assets/Prefabs/Battle/Path.prefab");

			PreInstall();

			Container.BindFactory<CustomTile, TilemapFactory>().AsSingle();
			Container.Bind<IGridMap>().To<ArrayGridMap>().FromNew().AsSingle();
			Container.Bind<IPathFinder>().To<BellmanFordPathFinder>().FromNew().AsSingle();
			Container.Bind<ITilesRenderingAggregator>().To<TilesRenderingAggregator>().FromNew().AsSingle();
			Container.Bind<CameraMover>().FromComponentInNewPrefab(cameraMover).AsSingle();
			Container.BindInterfacesTo<PathLineRenderer>().FromNew().AsSingle();
			Container.BindInterfacesTo<PathSelector>().FromNew().AsSingle();
			Container.BindInterfacesTo<GridmapInteractor>().FromNew().AsSingle();
			Container.Bind<Pool<LineRenderer>>().
				FromMethod(_ => new Pool<LineRenderer>(Container, lineRendererPrefab.gameObject, 5)).AsSingle();

			PostInstall();

			Container.Resolve<IGridMap>().GenerateMap();
		}
	}
}