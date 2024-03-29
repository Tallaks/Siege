using System.Collections;
using Kulinaria.Siege.Runtime.Debugging.Logging;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Selection;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Tiles;
using Kulinaria.Siege.Runtime.Infrastructure.Coroutines;
using Kulinaria.Siege.Runtime.Infrastructure.Inputs;
using UnityEngine;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Path
{
	public class PathSelector : IPathSelector
	{
		private readonly ILoggerService _loggerService;
		private readonly IInputService _inputService;
		private readonly IPathRenderer _pathRenderer;
		private readonly CameraMover _cameraMover;
		private readonly ICoroutineRunner _coroutineRunner;

		public bool HasPath => _firstPoint != null && _secondPoint != null;
		public bool HasFirstSelectedTile => _firstPoint != null;
		private Coroutine _currentCoroutine;

		private CustomTile _firstPoint;
		private CustomTile _previewSecondPoint;
		private CustomTile _secondPoint;

		public PathSelector(
			ILoggerService loggerService,
			ICoroutineRunner coroutineRunner,
			IInputService inputService,
			IPathRenderer pathRenderer,
			CameraMover cameraMover)
		{
			_loggerService = loggerService;
			_coroutineRunner = coroutineRunner;
			_inputService = inputService;
			_pathRenderer = pathRenderer;
			_cameraMover = cameraMover;
		}

		public void Dispose()
		{
			_loggerService.Log("Path selector disposal", LoggerLevel.Map);
			if (_currentCoroutine != null)
				_coroutineRunner?.StopCoroutine(_currentCoroutine);
			_currentCoroutine = null;
		}

		public void SelectFirstTile(CustomTile tile)
		{
			if (_firstPoint == null)
			{
				_firstPoint = tile;
				_currentCoroutine = _coroutineRunner?.StartCoroutine(PathSelectionPreview());
			}
		}

		public void SelectSecondTile(CustomTile tile)
		{
			if (_secondPoint == null && _firstPoint != null)
			{
				_secondPoint = tile;
				Dispose();
			}
		}

		public void Deselect()
		{
			_firstPoint = null;
			_secondPoint = null;
			_pathRenderer.Clear();
			Dispose();
		}

		private IEnumerator PathSelectionPreview()
		{
			while (true)
			{
				yield return null;
				Ray ray = _cameraMover.Camera.ScreenPointToRay(_inputService.PointPosition);
				if (Physics.Raycast(ray, out RaycastHit hit))
				{
					var tileSelectable = hit.transform.GetComponent<IInteractable>();
					if (tileSelectable != null)
					{
						if (_previewSecondPoint != tileSelectable.Tile)
						{
							_previewSecondPoint = tileSelectable.Tile;
							_pathRenderer.DrawPathFromSelectedTileTo(tileSelectable.Tile);
						}
					}
					else
					{
						_pathRenderer.Clear();
					}
				}
				else
				{
					_pathRenderer.Clear();
				}
			}
		}
	}
}