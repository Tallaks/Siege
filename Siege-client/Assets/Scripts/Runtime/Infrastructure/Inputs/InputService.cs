using System;
using UnityEngine;

namespace Kulinaria.Siege.Runtime.Infrastructure.Inputs
{
	public class InputService : MonoBehaviour, IInputService
	{
		public Action<Vector2> OnClick { get; set; }
	}
}