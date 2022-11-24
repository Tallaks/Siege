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
		private PlayerFactory _playerFactory;
		private EnemyFactory _enemyFactory;
		private Setup _spawnSetup;
		private ICharacterRegistry _characterRegistry;

		[UnitySetUp]
		public IEnumerator LoadSceneAndReferences()
		{
			GameInstaller.Testing = true;
			yield return SceneManager.LoadSceneAsync(SceneNames.BattleScene);

			DiContainer diContainer = Object.FindObjectOfType<SceneContext>().Container;
			_spawnSetup = diContainer.Resolve<Setup>();
			_playerFactory = diContainer.Resolve<PlayerFactory>();
			_enemyFactory = diContainer.Resolve<EnemyFactory>();
			_characterRegistry = diContainer.Resolve<ICharacterRegistry>();
		}

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
			Assert.NotZero(Object.FindObjectsOfType<PlayerSlot>().Length);
			Assert.NotZero(_spawnSetup.PlayerSlots.Count());
			Assert.AreEqual(Object.FindObjectsOfType<PlayerSlot>().Length, _spawnSetup.PlayerSlots.Count());
			yield break;
		}

		[UnityTest]
		public IEnumerator WhenBattleSceneLoaded_ThenPlayersSpawnedCorrectly()
		{
			Assert.NotZero(Object.FindObjectsOfType<BasePlayer>().Length);
			Assert.AreEqual(Object.FindObjectsOfType<BasePlayer>().Length, _spawnSetup.PlayerSlots.Count());
			yield break;
		}
	}
}