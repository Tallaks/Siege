using UnityEngine;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Movement.Tiles.Rendering.UvRotators
{
	public class Uv2_2_4Rotator : IUvRotator
	{
		private readonly Vector2Int _neighbourPos;

		public Uv2_2_4Rotator(Vector2Int neighbourPos) => 
			_neighbourPos = neighbourPos;

		public float AngleDeg(CustomTile sourceTile) =>
			_neighbourPos.x == sourceTile.CellPosition.x ? 0f : 90f;
	}
}