using Kulinaria.Siege.Runtime.Debugging.Logging;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Grid;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Path;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Selection;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Tiles.Rendering;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Movement;
using Zenject;

namespace Kulinaria.Siege.Runtime.Infrastructure.ZenjectInstallers
{
	public class TilemapInstaller : MonoInstaller, IInitializable
	{
		private ILoggerService _loggerService;

		[Inject]
		private void Construct(ILoggerService loggerService) => 
			_loggerService = loggerService;

		public void Initialize()
		{
			_loggerService.Log("Tilemap Initialization", LoggerLevel.Battle);
			Container.Resolve<IGridMap>().GenerateMap();
		}

		public override void InstallBindings()
		{
			Container
				.Bind<IInitializable>()
				.To<TilemapInstaller>()
				.FromInstance(this)
				.AsSingle();

			Container
				.Bind<IGridMap>()
				.To<OnSceneGridMap>()
				.FromNew()
				.AsSingle();

			Container
				.Bind<IMovementService>()
				.To<TileMovementService>()
				.FromNew()
				.AsSingle();

			Container
				.Bind<ITilesRenderingAggregator>()
				.To<TilesRenderingAggregator>()
				.FromNew()
				.AsSingle();

			Container
				.Bind<IPathFinder>()
				.To<BellmanFordPathFinder>()
				.FromNew()
				.AsSingle();

			Container
				.BindInterfacesTo<PathSelector>()
				.FromNew()
				.AsSingle();

			Container
				.BindInterfacesTo<CustomTileSelector>()
				.FromNew()
				.AsSingle();
		}
	}
}