using System.ComponentModel;
using UnityEngine;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Movement.Tiles.Rendering.UvRotators
{
	public class Uv2_3_3Rotator : IUvRotator
	{
		private readonly Vector2Int _neighbour1;
		private readonly Vector2Int _neighbour2;

		public Uv2_3_3Rotator(Vector2Int neighbour1, Vector2Int neighbour2)
		{
			_neighbour1 = neighbour1;
			_neighbour2 = neighbour2;
		}

		public float AngleDeg(CustomTile sourceTile)
		{
			Vector2Int missingBetweenTilesPosition =
				_neighbour1 + _neighbour2 - sourceTile.CellPosition;
			if (missingBetweenTilesPosition == sourceTile[-1, -1])
				return 0f;
			if (missingBetweenTilesPosition == sourceTile[1, -1])
				return 90f;
			if (missingBetweenTilesPosition == sourceTile[1, 1])
				return 180f;
			if (missingBetweenTilesPosition == sourceTile[-1, 1])
				return 270f;
			
			throw new InvalidEnumArgumentException("Некорректный тайл для определения!");
		}
	}
}