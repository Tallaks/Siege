using Kulinaria.Siege.Runtime.Gameplay.Battle.Movement;
using Zenject;

namespace Kulinaria.Siege.Runtime.Infrastructure.ZenjectInstallers
{
	public class BattleInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container
				.Bind<IMovementService>()
				.To<TileMovementService>()
				.FromNew()
				.AsSingle();
			
			Container
				.BindFactory<CustomTile, TilemapFactory>()
				.AsSingle();
		}
	}
}