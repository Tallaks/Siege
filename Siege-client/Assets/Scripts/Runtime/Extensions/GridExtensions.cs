using UnityEngine;

namespace Kulinaria.Siege.Runtime.Extensions
{
	public static class GridExtensions
	{
		public static Vector3 ToWorld(this Vector2Int cellPosition) =>
			new(0.5f + cellPosition.x, 0.1f, 0.5f + cellPosition.y);
	}
}