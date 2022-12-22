using Kulinaria.Siege.Runtime.Debugging.Logging;
using Kulinaria.Siege.Runtime.Infrastructure.Inputs;
using UnityEngine;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Selection
{
	public class GridmapInteractor : IClickInteractor
	{
		private readonly ILoggerService _loggerService;
		private readonly IInputService _inputService;
		private readonly CameraMover _cameraMover;
		private IDeselectService _deselectService;

		public GridmapInteractor(
			ILoggerService loggerService,
			IInputService inputService,
			IDeselectService deselectService,
			CameraMover cameraMover)
		{
			_deselectService = deselectService;
			_loggerService = loggerService;
			_inputService = inputService;
			_cameraMover = cameraMover;
		}

		public void Initialize()
		{
			_loggerService.Log("Tile selector initialization", LoggerLevel.Battle);
			_inputService.OnClick += RegisterClick;
		}

		public void Dispose()
		{
			_loggerService.Log("Tile selector disposal", LoggerLevel.Battle);
			_inputService.OnClick -= RegisterClick;
		}

		private void RegisterClick(Vector2 clickPos)
		{
			Ray ray = _cameraMover.Camera.ScreenPointToRay(clickPos);
			if (Physics.Raycast(ray, out RaycastHit hit))
			{
				var tileSelectable = hit.transform.GetComponent<IInteractable>();
				if (tileSelectable != null)
				{
					tileSelectable.Interact();
					return;
				}
			}
			
			_deselectService.Deselect();
		}
	}
}