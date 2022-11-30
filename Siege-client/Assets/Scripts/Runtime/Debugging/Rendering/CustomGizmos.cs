using UnityEngine;

namespace Kulinaria.Siege.Runtime.Debugging.Rendering
{
	public static class CustomGizmos
	{
		public static void DrawTile(Bounds b, Color color)
		{
			// top
			var p5 = new Vector3(b.min.x, b.max.y, b.min.z);
			var p6 = new Vector3(b.max.x, b.max.y, b.min.z);
			var p7 = new Vector3(b.max.x, b.max.y, b.max.z);
			var p8 = new Vector3(b.min.x, b.max.y, b.max.z);

			Debug.DrawLine(p5, p6, color);
			Debug.DrawLine(p6, p7, color);
			Debug.DrawLine(p7, p8, color);
			Debug.DrawLine(p8, p5, color);
		}
	}
}