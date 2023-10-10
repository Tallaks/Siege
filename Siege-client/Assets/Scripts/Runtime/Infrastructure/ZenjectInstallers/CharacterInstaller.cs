using Kulinaria.Siege.Runtime.Debugging.Logging;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Characters.Enemies;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Characters.Factory;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Characters.Players;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Characters.Registry;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Spawn;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Kulinaria.Siege.Runtime.Infrastructure.ZenjectInstallers
{
	public class CharacterInstaller : MonoInstaller, IInitializable
	{
		[SerializeField, Required] private Setup _spawnSetup;

		private ILoggerService _loggerService;

		[Inject]
		private void Construct(ILoggerService loggerService)
		{
			_loggerService = loggerService;
		}

		public void Initialize()
		{
			_loggerService.Log("Characters Initialization", LoggerLevel.Battle);

			foreach (PlayerSlot playerSlot in Container.Resolve<Setup>().PlayerSlots)
			{
				BasePlayer player = Container.Resolve<PlayerFactory>().Create(playerSlot);
				Container.Resolve<ICharacterRegistry>().RegisterPlayer(player);
			}

			foreach (EnemySlot enemySlot in Container.Resolve<Setup>().EnemySlots)
			{
				BaseEnemy enemy = Container.Resolve<EnemyFactory>().Create(enemySlot);
				Container.Resolve<ICharacterRegistry>().RegisterEnemy(enemy);
			}
		}

		public override void InstallBindings()
		{
			Container
				.Bind<IInitializable>()
				.To<CharacterInstaller>()
				.FromInstance(this)
				.AsSingle();

			Container
				.Bind<Setup>()
				.FromInstance(_spawnSetup)
				.AsSingle();

			Container
				.Bind<EnemyFactory>()
				.FromNew()
				.AsSingle();

			Container
				.Bind<PlayerFactory>()
				.FromNew()
				.AsSingle();

			Container
				.Bind<ICharacterRegistry>()
				.To<CharacterRegistry>()
				.FromNew()
				.AsSingle();
		}
	}
}