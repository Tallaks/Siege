using System.Collections.Generic;
using System.Linq;
using Kulinaria.Siege.Runtime.Debugging.Logging;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Grid;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Path;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Tiles;
using Kulinaria.Siege.Runtime.Infrastructure.Inputs;
using UnityEngine;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Selection
{
	public class CustomTileSelector : ITileSelector
	{
		private readonly ILoggerService _loggerService;
		private readonly IInputService _inputService;
		private readonly IGridMap _map;
		private readonly IPathFinder _pathFinder;
		private readonly CameraMover _cameraMover;

		public CustomTileSelector(ILoggerService loggerService, IInputService inputService, IGridMap map, IPathFinder pathFinder, CameraMover cameraMover)
		{
			_loggerService = loggerService;
			_inputService = inputService;
			_map = map;
			_pathFinder = pathFinder;
			_cameraMover = cameraMover;
		}

		public void Initialize() => 
			_inputService.OnClick += RegisterClick;

		public void Dispose() => 
			_inputService.OnClick -= RegisterClick;

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

		private void RegisterClick(Vector2 clickPos)
		{
			Ray ray = _cameraMover.Camera.ScreenPointToRay(clickPos);
			if (Physics.Raycast(ray, out RaycastHit hit))
				if (hit.transform.GetComponent<CustomTile>())
					Select(hit.transform.GetComponent<CustomTile>(), 10);
		}
	}
}