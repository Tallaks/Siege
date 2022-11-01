using Kulinaria.Siege.Runtime.Gameplay.Battle.Movement;
using Zenject;

namespace Kulinaria.Siege.Runtime.Infrastructure.ZenjectInstallers
{
	public class BattleInstaller : MonoInstaller, IInitializable
	{
		public override void InstallBindings()
		{
			Container
				.Bind<IInitializable>()
				.To<BattleInstaller>()
				.FromInstance(this)
				.AsSingle();
			
			Container
				.Bind<IMovementService>()
				.To<TileMovementService>()
				.FromNew()
				.AsSingle();
			
			Container
				.BindFactory<CustomTile, TilemapFactory>()
				.AsSingle();
		}

		public void Initialize()
		{
		}
	}
}