using System.ComponentModel;
using UnityEngine;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Tiles.Rendering.UvRotators
{
	public class Uv5_3_0Rotator : IUvRotator
	{
		private readonly Vector2Int _diagonalTilePos;

		public Uv5_3_0Rotator(Vector2Int diagonalTilePos) =>
			_diagonalTilePos = diagonalTilePos;

		public float AngleDeg(CustomTile sourceTile)
		{
			if (sourceTile[1, -1] == _diagonalTilePos)
				return 0f;
			if (sourceTile[1, 1] == _diagonalTilePos)
				return 90f;
			if (sourceTile[-1, 1] == _diagonalTilePos)
				return 180f;
			if (sourceTile[-1, -1] == _diagonalTilePos)
				return 270f;

			throw new InvalidEnumArgumentException("Некорректный тайл для определения!");
		}
	}
}