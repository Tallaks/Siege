using System;
using Kulinaria.Siege.Runtime.Extensions;
using Kulinaria.Siege.Runtime.Infrastructure.Inputs;
using UnityEngine;
using Zenject;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle
{
	public class CameraMover : MonoBehaviour
	{
		[SerializeField] private float _movementSpeed = 1;
		[SerializeField] private float _zoomSpeed = 1;
		[SerializeField] private float _rotationSpeed = 1;
		
		private IInputService _inputService;
		private Camera _camera;

		private float _xDeg = 0f;
		private float _yDeg = 0f;
		
		[Inject]
		private void Construct(IInputService inputService) => 
			_inputService = inputService;

		private void Awake() => 
			_camera = GetComponentInChildren<Camera>();

		private void Start()
		{
			_inputService.OnMove += MoveCamera();
			_inputService.OnZoom += ZoomCamera();
			_inputService.OnRotate += RotateCamera();
		}

		private void OnDestroy()
		{
			_inputService.OnMove -= MoveCamera();
			_inputService.OnZoom -= ZoomCamera();
			_inputService.OnRotate -= RotateCamera();
		}

		private Action<Vector2> RotateCamera() =>
			delta =>
			{
				_xDeg += delta.x * _rotationSpeed * Time.deltaTime;
				_yDeg -= delta.y * _rotationSpeed * Time.deltaTime;
				_yDeg = _yDeg.ClampedAngle(-80f, 80f);
				
				transform.rotation = Quaternion.Euler(_yDeg, _xDeg, 0);
			};

		private Action<float> ZoomCamera() =>
			zoomInput => _camera.transform.position += _camera.transform.forward * zoomInput * _zoomSpeed;

		private Action<Vector2> MoveCamera() =>
			moveInput =>
			{
				var delta = new Vector3(moveInput.x, 0, moveInput.y);
				transform.position += delta * Time.deltaTime * _movementSpeed;
			};
	}
}