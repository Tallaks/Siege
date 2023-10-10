using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Kulinaria.Siege.Runtime.Infrastructure.Inputs
{
	public class InputService : MonoBehaviour, IInputService
	{
		public Vector2 PointPosition { get; set; }
		public Action<Vector2> OnClick { get; set; }
		public Action<Vector2> OnMove { get; set; }
		public Action<Vector2> OnRotate { get; set; }
		public Action<float> OnZoom { get; set; }
		private GameControls _inputSystem;

		private void Awake()
		{
			_inputSystem = new GameControls();
			_inputSystem.Enable();
		}

		private void Update()
		{
			PointPosition = _inputSystem.CameraActions.PointPosition.ReadValue<Vector2>();

			if (_inputSystem.CameraActions.Click.WasPerformedThisFrame() && !Keyboard.current.leftAltKey.isPressed)
				OnClick?.Invoke(Mouse.current.position.ReadValue());

			if (_inputSystem.CameraActions.Move.IsInProgress())
				OnMove?.Invoke(_inputSystem.CameraActions.Move.ReadValue<Vector2>());

			if (_inputSystem.CameraActions.Rotate.IsInProgress())
				OnRotate?.Invoke(_inputSystem.CameraActions.Rotate.ReadValue<Vector2>());

			if (_inputSystem.CameraActions.Zoom.IsInProgress())
				OnZoom?.Invoke(_inputSystem.CameraActions.Zoom.ReadValue<float>());
		}
	}
}