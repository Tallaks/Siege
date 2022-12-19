using System.Collections;
using System.Linq;
using Kulinaria.Siege.Runtime.Gameplay.Battle;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Grid;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Path;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Selection;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Tiles;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Tiles.Rendering;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Movement;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Utilities;
using Kulinaria.Siege.Runtime.Infrastructure.ZenjectInstallers;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;
using Zenject;

namespace Kulinaria.Siege.Tests.Tiles
{
	public partial class TileRenderingTests : ZenjectIntegrationTestFixture
	{
		private IGridMap _gridMap;
		private IPathFinder _pathFinder;
		private IClickInteractor _selector;

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

			TileRenderer[] tiles = Object.FindObjectsOfType<TileRenderer>(includeInactive: true);
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

			CustomTile[] tiles = Object.FindObjectsOfType<CustomTile>(includeInactive: true);

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
			//_selector.Select(targetTile, _pathFinder.GetAvailableTilesByDistance(1000));

			foreach (CustomTile tile in _gridMap.AllTiles)
				tile.Active = true;

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
			//_selector.Select(targetTile, _pathFinder.GetAvailableTilesByDistance(1000));
			foreach (CustomTile tile in _gridMap.AllTiles)
				tile.Active = true;

			yield return new WaitForSeconds(0.01f);

			var tileRenderer = targetTile.GetComponent<TileRenderer>();

			Assert.AreEqual(targetTexture, tileRenderer.CurrentTexture);

			_gridMap.Clear();
		}

		private void PrepareTiles()
		{
			GameInstaller.Testing = true;

			var cameraMover = AssetDatabase.LoadAssetAtPath<CameraMover>("Assets/Prefabs/Battle/CameraMover.prefab");
			var lineRendererPrefab = AssetDatabase.LoadAssetAtPath<LineRenderer>("Assets/Prefabs/Battle/Path.prefab");

			PreInstall();

			Container.BindFactory<CustomTile, TilemapFactory>().AsSingle();
			Container.Bind<IGridMap>().To<Runtime.Gameplay.Battle.Prototype.ArrayGridMap>().FromNew().AsSingle();
			Container.Bind<IPathFinder>().To<BellmanFordPathFinder>().FromNew().AsSingle();
			Container.Bind<IMovementService>().To<TileMovementService>().FromNew().AsSingle();
			Container.Bind<ITilesRenderingAggregator>().To<TilesRenderingAggregator>().FromNew().AsSingle();
			Container.Bind<CameraMover>().FromComponentInNewPrefab(cameraMover).AsSingle();
			Container.BindInterfacesTo<PathLineRenderer>().FromNew().AsSingle();
			Container.BindInterfacesTo<GridmapInteractor>().FromNew().AsSingle();
			Container.BindInterfacesTo<PathSelector>().FromNew().AsSingle();
			Container.Bind<Pool<LineRenderer>>().
				FromMethod(_ => new Pool<LineRenderer>(Container, lineRendererPrefab.gameObject, 5)).AsSingle();

			PostInstall();

			_gridMap = Container.Resolve<IGridMap>();
			_selector = Container.Resolve<IClickInteractor>();
			_pathFinder = Container.Resolve<IPathFinder>();
		}
	}
}