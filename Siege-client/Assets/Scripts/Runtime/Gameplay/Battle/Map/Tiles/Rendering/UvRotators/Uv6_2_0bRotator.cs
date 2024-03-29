using UnityEngine;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Tiles.Rendering.UvRotators
{
	public class Uv6_2_0bRotator : IUvRotator
	{
		private readonly Vector2Int _neighbourPos;

		public Uv6_2_0bRotator(Vector2Int neighbourPos) =>
			_neighbourPos = neighbourPos;

		public float AngleDeg(CustomTile sourceTile)
		{
			if (sourceTile[1, 1] == _neighbourPos || sourceTile[-1, -1] == _neighbourPos)
				return 0f;
			return 90;
		}
	}
}