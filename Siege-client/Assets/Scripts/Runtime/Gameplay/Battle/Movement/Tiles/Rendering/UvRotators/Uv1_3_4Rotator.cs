using System.ComponentModel;
using UnityEngine;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Movement.Tiles.Rendering.UvRotators
{
	public class Uv1_3_4Rotator : IUvRotator
	{
		private readonly Vector2Int _sideNeighbourPos;

		public Uv1_3_4Rotator(Vector2Int sideNeighbourPos) =>
			_sideNeighbourPos = sideNeighbourPos;

		public float AngleDeg(CustomTile sourceTile)
		{
			if (sourceTile[0, 1] == _sideNeighbourPos)
				return 180f;

			if (sourceTile[0, -1] == _sideNeighbourPos)
				return 0f;

			if (sourceTile[-1, 0] == _sideNeighbourPos)
				return 270f;

			if (sourceTile[1, 0] == _sideNeighbourPos)
				return 90f;

			throw new InvalidEnumArgumentException("Некорректный тайл для определения!");
		}
	}
}