using System.ComponentModel;
using UnityEngine;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Tiles.Rendering.UvRotators
{
	public class Uv3_3_2Rotator : IUvRotator
	{
		private readonly Vector2Int _missingSide;

		public  Uv3_3_2Rotator(Vector2Int missingSide) => 
			_missingSide = missingSide;

		public float AngleDeg(CustomTile sourceTile)
		{
			if (sourceTile[-1, 0] == _missingSide)
				return 0f;
			if (sourceTile[0, -1] == _missingSide)
				return 90f;
			if (sourceTile[1, 0] == _missingSide)
				return 180f;
			if (sourceTile[0, 1] == _missingSide)
				return 270f;
			
			throw new InvalidEnumArgumentException("Некорректный тайл для определения!");
		}
	}
}