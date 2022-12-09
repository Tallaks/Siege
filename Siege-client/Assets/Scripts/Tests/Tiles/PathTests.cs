using System.Collections;
using System.Linq;
using Kulinaria.Siege.Runtime.Extensions;
using Kulinaria.Siege.Runtime.Gameplay.Battle;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Characters.Factory;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Characters.Players;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Characters.Registry;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Grid;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Path;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Selection;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Tiles;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Tiles.Rendering;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Movement;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Spawn;
using Kulinaria.Siege.Runtime.Infrastructure.Inputs;
using Kulinaria.Siege.Runtime.Infrastructure.ZenjectInstallers;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;
using Zenject;

namespace Kulinaria.Siege.Tests.Tiles
{
	public class PathTests : ZenjectIntegrationTestFixture
	{
		private CameraMover _cameraMover;
		private Setup _spawnSetup;
		private IGridMap _gridMap;
		private BasePlayer _player;
		private ICharacterRegistry _registry;
		private ITileSelector _tileSelector;
		private IPathFinder _pathFinder;
		private IPathSelector _pathSelector;
		private IPathRenderer _pathRenderer;

		[UnityTest]
		public IEnumerator WhenMouseOverTileAfterTileSelected_ThenLineRendererExists()
		{
			PrepareTilesWithOneVisitor(new[,]
			{
				{ 1, 1, 1, 1, 1 }
			});
			yield return WaitForMouseOverSecondTile();

			Assert.NotNull(Object.FindObjectOfType<LineRenderer>());
		}

		[UnityTest]
		public IEnumerator WhenMouseOverTileAfterTileSelected_ThenPathIsRed()
		{
			PrepareTilesWithOneVisitor(new[,]
			{
				{ 1, 1, 1, 1, 1 }
			});
			yield return WaitForMouseOverSecondTile();

			var lineRenderer = Object.FindObjectOfType<LineRenderer>();
			Assert.IsTrue(lineRenderer.startColor == Color.red);
			Assert.IsTrue(lineRenderer.endColor == Color.red);
		}

		[UnityTest]
		public IEnumerator WhenMouseOverTileAfterTileSelected_ThenPathIsGreen()
		{
			PrepareTilesWithOneVisitor(new[,]
			{
				{ 1, 1, 1, 1, 1 }
			});
			_registry.ChangeActionPointsForAll(100);
			yield return WaitForMouseOverSecondTile();

			var lineRenderer = Object.FindObjectOfType<LineRenderer>();
			Assert.IsTrue(lineRenderer.startColor == Color.green);
			Assert.IsTrue(lineRenderer.endColor == Color.green);
		}
		
		[UnityTest]
		public IEnumerator WhenMouseOverTileAfterTileSelected_ThenPathIsGreenAndRed()
		{
			PrepareTilesWithOneVisitor(new[,]
			{
				{ 1, 1, 1, 1, 1 }
			});
			_registry.ChangeActionPointsForAll(2);
			yield return WaitForMouseOverThirdTile();

			LineRenderer[] lineRenderers = Object.FindObjectsOfType<LineRenderer>();
			LineRenderer firstLine = lineRenderers.
				First(k => k.GetPosition(0) == _gridMap.GetTile(0, 0).CellPosition.ToWorld());
			LineRenderer lastLine = lineRenderers.
				First(k => k.GetPosition(0) == _gridMap.GetTile(1, 0).CellPosition.ToWorld());
			
			Assert.IsTrue(firstLine.startColor == Color.green);
			Assert.IsTrue(firstLine.endColor == Color.green);
			Assert.IsTrue(lastLine.startColor == Color.red);
			Assert.IsTrue(lastLine.endColor == Color.red);
		}

		[UnityTest]
		public IEnumerator WhenMouseOverTileAfterTileSelected_ThenPathNotExist()
		{
			PrepareTilesWithOneVisitor(new[,]
			{
				{ 1, 0, 1, 1, 1 }
			});
			_registry.ChangeActionPointsForAll(2);
			yield return WaitForMouseOverThirdTile();

			Assert.Null(Object.FindObjectOfType<LineRenderer>());
		}
		
		private void PrepareTilesWithOneVisitor(int[,] array)
		{
			GameInstaller.Testing = true;

			Runtime.Gameplay.Battle.Prototype.ArrayGridMap.GridArray = array;

			_cameraMover = AssetDatabase.LoadAssetAtPath<CameraMover>("Assets/Prefabs/Battle/CameraMover.prefab");
			_spawnSetup = AssetDatabase.LoadAssetAtPath<Setup>("Assets/Prefabs/Battle/SpawnSetup.prefab");

			PreInstall();

			Container.BindFactory<CustomTile, TilemapFactory>().AsSingle();
			Container.Bind<IGridMap>().To<Runtime.Gameplay.Battle.Prototype.ArrayGridMap>().FromNew().AsSingle();
			Container.Bind<IMovementService>().To<TileMovementService>().FromNew().AsSingle();
			Container.Bind<CameraMover>().FromComponentInNewPrefab(_cameraMover).AsSingle();
			Container.Bind<ITilesRenderingAggregator>().To<TilesRenderingAggregator>().FromNew().AsSingle();
			Container.Bind<IPathFinder>().To<BellmanFordPathFinder>().FromNew().AsSingle();
			Container.BindInterfacesTo<CustomTileSelector>().FromNew().AsSingle();
			Container.BindInterfacesTo<PathSelector>().FromNew().AsSingle();
			Container.BindInterfacesTo<PathLineRenderer>().FromNew().AsSingle();
			Container.Bind<PlayerFactory>().FromNew().AsSingle();
			Container.Bind<ICharacterRegistry>().To<CharacterRegistry>().FromNew().AsSingle();
			Container.Bind<Setup>().FromInstance(_spawnSetup).AsSingle();

			PostInstall();

			_gridMap = Container.Resolve<IGridMap>();
			_registry = Container.Resolve<ICharacterRegistry>();
			_gridMap.GenerateMap();

			CustomTile tile = _gridMap.GetTile(0, 0);
			var playerSlot = Container.InstantiateComponent<PlayerSlot>(tile.gameObject);
			Container.Resolve<Setup>().InitPlayers(new[] { playerSlot });

			foreach (PlayerSlot slot in Container.Resolve<Setup>().PlayerSlots)
			{
				_player = Container.Resolve<PlayerFactory>().Create(slot);
				_registry.RegisterPlayer(_player);
			}

			_gridMap = Container.Resolve<IGridMap>();
			_tileSelector = Container.Resolve<ITileSelector>();
			_pathFinder = Container.Resolve<IPathFinder>();
			_pathRenderer = Container.Resolve<IPathRenderer>();
			_pathSelector = Container.Resolve<IPathSelector>();
		}

		private bool MouseOverTile(CustomTile tile)
		{
			Ray ray = _cameraMover.Camera.ScreenPointToRay(Container.Resolve<IInputService>().PointPosition);
			if (Physics.Raycast(ray, out RaycastHit hit))
			{
				var tileSelectable = hit.transform.GetComponent<ITileSelectable>();
				return tile == tileSelectable.Tile;
			}

			return false;
		}

		private IEnumerator WaitForMouseOverSecondTile()
		{
			while (true)
			{
				if (_gridMap.GetTile(0, 0).Active)
					if (MouseOverTile(_gridMap.GetTile(1, 0)))
						break;

				yield return null;
			}
		}

		private IEnumerator WaitForMouseOverThirdTile()
		{	
			while (true)
			{
				if (_gridMap.GetTile(0, 0).Active)
					if (MouseOverTile(_gridMap.GetTile(2, 0)))
						break;

				yield return null;
			}
		}
	}
}