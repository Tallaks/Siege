using UnityEngine;

namespace Kulinaria.Siege.Runtime.Extensions
{
	public static class CustomGizmos
	{
		public static void DrawBounds(Bounds b, Color color)
		{
			// bottom
			var p1 = new Vector3(b.min.x, b.min.y, b.min.z);
			var p2 = new Vector3(b.max.x, b.min.y, b.min.z);
			var p3 = new Vector3(b.max.x, b.min.y, b.max.z);
			var p4 = new Vector3(b.min.x, b.min.y, b.max.z);

			Debug.DrawLine(p1, p2, color);
			Debug.DrawLine(p2, p3, color);
			Debug.DrawLine(p3, p4, color);
			Debug.DrawLine(p4, p1, color);

			// top
			var p5 = new Vector3(b.min.x, b.max.y, b.min.z);
			var p6 = new Vector3(b.max.x, b.max.y, b.min.z);
			var p7 = new Vector3(b.max.x, b.max.y, b.max.z);
			var p8 = new Vector3(b.min.x, b.max.y, b.max.z);

			Debug.DrawLine(p5, p6, color);
			Debug.DrawLine(p6, p7, color);
			Debug.DrawLine(p7, p8, color);
			Debug.DrawLine(p8, p5, color);

			// sides
			Debug.DrawLine(p1, p5, color);
			Debug.DrawLine(p2, p6, color);
			Debug.DrawLine(p3, p7, color);
			Debug.DrawLine(p4, p8, color);
		}
	}
}