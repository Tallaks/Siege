using System.ComponentModel;
using UnityEngine;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Tiles.Rendering.UvRotators
{
	public class Uv3_2_3Rotator : IUvRotator
	{
		private readonly Vector2Int _diagonalPos;

		public Uv3_2_3Rotator(Vector2Int diagonalPos) =>
			_diagonalPos = diagonalPos;

		public float AngleDeg(CustomTile sourceTile)
		{
			if (_diagonalPos == sourceTile[-1, -1])
				return 0f;
			if (_diagonalPos == sourceTile[1, -1])
				return 90f;
			if (_diagonalPos == sourceTile[1, 1])
				return 180f;
			if (_diagonalPos == sourceTile[-1, 1])
				return 270f;

			throw new InvalidEnumArgumentException("Некорректный тайл для определения!");
		}
	}
}