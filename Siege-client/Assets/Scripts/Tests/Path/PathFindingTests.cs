using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Grid;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Path;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Selection;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Tiles;
using Kulinaria.Siege.Runtime.Infrastructure.ZenjectInstallers;
using Kulinaria.Siege.Tests.TestInfrastructure.Installers;
using NUnit.Framework;
using UnityEngine.TestTools;
using Zenject;
using PoolsInstaller = Kulinaria.Siege.Tests.TestInfrastructure.Installers.PoolsInstaller;
using TilemapInstaller = Kulinaria.Siege.Tests.TestInfrastructure.Installers.TilemapInstaller;

namespace Kulinaria.Siege.Tests.Path
{
	public class PathFindingTests : ZenjectIntegrationTestFixture
	{
		private IGridMap _gridMap;
		private IPathFinder _pathFinder;

		[UnityTest]
		public IEnumerator WhenTileSelected_ThenDistanceAndPathToSameTileIsZero()
		{
			PrepareTiles();

			CustomTile tile04 = _gridMap.GetTile(0, 4);

			_pathFinder.FindDistancesToAllTilesFrom(tile04);

			LinkedList<CustomTile> path = _pathFinder.GetShortestPath(tile04);

			Assert.AreEqual(0, _pathFinder.Distance(tile04));
			Assert.AreEqual(0, path.Count);

			yield break;
		}

		[UnityTest]
		public IEnumerator WhenTileSelected_ThenDistancesToChosenTileAreCorrect()
		{
			PrepareTiles();

			CustomTile tile04 = _gridMap.GetTile(0, 4);
			CustomTile tile32 = _gridMap.GetTile(3, 2);
			CustomTile tile00 = _gridMap.GetTile(0, 0);
			CustomTile tile14 = _gridMap.GetTile(1, 4);
			CustomTile tile07 = _gridMap.GetTile(0, 7);

			_pathFinder.FindDistancesToAllTilesFrom(tile04);

			Assert.AreEqual(12, _pathFinder.Distance(tile32));
			Assert.AreEqual(int.MaxValue, _pathFinder.Distance(tile00));
			Assert.AreEqual(2, _pathFinder.Distance(tile14));
			Assert.AreEqual(6, _pathFinder.Distance(tile07));
			yield break;
		}

		[UnityTest]
		public IEnumerator WhenTwoTilesSelected_ThenShortestPathCounted()
		{
			PrepareTiles();

			CustomTile tile04 = _gridMap.GetTile(0, 4);
			CustomTile tile34 = _gridMap.GetTile(3, 4);
			CustomTile tile14 = _gridMap.GetTile(1, 4);
			CustomTile tile24 = _gridMap.GetTile(2, 4);
			CustomTile tile00 = _gridMap.GetTile(0, 0);

			_pathFinder.FindDistancesToAllTilesFrom(tile04);
			LinkedList<CustomTile> path = _pathFinder.GetShortestPath(tile34);
			LinkedListNode<CustomTile> currentNode = path.First;
			for (var i = 0; i < path.Count; i++)
				currentNode = path.Find(currentNode.Value).Next;

			Assert.AreEqual(4, path.Count);
			Assert.That(path.First.Value == tile04);
			Assert.That(path.Last.Value == tile34);
			Assert.That(path.Contains(tile14));
			Assert.That(path.Contains(tile24));
			Assert.That(path.Find(tile24).Next.Value == tile34);
			Assert.That(path.Find(tile14).Next.Value == tile24);
			Assert.That(path.Find(tile04).Next.Value == tile14);
			Assert.That(!path.Contains(tile00));

			path = _pathFinder.GetShortestPath(tile00);
			Assert.AreEqual(0, path.Count);

			yield break;
		}

		[UnityTest]
		public IEnumerator WhenTileSelected_ThenCorrectAvailableAreaCreated()
		{
			PrepareTiles();

			CustomTile tile04 = _gridMap.GetTile(0, 4);
			CustomTile tile05 = _gridMap.GetTile(0, 5);
			CustomTile tile06 = _gridMap.GetTile(0, 6);
			CustomTile tile07 = _gridMap.GetTile(0, 7);
			CustomTile tile17 = _gridMap.GetTile(1, 7);
			CustomTile tile27 = _gridMap.GetTile(2, 7);
			CustomTile tile14 = _gridMap.GetTile(1, 4);
			CustomTile tile24 = _gridMap.GetTile(2, 4);
			CustomTile tile34 = _gridMap.GetTile(3, 4);
			CustomTile tile43 = _gridMap.GetTile(4, 3);
			CustomTile tile36 = _gridMap.GetTile(3, 6);

			_pathFinder.FindDistancesToAllTilesFrom(tile04);
			IEnumerable<CustomTile> nearestTiles = _pathFinder.GetAvailableTilesByDistance(9);

			Assert.AreEqual(10, nearestTiles.Count());
			Assert.IsTrue(nearestTiles.Contains(tile04));
			Assert.IsTrue(nearestTiles.Contains(tile05));
			Assert.IsTrue(nearestTiles.Contains(tile06));
			Assert.IsTrue(nearestTiles.Contains(tile07));
			Assert.IsTrue(nearestTiles.Contains(tile17));
			Assert.IsTrue(nearestTiles.Contains(tile27));
			Assert.IsTrue(nearestTiles.Contains(tile14));
			Assert.IsTrue(nearestTiles.Contains(tile24));
			Assert.IsTrue(nearestTiles.Contains(tile34));
			Assert.IsTrue(nearestTiles.Contains(tile43));
			Assert.IsTrue(!nearestTiles.Contains(tile36));

			nearestTiles = _pathFinder.GetAvailableTilesByDistance(1);
			Assert.AreEqual(1, nearestTiles.Count());

			yield break;
		}

		private void PrepareTiles()
		{
			GameInstaller.Testing = true;

			Runtime.Gameplay.Battle.Prototype.ArrayGridMap.GridArray = new[,]
			{
				{ 1, 1, 1, 1, 0, 1, 1 },
				{ 1, 0, 0, 1, 0, 1, 0 },
				{ 1, 0, 0, 0, 0, 1, 0 },
				{ 1, 1, 1, 1, 0, 1, 1 },
				{ 0, 0, 0, 0, 1, 1, 1 },
				{ 1, 1, 1, 1, 1, 0, 0 },
				{ 0, 0, 1, 0, 1, 0, 1 },
				{ 1, 0, 1, 1, 0, 0, 1 }
			};

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

			Container.Resolve<IClickInteractor>();
			_gridMap = Container.Resolve<IGridMap>();
			_gridMap.GenerateMap();

			_pathFinder = Container.Resolve<IPathFinder>();
		}
	}
}