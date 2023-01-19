using System.Collections;
using System.Linq;
using Kulinaria.Siege.Runtime.Extensions;
using Kulinaria.Siege.Runtime.Gameplay.Battle;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Characters.Config;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Characters.Factory;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Characters.Players;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Characters.Registry;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Grid;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Path;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Selection;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Tiles;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Spawn;
using Kulinaria.Siege.Runtime.Infrastructure.Inputs;
using Kulinaria.Siege.Runtime.Infrastructure.ZenjectInstallers;
using Kulinaria.Siege.Tests.TestInfrastructure.Installers;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Zenject;
using PoolsInstaller = Kulinaria.Siege.Tests.TestInfrastructure.Installers.PoolsInstaller;
using TilemapInstaller = Kulinaria.Siege.Tests.TestInfrastructure.Installers.TilemapInstaller;

namespace Kulinaria.Siege.Tests.Path
{
	public class PathRenderingTests : ZenjectIntegrationTestFixture
	{
		private Setup _spawnSetup;
		private IGridMap _gridMap;
		private BasePlayer _player;
		private ICharacterRegistry _registry;
		private IInputService _inputService;
		private int _clickCount;

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

			_registry.ChangeActionPointsForAll(0);
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

		[UnityTest]
		public IEnumerator WhenTwoTilesSelected_ThenThePathIsGreen()
		{
			PrepareTilesWithOneVisitor(new[,]
			{
				{ 1, 1, 1, 1, 1 }
			});
			_registry.ChangeActionPointsForAll(2);
			yield return WaitForTwoTilesSelected();

			var firstLine = Object.FindObjectOfType<LineRenderer>();

			Assert.IsTrue(firstLine.startColor == Color.green);
			Assert.IsTrue(firstLine.endColor == Color.green);
		}

		[UnityTest]
		public IEnumerator WhenTwoTilesSelected_ThenThePathIsRed()
		{
			PrepareTilesWithOneVisitor(new[,]
			{
				{ 1, 1, 1, 1, 1 }
			});
			_registry.ChangeActionPointsForAll(0);
			yield return WaitForTwoTilesSelected();

			var firstLine = Object.FindObjectOfType<LineRenderer>();

			Assert.IsTrue(firstLine.startColor == Color.red);
			Assert.IsTrue(firstLine.endColor == Color.red);
		}

		[UnityTest]
		public IEnumerator WhenTwoTilesSelected_ThenThePathIsRedAndGreen()
		{
			PrepareTilesWithOneVisitor(new[,]
			{
				{ 1, 1, 1, 1, 1 }
			});
			_registry.ChangeActionPointsForAll(2);
			yield return WaitForTwoTilesSelected();

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
		public IEnumerator WhenTwoTileSelected_ThenPathNotExist()
		{
			PrepareTilesWithOneVisitor(new[,]
			{
				{ 1, 0, 1, 1, 1 }
			});
			_registry.ChangeActionPointsForAll(2);
			yield return WaitForTwoTilesSelected();

			Assert.Null(Object.FindObjectOfType<LineRenderer>());
		}

		private void PrepareTilesWithOneVisitor(int[,] array)
		{
			ApplicationInstaller.Testing = true;

			Runtime.Gameplay.Battle.Prototype.ArrayGridMap.GridArray = array;

			var tilemapInstaller = new TilemapInstaller();
			tilemapInstaller.PreInstall();

			var charactersInstaller = new CharactersInstaller();
			charactersInstaller.PreInstall();

			var gameplayInstaller = new TestInfrastructure.Installers.GameplayInstaller();
			gameplayInstaller.PreInstall();

			var poolsInstaller = new PoolsInstaller();
			poolsInstaller.PreInstall();

			_clickCount = 0;

			PreInstall();

			tilemapInstaller.Install(Container);
			charactersInstaller.Install(Container);
			gameplayInstaller.Install(Container);
			poolsInstaller.Install(Container);

			PostInstall();

			_gridMap = Container.Resolve<IGridMap>();
			_registry = Container.Resolve<ICharacterRegistry>();
			_gridMap.GenerateMap();

			CustomTile tile = _gridMap.GetTile(0, 0);
			var playerSlot = Container.InstantiateComponent<PlayerSpawnTile>(tile.gameObject);
			var playerConfig0 = Resources.Load<PlayerConfig>("Configs/Characters/Players/Doctor");
			Container.Resolve<Setup>().InitPlayers(new[]
			{
				new PlayerSlot { Spawn = playerSlot, Player = playerConfig0 }
			});

			foreach (PlayerSlot slot in Container.Resolve<Setup>().PlayerSpawnersForTest)
			{
				_player = Container.Resolve<PlayerFactory>().Create(slot);
				_registry.RegisterPlayer(_player);
			}

			_gridMap = Container.Resolve<IGridMap>();
			Container.Resolve<IClickInteractor>();
			Container.Resolve<IPathFinder>();
			Container.Resolve<IPathRenderer>();
			Container.Resolve<IPathSelector>();
			_inputService = Container.Resolve<IInputService>();
			_inputService.OnClick += _ => _clickCount++;
		}

		private bool MouseOverTile(CustomTile tile)
		{
			Ray ray = Container.Resolve<CameraMover>().Camera.ScreenPointToRay(_inputService.PointPosition);
			if (Physics.Raycast(ray, out RaycastHit hit))
			{
				var tileSelectable = hit.transform.GetComponent<IInteractable>();
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

		private IEnumerator WaitForTwoTilesSelected()
		{
			while (true)
			{
				yield return null;
				if (_clickCount == 2)
					break;
			}
		}
	}
}