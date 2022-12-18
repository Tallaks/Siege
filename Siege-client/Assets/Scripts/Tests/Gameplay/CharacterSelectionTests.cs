using System.Collections;
using System.Collections.Generic;
using Kulinaria.Siege.Runtime.Gameplay.Battle;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Characters.Factory;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Grid;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Path;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Selection;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Tiles;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Tiles.Rendering;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Movement;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Spawn;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Utilities;
using Kulinaria.Siege.Runtime.Infrastructure.ZenjectInstallers;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;
using Zenject;

namespace Kulinaria.Siege.Tests.Gameplay
{
	public class CharacterSelectionTests : ZenjectIntegrationTestFixture
	{
		private IGridMap _gridMap;
		private CustomTile _tile00;
		private CustomTile _tile20;

		[UnityTest]
		public IEnumerator WhenOnePlayerSelected_ThenOtherPlayersTileNotActive()
		{
			PrepareTilesWithPlayer();

			Container.Resolve<IPathFinder>().FindDistancesToAllTilesFrom(_tile00);
			IEnumerable<CustomTile> availableTiles = Container.Resolve<IPathFinder>().GetAvailableTilesByDistance(10);
			Container.Resolve<ITileSelector>().Select(_tile00, availableTiles);
			yield return new WaitForSeconds(0.1f);
			Assert.IsTrue(_tile00.Active);
			Assert.IsFalse(_tile20.Active);

			Container.Resolve<IPathFinder>().FindDistancesToAllTilesFrom(_tile20);
			availableTiles = Container.Resolve<IPathFinder>().GetAvailableTilesByDistance(10);
			Container.Resolve<ITileSelector>().Select(_tile20, availableTiles);
			yield return new WaitForSeconds(0.1f);
			Assert.IsTrue(_tile20.Active);
			Assert.IsFalse(_tile00.Active);
		}
		
		[UnityTest]
		public IEnumerator WhenPlayersInstantiated_ThenTheyCanBeSelected()
		{
			PrepareTilesWithPlayer();
			
			yield return new WaitForSeconds(0.1f);

			_tile00.GetComponent<Collider>().enabled = false;
			_tile20.GetComponent<Collider>().enabled = false;
			
			while (true)
			{
				if (_gridMap.GetTile(0, 0).Active)
				{
					while (true)
					{
						yield return null;
						if (_gridMap.GetTile(2, 0).Active)
						{
							Assert.Pass();
							yield break;
						}
					}
				}

				if (_gridMap.GetTile(2, 0).Active)
				{
					while (true)
					{
						yield return null;
						if (_gridMap.GetTile(0, 0).Active)
						{
							Assert.Pass();
							yield break;
						}
					}
				}
				
				yield return null;
			}
		}

		private void PrepareTilesWithPlayer()
		{
			GameInstaller.Testing = true;

			Runtime.Gameplay.Battle.Prototype.ArrayGridMap.GridArray = new[,]
			{
				{ 1, 0, 1 }
			};

			var cameraMover = AssetDatabase.LoadAssetAtPath<CameraMover>("Assets/Prefabs/Battle/CameraMover.prefab");
			var cube = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Cube.prefab");
			var spawnSetup = AssetDatabase.LoadAssetAtPath<Setup>("Assets/Prefabs/Battle/SpawnSetup.prefab");
			var lineRendererPrefab = AssetDatabase.LoadAssetAtPath<LineRenderer>("Assets/Prefabs/Battle/Path.prefab");
			Object.Instantiate(cube);

			PreInstall();

			Container.BindFactory<CustomTile, TilemapFactory>().AsSingle();
			Container.Bind<IGridMap>().To<Runtime.Gameplay.Battle.Prototype.ArrayGridMap>().FromNew().AsSingle();
			Container.Bind<IMovementService>().To<TileMovementService>().FromNew().AsSingle();
			Container.Bind<CameraMover>().FromComponentInNewPrefab(cameraMover).AsSingle();
			Container.Bind<ITilesRenderingAggregator>().To<TilesRenderingAggregator>().FromNew().AsSingle();
			Container.Bind<IPathFinder>().To<BellmanFordPathFinder>().FromNew().AsSingle();
			Container.BindInterfacesTo<CustomTileSelector>().FromNew().AsSingle();
			Container.BindInterfacesTo<PathSelector>().FromNew().AsSingle();
			Container.BindInterfacesTo<PathLineRenderer>().FromNew().AsSingle();
			Container.Bind<PlayerFactory>().FromNew().AsSingle();
			Container.Bind<Setup>().FromInstance(spawnSetup).AsSingle();
			Container.Bind<Pool<LineRenderer>>().FromMethod(_ => new Pool<LineRenderer>(Container, lineRendererPrefab.gameObject, 5)).AsSingle();

			PostInstall();

			_gridMap = Container.Resolve<IGridMap>();
			_gridMap.GenerateMap();
			
			_tile00 = _gridMap.GetTile(0, 0);
			var playerSlot0 = Container.InstantiateComponent<PlayerSpawnTile>(_tile00.gameObject);
			_tile20 = _gridMap.GetTile(2, 0);
			var playerSlot1 = Container.InstantiateComponent<PlayerSpawnTile>(_tile20.gameObject);
			
			Container.Resolve<Setup>().InitPlayers(new[] { playerSlot0, playerSlot1 });
			
			foreach (PlayerSpawnTile slot in Container.Resolve<Setup>().PlayerSpawnersForTest)
				Container.Resolve<PlayerFactory>().Create(slot); 
		}
	}
}