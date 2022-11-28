using Kulinaria.Siege.Runtime.Debugging.Logging;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Level;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Level.Tiles;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Level.Tiles.Rendering;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Movement;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Prototype;
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
		}
	}
}