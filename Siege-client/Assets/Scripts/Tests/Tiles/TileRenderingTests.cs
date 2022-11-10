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

			Assert.IsTrue(tiles.All(k => !k.Active));
			
			yield break;
		}

		[UnityTest]
		public IEnumerator WhenOneTileGeneratedAndActive_ThenItLoadedTileConfig()
		{
			PrepareOneTile();
			CustomTile targetTile = _gridMap.GetTile(0, 0);
			targetTile.Active = true;
			
			Assert.NotNull(targetTile.GetComponent<TileRenderer>().ConfigForTest);
			yield break;
		}

		
		[UnityTest]
		public IEnumerator WhenOneTileGeneratedAndActive_ThenItHas0_4_4()
		{
			PrepareOneTile();
			CustomTile targetTile = _gridMap.GetTile(0, 0);
			
			_gridMap.OnTileSelection?.Invoke(targetTile);
			targetTile.Active = true;

			yield return new WaitForSeconds(0.1f);
			
			var tileRenderer = targetTile.GetComponent<TileRenderer>();
			Assert.AreEqual( tileRenderer.ConfigForTest.Tile0_4_4, tileRenderer.CurrentTexture); 
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

			var camera = new GameObject().AddComponent<Camera>();
			camera.orthographic = true;
			camera.transform.eulerAngles = new Vector3(90, 0, 0);
			camera.transform.position = new Vector3(0, 5, 0);

			PreInstall();

			Container.BindFactory<CustomTile, TilemapFactory>().AsSingle();
			Container.Bind<IGridMap>().To<Runtime.Gameplay.Battle.Prototype.GridMap>().FromNew().AsSingle();
			Container.Bind<IPathFinder>().To<BellmanFordPathFinder>().FromNew().AsSingle();
			Container.Bind<IMovementService>().To<TileMovementService>().FromNew().AsSingle();
			Container.Bind<Camera>().FromInstance(camera).AsSingle();

			PostInstall();

			_gridMap = Container.Resolve<IGridMap>();
			_gridMap.GenerateMap();
		}
		
		private void PrepareOneTile()
		{
			GameInstaller.Testing = true;

			Runtime.Gameplay.Battle.Prototype.GridMap.GridArray = new[,]
			{
				{ 1 }
			};

			var camera = new GameObject().AddComponent<Camera>();
			camera.orthographic = true;
			camera.transform.eulerAngles = new Vector3(90, 0, 0);
			camera.transform.position = new Vector3(0, 5, 0);

			PreInstall();

			Container.BindFactory<CustomTile, TilemapFactory>().AsSingle();
			Container.Bind<IGridMap>().To<Runtime.Gameplay.Battle.Prototype.GridMap>().FromNew().AsSingle();
			Container.Bind<IPathFinder>().To<BellmanFordPathFinder>().FromNew().AsSingle();
			Container.Bind<IMovementService>().To<TileMovementService>().FromNew().AsSingle();
			Container.Bind<Camera>().FromInstance(camera).AsSingle();

			PostInstall();

			_gridMap = Container.Resolve<IGridMap>();
			_gridMap.GenerateMap();
		}
	}
}