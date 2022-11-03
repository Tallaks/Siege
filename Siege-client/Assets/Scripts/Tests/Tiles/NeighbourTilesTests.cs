using System.Collections;
using System.Linq;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Movement;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Prototype;
using NUnit.Framework;
using UnityEngine.TestTools;
using Zenject;

namespace Kulinaria.Siege.Tests.Tiles
{
	public class NeighbourTilesTests : ZenjectIntegrationTestFixture
	{
		private IGridMap _gridMap;

		[UnitySetUp]
		public IEnumerator SetUp()
		{
			var grid = new[,]
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

			PostInstall();

			_gridMap = Container.Resolve<IGridMap>();
			_gridMap.GenerateMap(grid);
			yield break;
		}

		[UnityTest]
		public IEnumerator WhenMapGenerated_ThenTilesHaveRightNeighbourCount()
		{
			CustomTile tile0 = _gridMap.GetTile(0, 0);
			CustomTile tile1 = _gridMap.GetTile(2, 0);
			CustomTile tile2 = _gridMap.GetTile(1, 2);

			Assert.Zero(tile0.Neighbours.Count());
			Assert.AreEqual(2, tile1.Neighbours.Count());
			Assert.AreEqual(3, tile2.Neighbours.Count());
			yield break;
		}

		[UnityTest]
		public IEnumerator WhenMapGenerated_ThenTilesAreNeighboursToEachOther()
		{
			CustomTile tile0 = _gridMap.GetTile(0, 4);
			CustomTile tile1 = _gridMap.GetTile(1, 4);

			Assert.IsTrue(tile0.Neighbours.Contains(tile1));
			Assert.IsTrue(tile1.Neighbours.Contains(tile0));
			yield break;
		}
	}
}