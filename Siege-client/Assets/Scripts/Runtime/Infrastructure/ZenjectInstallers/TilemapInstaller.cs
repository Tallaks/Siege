using Kulinaria.Siege.Runtime.Gameplay.Battle.Movement;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Movement.Tiles;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Movement.Tiles.Rendering;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Prototype;
using UnityEngine;
using Zenject;

namespace Kulinaria.Siege.Runtime.Infrastructure.ZenjectInstallers
{
	public class TilemapInstaller : MonoInstaller, IInitializable
	{
		[SerializeField] 
		private Transform _tilemapTransform;
		
		public void Initialize()
		{
			GridMap.GridArray = new[,]
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
				.To<GridMap>()
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