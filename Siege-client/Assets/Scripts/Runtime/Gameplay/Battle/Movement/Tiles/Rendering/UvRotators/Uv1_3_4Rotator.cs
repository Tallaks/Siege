using System.ComponentModel;
using UnityEngine;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Movement.Tiles.Rendering.UvRotators
{
	public class Uv1_3_4Rotator : IUvRotator
	{
		private readonly Vector2Int _neighbourPos;

		public Uv1_3_4Rotator(Vector2Int neighbourPos) =>
			_neighbourPos = neighbourPos;

		public float AngleDeg(CustomTile sourceTile)
		{
			if (sourceTile[0, 1] == _neighbourPos)
				return 180f;

			if (sourceTile[0, -1] == _neighbourPos)
				return 0f;

			if (sourceTile[-1, 0] == _neighbourPos)
				return 270f;

			if (sourceTile[1, 0] == _neighbourPos)
				return 90f;

			throw new InvalidEnumArgumentException("Некорректный тайл для определения!");
		}
	}
}