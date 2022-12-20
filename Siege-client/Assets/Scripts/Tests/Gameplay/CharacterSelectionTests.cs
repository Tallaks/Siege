using System.Collections;
using System.Collections.Generic;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Characters.Config;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Characters.Factory;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Grid;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Path;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Tiles;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Spawn;
using Kulinaria.Siege.Runtime.Infrastructure.ZenjectInstallers;
using Kulinaria.Siege.Tests.TestInfrastructure.Installers;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;
using Zenject;
using GameplayInstaller = Kulinaria.Siege.Tests.TestInfrastructure.Installers.GameplayInstaller;
using PoolsInstaller = Kulinaria.Siege.Tests.TestInfrastructure.Installers.PoolsInstaller;
using TilemapInstaller = Kulinaria.Siege.Tests.TestInfrastructure.Installers.TilemapInstaller;

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
			//Container.Resolve<IClickInteractor>().Select(_tile00, availableTiles);
			yield return new WaitForSeconds(0.1f);
			Assert.IsTrue(_tile00.Active);
			Assert.IsFalse(_tile20.Active);

			Container.Resolve<IPathFinder>().FindDistancesToAllTilesFrom(_tile20);
			availableTiles = Container.Resolve<IPathFinder>().GetAvailableTilesByDistance(10);
			//Container.Resolve<IClickInteractor>().Select(_tile20, availableTiles);
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

			var cube = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Cube.prefab");
			Object.Instantiate(cube);

			var tilemapInstaller = new TilemapInstaller();
			tilemapInstaller.PreInstall();

			var charactersInstaller = new CharactersInstaller();
			charactersInstaller.PreInstall();

			var gameplayInstaller = new GameplayInstaller();
			gameplayInstaller.PreInstall();

			var poolsInstaller = new PoolsInstaller();
			poolsInstaller.PreInstall();

			PreInstall();

			tilemapInstaller.Install(Container);
			charactersInstaller.Install(Container);
			gameplayInstaller.Install(Container);
			poolsInstaller.Install(Container);

			PostInstall();

			_gridMap = Container.Resolve<IGridMap>();
			_gridMap.GenerateMap();

			_tile00 = _gridMap.GetTile(0, 0);
			var playerTile0 = Container.InstantiateComponent<PlayerSpawnTile>(_tile00.gameObject);
			var playerConfig0 = Resources.Load<PlayerConfig>("Configs/Characters/Players/Doctor");

			_tile20 = _gridMap.GetTile(2, 0);
			var playerTile1 = Container.InstantiateComponent<PlayerSpawnTile>(_tile20.gameObject);
			var playerConfig1 = Resources.Load<PlayerConfig>("Configs/Characters/Players/Gregor");

			Container.Resolve<Setup>().InitPlayers(new List<PlayerSlot>
			{
				new() { Player = playerConfig0, Spawn = playerTile0 },
				new() { Player = playerConfig1, Spawn = playerTile1 },
			});

			foreach (PlayerSlot slot in Container.Resolve<Setup>().PlayerSpawnersForTest)
				Container.Resolve<PlayerFactory>().Create(slot);
		}
	}
}