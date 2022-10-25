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
	}
}