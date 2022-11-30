using UnityEngine;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Tiles.Rendering.UvRotators
{
	public class Uv2_2_4Rotator : IUvRotator
	{
		private readonly Vector2Int _sideNeighbourPos;

		public Uv2_2_4Rotator(Vector2Int sideNeighbourPos) => 
			_sideNeighbourPos = sideNeighbourPos;

		public float AngleDeg(CustomTile sourceTile) =>
			_sideNeighbourPos.x == sourceTile.CellPosition.x ? 0f : 90f;
	}
}