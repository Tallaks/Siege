using System.ComponentModel;
using UnityEngine;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Tiles.Rendering.UvRotators
{
	public class Uv2_3_3Rotator : IUvRotator
	{
		private readonly Vector2Int _missingDiagonal;

		public Uv2_3_3Rotator(Vector2Int missingDiagonal) => 
			_missingDiagonal = missingDiagonal;

		public float AngleDeg(CustomTile sourceTile)
		{
			if (_missingDiagonal == sourceTile[-1, -1])
				return 0f;
			if (_missingDiagonal == sourceTile[1, -1])
				return 90f;
			if (_missingDiagonal == sourceTile[1, 1])
				return 180f;
			if (_missingDiagonal == sourceTile[-1, 1])
				return 270f;
			
			throw new InvalidEnumArgumentException("Некорректный тайл для определения!");
		}
	}
}