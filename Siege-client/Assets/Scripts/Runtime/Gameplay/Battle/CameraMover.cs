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
		private Camera _camera;

		private IInputService _inputService;

		private float _xDeg;
		private float _yDeg;

		private void Awake()
		{
			_camera = GetComponentInChildren<Camera>();
			_xDeg = transform.eulerAngles.y;
			_yDeg = transform.eulerAngles.x;
		}

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

		[Inject]
		private void Construct(IInputService inputService) => 
			_inputService = inputService;

		private Action<Vector2> RotateCamera()
		{
			return rotateInput =>
			{
				_xDeg += rotateInput.x * _rotationSpeed * Time.deltaTime;
				_yDeg -= rotateInput.y * _rotationSpeed * Time.deltaTime;
				_yDeg = _yDeg.ClampedAngle(-80f, 80f);

				transform.rotation = Quaternion.Euler(_yDeg, _xDeg, 0);
			};
		}

		private Action<float> ZoomCamera()
		{
			return zoomInput =>
			{
				Transform transform1 = _camera.transform;
				transform1.position += transform1.forward * zoomInput * _zoomSpeed;
			};
		}

		private Action<Vector2> MoveCamera()
		{
			return moveInput =>
			{
				Transform cameraTransform = _camera.transform;
				Vector3 moveDirection = moveInput.y * cameraTransform.forward + moveInput.x * cameraTransform.right;
				Vector3 delta = Vector3.ProjectOnPlane(moveDirection, Vector3.up);
				transform.position += delta * Time.deltaTime * _movementSpeed;
			};
		}
	}
}