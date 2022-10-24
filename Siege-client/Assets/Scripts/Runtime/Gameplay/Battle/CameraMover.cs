using System;
using Kulinaria.Siege.Runtime.Infrastructure.Inputs;
using UnityEngine;
using Zenject;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle
{
	public class CameraMover : MonoBehaviour
	{
		[SerializeField] private float _movementSpeed = 1;
		[SerializeField] private float _zoomSpeed = 1;
		
		private IInputService _inputService;
		private Camera _camera;

		[Inject]
		private void Construct(IInputService inputService) => 
			_inputService = inputService;

		private void Awake() => 
			_camera = GetComponentInChildren<Camera>();

		private void Start()
		{
			_inputService.OnMove += MoveCamera();
			_inputService.OnZoom += ZoomCamera();
		}

		private void OnDestroy()
		{
			_inputService.OnMove -= MoveCamera();
			_inputService.OnZoom -= ZoomCamera();
		}

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