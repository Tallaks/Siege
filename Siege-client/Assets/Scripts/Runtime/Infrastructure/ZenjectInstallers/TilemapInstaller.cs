using Kulinaria.Siege.Runtime.Debugging.Logging;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Level.Tiles;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Level.Tiles.Rendering;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Movement;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Prototype;
using UnityEngine;
using Zenject;

namespace Kulinaria.Siege.Runtime.Infrastructure.ZenjectInstallers
{
	public class TilemapInstaller : MonoInstaller, IInitializable
	{
		[SerializeField] 
		private Transform _tilemapTransform;

		private ILoggerService _loggerService;

		[Inject]
		private void Construct(ILoggerService loggerService) => 
			_loggerService = loggerService;

		public void Initialize()
		{
			_loggerService.Log("Tilemap Initialization", LoggerLevel.Battle);
			
			ArrayGridMap.GridArray = new[,]
			{
				{ 1, 1, 1, 1, 1, 1, 1, 1 },
				{ 1, 1, 1, 1, 1, 1, 1, 1 },
				{ 1, 1, 1, 0, 0, 1, 1, 1 },
				{ 1, 1, 1, 0, 0, 0, 1, 1 },
				{ 1, 1, 1, 1, 0, 0, 1, 1 },
				{ 1, 1, 1, 1, 1, 1, 1, 1 },
				{ 1, 1, 1, 1, 1, 1, 1, 1 },
				{ 1, 1, 1, 1, 1, 1, 1, 0 },
				{ 0, 0, 0, 0, 0, 0, 1, 0 },
				{ 0, 1, 1, 1, 1, 1, 1, 0 },
				{ 0, 1, 0, 0, 1, 1, 1, 1 },
				{ 0, 1, 0, 0, 1, 1, 1, 1 },
				{ 0, 1, 1, 1, 1, 1, 1, 1 },
				{ 0, 1, 1, 1, 1, 1, 1, 1 },
				{ 0, 1, 1, 1, 1, 1, 1, 1 }
			};

			Container.Resolve<TilemapFactory>().Initialize(_tilemapTransform);
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
				.BindFactory<CustomTile, TilemapFactory>()
				.AsSingle();

			Container
				.Bind<IGridMap>()
				.To<ArrayGridMap>()
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