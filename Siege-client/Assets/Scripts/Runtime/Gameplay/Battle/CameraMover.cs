using Kulinaria.Siege.Runtime.Infrastructure.Inputs;
using UnityEngine;
using Zenject;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle
{
	public class CameraMover : MonoBehaviour
	{
		private IInputService _inputService;

		[Inject]
		private void Construct(IInputService inputService) => 
			_inputService = inputService;

		private void Awake()
		{
			_inputService.OnMove += moveInput =>
			{
				var delta = new Vector3(moveInput.x, 0, moveInput.y);
				transform.position += delta;
			};
		}
	}
}