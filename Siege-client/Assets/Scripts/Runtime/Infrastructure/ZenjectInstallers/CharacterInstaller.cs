using Kulinaria.Siege.Runtime.Gameplay.Battle.Characters.Factory;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Characters.Players;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Characters.Registry;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Spawn;
using UnityEngine;
using Zenject;

namespace Kulinaria.Siege.Runtime.Infrastructure.ZenjectInstallers
{
	public class CharacterInstaller : MonoInstaller, IInitializable
	{
		[SerializeField] private Setup _spawnSetup;
		
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
				.Bind<PlayerFactory>()
				.FromNew()
				.AsSingle();
			
			Container
				.Bind<ICharacterRegistry>()
				.To<CharacterRegistry>()
				.FromNew()
				.AsSingle();
		}

		public void Initialize()
		{
			foreach (PlayerSlot playerSlot in Container.Resolve<Setup>().PlayerSlots)
			{
				BasePlayer player = Container.Resolve<PlayerFactory>().Create(playerSlot);
				Container.Resolve<ICharacterRegistry>().RegisterPlayer(player);
			}
		}
	}
}