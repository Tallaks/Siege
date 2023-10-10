using UnityEngine;

namespace Kulinaria.Siege.Runtime.Extensions
{
	public static class Math
	{
		public static float ClampedAngle(this float value, float min, float max)
		{
			if (value < -360)
				value += 360;
			if (value > 360)
				value -= 360;
			return Mathf.Clamp(value, min, max);
		}

		public static Vector3 WithY(this Vector3 vector, float y)
		{
			return new Vector3(vector.x, y, vector.z);
		}
	}
}