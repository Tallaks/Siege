using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Movement.Tiles.Rendering.UvRotators
{
	public class Uv3_3_2Rotator : IUvRotator
	{
		private readonly IEnumerable<CustomTile> _neighbours;

		public Uv3_3_2Rotator(IEnumerable<CustomTile> neighbours) => 
			_neighbours = neighbours;

		public float AngleDeg(CustomTile sourceTile)
		{
			Vector2Int sum = _neighbours.Aggregate(Vector2Int.zero,
				(current, tile) => current + tile.CellPosition - sourceTile.CellPosition);
			if (sum.y == 0)
				return sum.x == 1 ? 0f : 180f;

			if (sum.x == 0)
				return sum.y == 1 ? 90f : 270f;

			throw new InvalidEnumArgumentException("Некорректный тайл для определения!");
		}
	}
}