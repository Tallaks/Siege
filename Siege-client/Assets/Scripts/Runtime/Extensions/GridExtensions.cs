using System.Collections.Generic;
using System.Linq;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Movement.Tiles;
using UnityEngine;

namespace Kulinaria.Siege.Runtime.Extensions
{
	public static class GridExtensions
	{
		public static Vector3 ToWorld(this Vector2Int cellPosition) =>
			new(0.5f + cellPosition.x, 0.1f, 0.5f + cellPosition.y);

		public static IEnumerable<Vector2Int> OfMissingNeighboursPositions(this CustomTile tile)
		{
			Vector2Int[] allPositions =
			{
				new(tile.CellPosition.x - 1, tile.CellPosition.y - 1),
				new(tile.CellPosition.x - 1, tile.CellPosition.y),
				new(tile.CellPosition.x - 1, tile.CellPosition.y + 1),
				new(tile.CellPosition.x, tile.CellPosition.y + 1),
				new(tile.CellPosition.x + 1, tile.CellPosition.y + 1),
				new(tile.CellPosition.x + 1, tile.CellPosition.y),
				new(tile.CellPosition.x + 1, tile.CellPosition.y - 1),
				new(tile.CellPosition.x, tile.CellPosition.y - 1)
			};

			return 
				from position in allPositions
					let neighbourPositions = tile.NeighboursWithDistances.Keys.Select(k => k.CellPosition) 
					where !neighbourPositions.Contains(position) 
					select position;
		}

		public static bool? IsDiagonalPositionTo(this Vector2Int point, Vector2Int target)
		{
			int deltaX = Mathf.Abs(point.x - target.x);
			int deltaY = Mathf.Abs(point.y - target.y);

			if (deltaX + deltaY > 2)
				return null;
			
			return deltaX + deltaY == 2;
		}
		
		public static bool? IsDiagonalPositionTo(this CustomTile a, CustomTile b)
		{
			int deltaX = Mathf.Abs(a.CellPosition.x - b.CellPosition.x);
			int deltaY = Mathf.Abs(a.CellPosition.y - b.CellPosition.y);

			if (deltaX + deltaY > 2)
				return null;
			
			return deltaX + deltaY == 2;
		}

	}
}