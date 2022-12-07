using System.Collections;
using System.Collections.Generic;
using Kulinaria.Siege.Runtime.Debugging.Logging;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Selection;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Tiles;
using Kulinaria.Siege.Runtime.Infrastructure.Assets;
using Kulinaria.Siege.Runtime.Infrastructure.Coroutines;
using Kulinaria.Siege.Runtime.Infrastructure.Inputs;
using UnityEngine;
using Zenject;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Path
{
	public class PathSelector : IPathSelector
	{
		private readonly DiContainer _container;
		private readonly IAssetsProvider _assetsProvider;
		private readonly ILoggerService _loggerService;
		private readonly ICoroutineRunner _coroutineRunner;
		private readonly IInputService _inputService;
		private readonly IPathFinder _pathFinder;
		private readonly CameraMover _cameraMover;

		private CustomTile _firstPoint;
		private CustomTile _secondPoint;
		private CustomTile _previewSecondPoint;
		private Coroutine _currentCoroutine;
		private LineRenderer _lineRenderer;

		public PathSelector(
			DiContainer container,
			IAssetsProvider assetsProvider,
			ILoggerService loggerService,
			ICoroutineRunner coroutineRunner,
			IInputService inputService,
			IPathFinder pathFinder,
			CameraMover cameraMover)
		{
			_container = container;
			_assetsProvider = assetsProvider;
			_loggerService = loggerService;
			_coroutineRunner = coroutineRunner;
			_inputService = inputService;
			_pathFinder = pathFinder;
			_cameraMover = cameraMover;
		}

		public bool HasPath => _firstPoint != null && _secondPoint != null;
		public bool HasFirstSelectedTile => _firstPoint != null;

		public void Initialize()
		{
			_loggerService.Log("Path selector initialization", LoggerLevel.Battle);
			var prefab = _assetsProvider.LoadAsset<LineRenderer>("Prefabs/Battle/Map/Path");
			_lineRenderer = 
				_container.InstantiatePrefabForComponent<LineRenderer>(prefab);
			_lineRenderer.positionCount = 0;
		}

		public void Dispose()
		{
			if(_currentCoroutine != null)
				_coroutineRunner.StopCoroutine(_currentCoroutine);
			_currentCoroutine = null;
		}

		public void SelectFirstTile(CustomTile tile)
		{
			if (_firstPoint == null)
			{
				_firstPoint = tile;
				_currentCoroutine = _coroutineRunner.StartCoroutine(PathSelectionPreview());
				return;
			}
		}

		public void SelectSecondTile(CustomTile tile)
		{
			if (_secondPoint == null && _firstPoint != null)
			{
				_secondPoint = tile;
				Dispose();
				return;
			}
		}

		public void Deselect()
		{
			_firstPoint = null;
			_secondPoint = null;
			ClearPath();
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
					var tileSelectable = hit.transform.GetComponent<ITileSelectable>();
					if (tileSelectable != null)
					{
						if (_previewSecondPoint != tileSelectable.Tile)
						{
							_previewSecondPoint = tileSelectable.Tile;
							DrawPath(to: tileSelectable.Tile);
						}
					}
					else
						ClearPath();
				}
				else
					ClearPath();
			}
		}

		private void DrawPath(CustomTile to)
		{
			ClearPath();
			LinkedList<CustomTile> path = _pathFinder.GetShortestPath(to);
			LinkedListNode<CustomTile> currentNode = path.First;
			var currentIndex = 0;
			while (currentNode != null)
			{
				_lineRenderer.positionCount++;
				_lineRenderer.SetPosition(currentIndex, currentNode.Value.transform.position);
				currentIndex++;
				currentNode = currentNode.Next;
			}
		}

		private void ClearPath()
		{
			_lineRenderer.positionCount = 0;
		}
	}
}