using System.Collections;
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

		[UnitySetUp]
		public IEnumerator PrepareTiles()
		{
			Setup();
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
			
			yield return null;
		}
		
		[UnityTest]
		public IEnumerator WhenMapGenerated_ThenTilesAreNeighboursToEachOther()
		{
			CustomTile tile04 = _gridMap.GetTile(0, 4);
			CustomTile tile32 = _gridMap.GetTile(3, 2);
			CustomTile tile00 = _gridMap.GetTile(0, 0);
			CustomTile tile14 = _gridMap.GetTile(1, 4);
			
			_gridMap.OnTileSelection?.Invoke(tile04);
			
			Assert.AreEqual(12, _pathFinder.Distance(tile04, tile32));
			Assert.AreEqual(int.MaxValue, _pathFinder.Distance(tile04, tile00));
			Assert.AreEqual(2,_pathFinder.Distance(tile04, tile14));
			yield break;
		}
	}
}