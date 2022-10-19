using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Kulinaria.Siege.Runtime.Infrastructure.Inputs
{
	public class InputService : MonoBehaviour, IInputService
	{
		private GameControls _inputSystem;
		public Action<Vector2> OnClick { get; set; }

		private void Awake()
		{
			_inputSystem = new GameControls();
			_inputSystem.Enable();
		}

		private void Update()
		{
			if (_inputSystem.CameraActions.Click.WasPerformedThisFrame()) 
				OnClick?.Invoke(Mouse.current.position.ReadValue());
		}
	}
}