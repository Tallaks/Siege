using System.Collections;
using System.Collections.Generic;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Movement;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Prototype;
using Kulinaria.Siege.Runtime.Infrastructure.ZenjectInstallers;
using NUnit.Framework;
using UnityEngine.TestTools;
using Zenject;

namespace Kulinaria.Siege.Tests.Movement
{
	public class PathFindingTests : ZenjectIntegrationTestFixture
	{
		private IGridMap _gridMap;
		private IPathFinder _pathFinder;

		[UnityTest]
		public IEnumerator WhenTileSelected_ThenDistancesToChosenTileAreCorrect()
		{
			PrepareTiles();

			CustomTile tile04 = _gridMap.GetTile(0, 4);
			CustomTile tile32 = _gridMap.GetTile(3, 2);
			CustomTile tile00 = _gridMap.GetTile(0, 0);
			CustomTile tile14 = _gridMap.GetTile(1, 4);

			_gridMap.OnTileSelection?.Invoke(tile04);

			Assert.AreEqual(12, _pathFinder.Distance(tile32));
			Assert.AreEqual(int.MaxValue, _pathFinder.Distance(tile00));
			Assert.AreEqual(2, _pathFinder.Distance(tile14));
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

			_gridMap.OnTileSelection?.Invoke(tile04);

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

		private void PrepareTiles()
		{
			GameInstaller.Testing = true;

			Runtime.Gameplay.Battle.Prototype.GridMap.GridArray = new[,]
			{
				{ 1, 1, 1, 1, 0 },
				{ 0, 0, 0, 0, 1 },
				{ 1, 1, 1, 1, 1 },
				{ 0, 0, 1, 0, 1 },
				{ 1, 0, 1, 1, 0 }
			};

			PreInstall();

			Container.BindFactory<CustomTile, TilemapFactory>().AsSingle();
			Container.Bind<IGridMap>().To<Runtime.Gameplay.Battle.Prototype.GridMap>().FromNew().AsSingle();
			Container.Bind<IPathFinder>().To<BellmanFordPathFinder>().FromNew().AsSingle();

			PostInstall();

			_gridMap = Container.Resolve<IGridMap>();
			_gridMap.GenerateMap();

			_pathFinder = Container.Resolve<IPathFinder>();
		}
	}
}