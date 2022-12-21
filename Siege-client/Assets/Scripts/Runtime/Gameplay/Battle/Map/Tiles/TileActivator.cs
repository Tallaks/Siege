using System.Collections.Generic;
using System.Linq;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Characters;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Grid;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Path;
using UnityEngine;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Tiles
{
	public class TileActivator : ITileActivator
	{
		private readonly IPathFinder _pathFinder;
		private readonly IGridMap _map;

		public TileActivator(IPathFinder pathFinder, IGridMap map)
		{
			Debug.Log(345);
			_pathFinder = pathFinder;
			_map = map;
		}
		
		public void ActivateTilesAround(BaseCharacter character)
		{
			CalculatePath(character.Interaction.Tile, character.MaxAP, out IEnumerable<CustomTile> availableTiles);
			Select(availableTiles);
		}

		public void DeactivateAllTiles()
		{
			foreach (CustomTile customTile in _map.AllTiles)
				customTile.Active = false;

			foreach (CustomTile customTile in _map.AllTiles.Where(k => k.Active))
				customTile.Renderer.Repaint();
		}

		private void Select(IEnumerable<CustomTile> availableTiles)
		{
			foreach (CustomTile customTile in _map.AllTiles)
				customTile.Active = false;

			foreach (CustomTile customTile in availableTiles)
				customTile.Active = true;

			foreach (CustomTile customTile in _map.AllTiles.Where(k => k.Active))
				customTile.Renderer.Repaint();
		}

		private void CalculatePath(CustomTile tile, int distancePoints, out IEnumerable<CustomTile> availableTiles)
		{
			_pathFinder.FindDistancesToAllTilesFrom(tile);
			availableTiles = _pathFinder.GetAvailableTilesByDistance(distancePoints);
		}
	}
}