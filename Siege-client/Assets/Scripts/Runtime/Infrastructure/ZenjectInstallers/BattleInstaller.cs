using Kulinaria.Siege.Runtime.Gameplay.Battle.Movement;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Prototype;
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
				.BindFactory<CustomTile, TilemapFactory>()
				.AsSingle();

			Container
				.Bind<IGridGenerator>()
				.To<GridGenerator>()
				.FromNew()
				.AsSingle();
			
			Container
				.Bind<IMovementService>()
				.To<TileMovementService>()
				.FromNew()
				.AsSingle();
		}

		public void Initialize()
		{
			var grid1 = new[,]
			{
				{ 1, 1, 1, 1, 0 }, 
				{ 0, 0, 0, 0, 1 }
			};

			Container.Resolve<IGridGenerator>().GenerateMap(grid1);
		}
	}
}