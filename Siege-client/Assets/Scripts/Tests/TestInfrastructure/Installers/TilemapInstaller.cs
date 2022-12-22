using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Grid;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Path;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Selection;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Tiles;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Tiles.Rendering;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Movement;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Prototype;
using Zenject;

namespace Kulinaria.Siege.Tests.TestInfrastructure.Installers
{
	public class TilemapInstaller : IInstaller
	{
		public void PreInstall(params object[] args)
		{
		}

		public void Install(DiContainer container)
		{
			container.BindFactory<CustomTile, TilemapFactory>().AsSingle();
			container.Bind<IGridMap>().To<ArrayGridMap>().FromNew().AsSingle();
			container.Bind<ITilesRenderingAggregator>().To<TilesRenderingAggregator>().FromNew().AsSingle();
			container.Bind<IMovementService>().To<TileMovementService>().FromNew().AsSingle();
			container.Bind<IPathFinder>().To<BellmanFordPathFinder>().FromNew().AsSingle();
			container.BindInterfacesTo<GridmapInteractor>().FromNew().AsSingle();
			container.BindInterfacesTo<PathSelector>().FromNew().AsSingle();
			container.BindInterfacesTo<PathLineRenderer>().FromNew().AsSingle();
			container.Bind<IDeselectService>().To<DeselectService>().FromNew().AsSingle();
			container.Bind<ITileActivator>().To<TileActivator>().FromNew().AsSingle();
		}
	}
}