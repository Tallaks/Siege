using System;
using Kulinaria.Siege.Runtime.Infrastructure.Inputs;
using UnityEngine;
using Zenject;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle
{
	public class CameraMover : MonoBehaviour
	{
		[SerializeField] private float _movementSpeed = 1;
		
		private IInputService _inputService;

		[Inject]
		private void Construct(IInputService inputService) => 
			_inputService = inputService;

		private void Start() => 
			_inputService.OnMove += MoveCamera();

		private void OnDestroy() =>
			_inputService.OnMove -= MoveCamera();

		private Action<Vector2> MoveCamera() =>
			moveInput =>
			{
				var delta = new Vector3(moveInput.x, 0, moveInput.y);
				transform.position += delta * Time.deltaTime * _movementSpeed;
			};
	}
}