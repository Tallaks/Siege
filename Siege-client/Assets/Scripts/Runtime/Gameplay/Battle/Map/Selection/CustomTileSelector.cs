using System.Collections.Generic;
using System.Linq;
using Kulinaria.Siege.Runtime.Debugging.Logging;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Grid;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Path;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Tiles;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Selection
{
	public class CustomTileSelector : ITileSelector
	{
		private readonly ILoggerService _loggerService;
		private readonly IGridMap _map;
		private readonly IPathFinder _pathFinder;

		public CustomTileSelector(ILoggerService loggerService, IGridMap map, IPathFinder pathFinder)
		{
			_loggerService = loggerService;
			_map = map;
			_pathFinder = pathFinder;
		}
		
		public void Select(CustomTile tile, int distancePoints)
		{
			_pathFinder.FindDistancesToAllTilesFrom(tile);
			IEnumerable<CustomTile> availableTiles = _pathFinder.GetAvailableTilesByDistance(distancePoints);

			foreach (CustomTile customTile in _map.AllTiles)
				customTile.Active = false;

			foreach (CustomTile customTile in availableTiles)
				customTile.Active = true;
			
			foreach (CustomTile customTile in _map.AllTiles.Where(k => k.Active))
				customTile.Renderer.Repaint();
		}
	}
}