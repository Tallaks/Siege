using System.Collections.Generic;
using Kulinaria.Siege.Runtime.Debugging.Logging;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Tiles;
using Kulinaria.Siege.Runtime.Infrastructure.Assets;
using UnityEngine;
using Zenject;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Path
{
	public class PathLineRenderer : IPathRenderer
	{
		private readonly DiContainer _container;
		private readonly IAssetsProvider _assetsProvider;
		private readonly ILoggerService _loggerService;
		private readonly IPathFinder _pathFinder;
		private LineRenderer _lineRenderer;

		public PathLineRenderer(
			DiContainer container,
			IAssetsProvider assetsProvider,
			ILoggerService loggerService,
			IPathFinder pathFinder)
		{
			_container = container;
			_assetsProvider = assetsProvider;
			_loggerService = loggerService;
			_pathFinder = pathFinder;
		}
		
		public void Initialize()
		{
			_loggerService.Log("Path selector initialization", LoggerLevel.Battle);
			var prefab = _assetsProvider.LoadAsset<LineRenderer>("Prefabs/Battle/Map/Path");
			_lineRenderer = 
				_container.InstantiatePrefabForComponent<LineRenderer>(prefab);
			_lineRenderer.positionCount = 0;
		}

		public void DrawPathFromSelectedTileTo(CustomTile tile)
		{
			Clear();
			DrawPath(_pathFinder.GetShortestPath(tile));
		}

		public void Clear() => 
			_lineRenderer.positionCount = 0;

		private void DrawPath(LinkedList<CustomTile> path)
		{
			LinkedListNode<CustomTile> currentNode = path.First;
			var currentIndex = 0;
			while (currentNode != null)
			{
				_lineRenderer.positionCount++;
				_lineRenderer.SetPosition(currentIndex, currentNode.Value.transform.position);
				currentIndex++;
				currentNode = currentNode.Next;
			}
			
			if (PathHasAvailableAndUnavailableTiles(path, out var index))
				DrawAvailableAndUnavailablePath(index, path);
			else
				DrawOnlyAvailablePath();
		}

		private void DrawOnlyAvailablePath()
		{
			Gradient colorGradient = _lineRenderer.colorGradient;
			GradientColorKey[] temp;
			GradientAlphaKey[] alphaTemp;

			temp = new GradientColorKey[2];
			temp[0] = new GradientColorKey(Color.green, 0);
			temp[1] = new GradientColorKey(Color.green, 1);

			alphaTemp = new GradientAlphaKey[] { new(1, 0) };
			colorGradient.mode = GradientMode.Fixed;
			colorGradient.colorKeys = temp;
			colorGradient.alphaKeys = alphaTemp;
			_lineRenderer.colorGradient = colorGradient;
		}

		private void DrawAvailableAndUnavailablePath(int index, LinkedList<CustomTile> path)
		{
			Gradient colorGradient = _lineRenderer.colorGradient;
			GradientColorKey[] temp;
			GradientAlphaKey[] alphaTemp;

			float time = (float)index / path.Count;
			_loggerService.Log("Index:" + index);
			_loggerService.Log("path.Count:" + path.Count);
			
			temp = new GradientColorKey[2];
			temp[0] = new GradientColorKey(Color.green, time);
			temp[1] = new GradientColorKey(Color.red, 1);

			alphaTemp = new GradientAlphaKey[] { new(1, 0) };
			colorGradient.mode = GradientMode.Fixed;
			colorGradient.colorKeys = temp;
			colorGradient.alphaKeys = alphaTemp;
			_lineRenderer.colorGradient = colorGradient;
		}

		private bool PathHasAvailableAndUnavailableTiles(LinkedList<CustomTile> path, out int index)
		{
			index = 0;
			LinkedListNode<CustomTile> currentNode = path.First;

			while (currentNode != null)
			{
				if (currentNode.Value.Active == false)
					return true;

				index++;
				currentNode = currentNode.Next;
			}
			
			return false;
		}
	}
}