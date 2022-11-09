using System.Collections;
using System.Linq;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Movement;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Movement.Tiles;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Prototype;
using Kulinaria.Siege.Runtime.Infrastructure.ZenjectInstallers;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Zenject;

namespace Kulinaria.Siege.Tests.Tiles
{
	public class TileRenderingTests : ZenjectIntegrationTestFixture
	{
		private IGridMap _gridMap;

		[UnityTest]
		public IEnumerator WhenTilesGenerated_ThenTheyHaveRenderingComponent()
		{
			PrepareTiles();
			
			TileRenderer[] tiles = Object.FindObjectsOfType<TileRenderer>(includeInactive: true);
			string shaderName = tiles[0].GetComponent<MeshRenderer>().material.shader.name;

			Assert.AreEqual("Shader Graphs/Tile", shaderName);
			Assert.NotZero(tiles.Length);
			
			yield break;
		}

		[UnityTest]
		public IEnumerator WhenTilesGeneratedAndIsUnselected_ThenTilesAreInActive()
		{
			PrepareTiles();

			CustomTile[] tiles = Object.FindObjectsOfType<CustomTile>(includeInactive: true);

			Assert.IsTrue(tiles.All(k => !k.isActiveAndEnabled));
			
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
			Container.Bind<IMovementService>().To<TileMovementService>().FromNew().AsSingle();
			
			PostInstall();

			_gridMap = Container.Resolve<IGridMap>();
			_gridMap.GenerateMap();
		}
	}
}