using System.ComponentModel;
using UnityEngine;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Movement.Tiles.Rendering.UvRotators
{
	public class Uv6_2_0aRotator : IUvRotator
	{
		private readonly Vector2Int _neighbour0;
		private readonly Vector2Int _neighbour1;

		public Uv6_2_0aRotator(Vector2Int neighbour0, Vector2Int neighbour1)
		{
			_neighbour0 = neighbour0;
			_neighbour1 = neighbour1;
		}

		public float AngleDeg(CustomTile sourceTile)
		{
			Vector2Int neighbourDeltaSum = _neighbour0 + _neighbour1 - 2 * sourceTile.CellPosition;

			if (neighbourDeltaSum == Vector2Int.up * 2)
				return 0;
			if (neighbourDeltaSum == Vector2Int.down * 2)
				return 180f;
			if (neighbourDeltaSum == Vector2Int.right * 2)
				return 270f;
			if (neighbourDeltaSum == Vector2Int.left * 2)
				return 90f;

			throw new InvalidEnumArgumentException("Некорректный тайл для определения!");
		}
	}
}