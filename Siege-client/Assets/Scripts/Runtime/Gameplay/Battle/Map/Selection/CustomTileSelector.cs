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
		private readonly IPathSelector _pathSelector;
		private readonly CameraMover _cameraMover;

		private CustomTile _lastSelectedTile; 
		
		public CustomTileSelector(
			ILoggerService loggerService,
			IInputService inputService,
			IGridMap map,
			IPathFinder pathFinder,
			IPathSelector pathSelector,
			CameraMover cameraMover)
		{
			_loggerService = loggerService;
			_inputService = inputService;
			_map = map;
			_pathFinder = pathFinder;
			_pathSelector = pathSelector;
			_cameraMover = cameraMover;
		}

		public void Initialize()
		{
			_loggerService.Log("Tile selector initialization", LoggerLevel.Battle);
			_inputService.OnClick += RegisterClick;
		}

		public void Dispose() => 
			_inputService.OnClick -= RegisterClick;

		public void Select(CustomTile tile, IEnumerable<CustomTile> availableTiles)
		{
			_lastSelectedTile = tile;
			_pathSelector.SelectFirstTile(tile);
			
			foreach (CustomTile customTile in _map.AllTiles)
				customTile.Active = false;

			foreach (CustomTile customTile in availableTiles)
				customTile.Active = true;
			
			foreach (CustomTile customTile in _map.AllTiles.Where(k => k.Active))
				customTile.Renderer.Repaint();
		}
		
		public void Deselect()
		{
			_loggerService.Log("Deselect tile");
			_lastSelectedTile = null;
			_pathSelector.Deselect();

			foreach (CustomTile customTile in _map.AllTiles)
				customTile.Active = false;
			
			foreach (CustomTile customTile in _map.AllTiles.Where(k => k.Active))
				customTile.Renderer.Repaint();
		}

		private void CalculatePath(CustomTile tile, int distancePoints, out IEnumerable<CustomTile> availableTiles)
		{
			_pathFinder.FindDistancesToAllTilesFrom(tile);
			availableTiles = _pathFinder.GetAvailableTilesByDistance(distancePoints);
		}

		private void RegisterClick(Vector2 clickPos)
		{
			Ray ray = _cameraMover.Camera.ScreenPointToRay(clickPos);
			if (Physics.Raycast(ray, out RaycastHit hit))
			{
				var tileSelectable = hit.transform.GetComponent<ITileSelectable>();
				if (tileSelectable != null && tileSelectable.Tile.HasVisitor)
				{
					if (_lastSelectedTile != tileSelectable.Tile && _pathSelector.HasFirstSelectedTile)
					{
						if (_pathSelector.HasPath)
							SelectVisitor(tileSelectable);
						else
							DrawPathFromSelectedTo(tileSelectable);
					}
					else
						SelectVisitor(tileSelectable);
				}
				else if (tileSelectable != null && _pathSelector.HasFirstSelectedTile)
					DrawPathFromSelectedTo(tileSelectable);
				else
					Deselect();
			}
			else
				Deselect();
		}

		private void DrawPathFromSelectedTo(ITileSelectable tileSelectable)
		{
			_loggerService.Log("Draw path between two tiles");
			_pathSelector.SelectSecondTile(tileSelectable.Tile);
		}

		private void SelectVisitor(ITileSelectable tileSelectable)
		{
			_loggerService.Log("Select visitor to draw path");
			_pathSelector.Deselect();
			CalculatePath(tileSelectable.Tile, tileSelectable.Visitor.ActionPoints, out IEnumerable<CustomTile> availableTiles);
			Select(tileSelectable.Tile, availableTiles);
		}
	}
}