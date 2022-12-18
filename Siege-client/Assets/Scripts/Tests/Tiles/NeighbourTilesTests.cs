using System.Collections;
using System.Linq;
using Kulinaria.Siege.Runtime.Gameplay.Battle;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Grid;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Path;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Selection;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Tiles;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Tiles.Rendering;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Utilities;
using Kulinaria.Siege.Runtime.Infrastructure.ZenjectInstallers;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;
using Zenject;

namespace Kulinaria.Siege.Tests.Tiles
{
	public class NeighbourTilesTests : ZenjectIntegrationTestFixture
	{
		private IGridMap _gridMap;

		[UnityTest]
		public IEnumerator WhenMapGenerated_ThenTilesHaveRightNeighbourCount()
		{
			PrepareTiles();
			CustomTile tile0 = _gridMap.GetTile(0, 0);
			CustomTile tile1 = _gridMap.GetTile(2, 0);
			CustomTile tile2 = _gridMap.GetTile(1, 2);

			Assert.Zero(tile0.NeighboursWithDistances.Count);
			Assert.AreEqual(2, tile1.NeighboursWithDistances.Count);
			Assert.AreEqual(3, tile2.NeighboursWithDistances.Count);
			yield break;
		}

		[UnityTest]
		public IEnumerator WhenMapGenerated_ThenTilesAreNeighboursToEachOther()
		{
			PrepareTiles();
			CustomTile tile0 = _gridMap.GetTile(0, 4);
			CustomTile tile1 = _gridMap.GetTile(1, 4);

			Assert.IsTrue(tile0.NeighboursWithDistances.Keys.Contains(tile1));
			Assert.IsTrue(tile1.NeighboursWithDistances.Keys.Contains(tile0));
			yield break;
		}

		[UnityTest]
		public IEnumerator WhenMapGenerated_ThenDistancesAreCalculated()
		{
			PrepareTiles();

			CustomTile tile21 = _gridMap.GetTile(2, 1);
			CustomTile tile22 = _gridMap.GetTile(2, 2);
			CustomTile tile12 = _gridMap.GetTile(1, 2);

			Assert.AreEqual(2, tile21.NeighboursWithDistances[tile22]);
			Assert.AreEqual(2, tile22.NeighboursWithDistances[tile21]);

			Assert.AreEqual(3, tile21.NeighboursWithDistances[tile12]);
			Assert.AreEqual(3, tile12.NeighboursWithDistances[tile21]);
			yield break;
		}

		private void PrepareTiles()
		{
			GameInstaller.Testing = true;

			Runtime.Gameplay.Battle.Prototype.ArrayGridMap.GridArray = new[,]
			{
				{ 1, 1, 1, 1, 0 },
				{ 0, 0, 0, 0, 1 },
				{ 1, 1, 1, 1, 1 },
				{ 0, 0, 1, 0, 1 },
				{ 1, 0, 1, 1, 0 }
			};

			var cameraMover = AssetDatabase.LoadAssetAtPath<CameraMover>("Assets/Prefabs/Battle/CameraMover.prefab");
			var lineRendererPrefab = AssetDatabase.LoadAssetAtPath<LineRenderer>("Assets/Prefabs/Battle/Path.prefab");

			PreInstall();

			Container.BindFactory<CustomTile, TilemapFactory>().AsSingle();
			Container.Bind<IGridMap>().To<Runtime.Gameplay.Battle.Prototype.ArrayGridMap>().FromNew().AsSingle();
			Container.Bind<CameraMover>().FromInstance(cameraMover).AsSingle();
			Container.Bind<ITilesRenderingAggregator>().To<TilesRenderingAggregator>().FromNew().AsSingle();
			Container.Bind<IPathFinder>().To<BellmanFordPathFinder>().FromNew().AsSingle();
			Container.BindInterfacesTo<PathLineRenderer>().FromNew().AsSingle();
			Container.BindInterfacesTo<PathSelector>().FromNew().AsSingle();
			Container.BindInterfacesTo<CustomTileSelector>().FromNew().AsSingle();
			Container.Bind<Pool<LineRenderer>>().
				FromMethod(_ => new Pool<LineRenderer>(Container, lineRendererPrefab.gameObject, 5)).AsSingle();

			PostInstall();

			_gridMap = Container.Resolve<IGridMap>();
			_gridMap.GenerateMap();
		}
	}
}