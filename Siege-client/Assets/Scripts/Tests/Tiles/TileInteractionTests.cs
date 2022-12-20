using System.Collections;
using System.Linq;
using Kulinaria.Siege.Runtime.Gameplay.Battle;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Characters.Config;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Characters.Factory;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Characters.Players;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Grid;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Selection;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Tiles;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Spawn;
using Kulinaria.Siege.Runtime.Infrastructure.Inputs;
using Kulinaria.Siege.Runtime.Infrastructure.ZenjectInstallers;
using Kulinaria.Siege.Tests.TestInfrastructure.Installers;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;
using Zenject;
using PoolsInstaller = Kulinaria.Siege.Tests.TestInfrastructure.Installers.PoolsInstaller;
using TilemapInstaller = Kulinaria.Siege.Tests.TestInfrastructure.Installers.TilemapInstaller;

namespace Kulinaria.Siege.Tests.Tiles
{
	public class TileInteractionTests : ZenjectIntegrationTestFixture
	{
		private IGridMap _gridMap;
		private SceneContext _sceneContext1;
		private bool _clickRegistered;

		[UnityTest]
		public IEnumerator WhenUserClicksOnTile_ThenTileSelected()
		{
			PrepareTilesWithoutPlayer();

			Container.Resolve<IInputService>().OnClick += clickPos =>
				RegisterClick(Container.Resolve<CameraMover>(), clickPos);

			yield return new WaitForSeconds(0.1f);

			while (true)
			{
				if (_clickRegistered)
				{
					Assert.Pass();
					break;
				}

				yield return null;
			}
		}

		[UnityTest]
		public IEnumerator WhenUserClicksOnPlayerTile_ThenItIsSelected()
		{
			PrepareTilesWithPlayer();

			yield return new WaitForSeconds(0.1f);

			while (true)
			{
				if (_gridMap.GetTile(0, 0).Active)
				{
					Assert.Pass();
					break;
				}

				yield return null;
			}
		}

		[UnityTest]
		public IEnumerator WhenUserClicksOnPlayerTileAndThenOnEmptyTile_ThenItIsNotSelected()
		{
			PrepareTilesWithPlayer();

			yield return new WaitForSeconds(0.1f);

			while (true)
			{
				if (_gridMap.GetTile(0, 0).Active)
				{
					while (true)
					{
						yield return null;
						if (!_gridMap.GetTile(0, 0).Active)
						{
							Assert.Pass();
							yield break;
						}
					}
				}

				yield return null;
			}
		}

		[UnityTest]
		public IEnumerator WhenUserClicksOnPlayerTileAndThenOnEmptyTileAndAgainOnPlayer_ThenItIsAgainSelected()
		{
			PrepareTilesWithPlayer();

			yield return new WaitForSeconds(0.1f);

			while (true)
			{
				if (_gridMap.GetTile(0, 0).Active)
				{
					while (true)
					{
						yield return null;
						if (!_gridMap.GetTile(0, 0).Active)
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
				{ 1 }
			};

			var cube = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Cube.prefab");

			var tilemapInstaller = new TilemapInstaller();
			tilemapInstaller.PreInstall();

			var charactersInstaller = new CharactersInstaller();
			charactersInstaller.PreInstall();

			var gameplayInstaller = new TestInfrastructure.Installers.GameplayInstaller();
			gameplayInstaller.PreInstall();

			var poolsInstaller = new PoolsInstaller();
			poolsInstaller.PreInstall();

			Object.Instantiate(cube);

			PreInstall();

			tilemapInstaller.Install(Container);
			charactersInstaller.Install(Container);
			gameplayInstaller.Install(Container);
			poolsInstaller.Install(Container);

			PostInstall();

			_gridMap = Container.Resolve<IGridMap>();
			_gridMap.GenerateMap();

			CustomTile tile = _gridMap.GetTile(0, 0);
			var playerSlot = Container.InstantiateComponent<PlayerSpawnTile>(tile.gameObject);
			var playerConfig0 = Resources.Load<PlayerConfig>("Configs/Characters/Players/Doctor");

			Container.Resolve<Setup>().InitPlayers(new[]
			{
				new PlayerSlot { Player = playerConfig0, Spawn = playerSlot }
			});

			foreach (PlayerSlot slot in Container.Resolve<Setup>().PlayerSpawnersForTest)
				Container.Resolve<PlayerFactory>().Create(slot);
			BasePlayer player = Container.Resolve<PlayerFactory>().
				Create(Container.Resolve<Setup>().PlayerSpawnersForTest.First());

			player.Interaction.GetComponent<Collider>().enabled = false;
		}

		private void PrepareTilesWithoutPlayer()
		{
			GameInstaller.Testing = true;

			Runtime.Gameplay.Battle.Prototype.ArrayGridMap.GridArray = new[,]
			{
				{ 1 }
			};

			var cube = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Cube.prefab");

			var tilemapInstaller = new TilemapInstaller();
			tilemapInstaller.PreInstall();

			var charactersInstaller = new CharactersInstaller();
			charactersInstaller.PreInstall();

			var gameplayInstaller = new TestInfrastructure.Installers.GameplayInstaller();
			gameplayInstaller.PreInstall();

			var poolsInstaller = new PoolsInstaller();
			poolsInstaller.PreInstall();

			Object.Instantiate(cube);

			PreInstall();

			tilemapInstaller.Install(Container);
			charactersInstaller.Install(Container);
			gameplayInstaller.Install(Container);
			poolsInstaller.Install(Container);

			PostInstall();

			_gridMap = Container.Resolve<IGridMap>();
			_gridMap.GenerateMap();
		}

		private void RegisterClick(CameraMover cameraMover, Vector2 clickPos)
		{
			Ray ray = cameraMover.Camera.ScreenPointToRay(clickPos);
			if (Physics.Raycast(ray, out RaycastHit hit))
			{
				if (hit.transform.GetComponent<IInteractable>() != null)
					_clickRegistered = true;
			}
		}
	}
}