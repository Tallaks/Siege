using System.Collections;
using System.Linq;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Characters.Factory;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Characters.Players;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Characters.Registry;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Spawn;
using Kulinaria.Siege.Runtime.Infrastructure.Constants;
using Kulinaria.Siege.Runtime.Infrastructure.ZenjectInstallers;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using Zenject;

namespace Kulinaria.Siege.Tests.Gameplay
{
	[TestFixture]
	public class CharacterTests
	{
		private ICharacterRegistry _characterRegistry;
		private EnemyFactory _enemyFactory;
		private PlayerFactory _playerFactory;
		private Setup _spawnSetup;

		[UnityTest]
		public IEnumerator WhenBattleSceneLoaded_ThenCharacterServicesInstalled()
		{
			Assert.NotNull(_playerFactory);
			Assert.NotNull(_enemyFactory);
			Assert.NotNull(_spawnSetup);
			Assert.NotNull(_characterRegistry);
			yield break;
		}

		[UnityTest]
		public IEnumerator WhenBattleSceneLoaded_ThenItHasPlayerSlots()
		{
			Assert.NotZero(Object.FindObjectsOfType<PlayerSpawnTile>().Length);
			Assert.NotZero(_spawnSetup.PlayerSlots.Count());
			Assert.AreEqual(Object.FindObjectsOfType<PlayerSpawnTile>().Length, _spawnSetup.PlayerSlots.Count());
			yield break;
		}

		[UnityTest]
		public IEnumerator WhenBattleSceneLoaded_ThenPlayersSpawnedCorrectly()
		{
			Assert.NotZero(Object.FindObjectsOfType<BasePlayer>().Length);
			Assert.AreEqual(Object.FindObjectsOfType<BasePlayer>().Length, _spawnSetup.PlayerSlots.Count());
			yield break;
		}

		[UnitySetUp]
		public IEnumerator LoadSceneAndReferences()
		{
			ApplicationInstaller.Testing = true;
			yield return SceneManager.LoadSceneAsync(SceneNames.BattleScene);

			DiContainer diContainer = Object.FindObjectOfType<SceneContext>().Container;
			_spawnSetup = diContainer.Resolve<Setup>();
			_playerFactory = diContainer.Resolve<PlayerFactory>();
			_enemyFactory = diContainer.Resolve<EnemyFactory>();
			_characterRegistry = diContainer.Resolve<ICharacterRegistry>();
		}
	}
}