using System;
using UnityEngine;

namespace Kulinaria.Siege.Runtime.Infrastructure.Inputs
{
	public interface IInputService
	{
		Action<Vector2> OnClick { get; set; }
		Action<Vector2> OnMove { get; set; }
		Action<Vector2> OnRotate { get; set; }
		Action<Vector2> OnZoom { get; set; }
	}
}