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
		private List<LineRenderer> _lineRenderers = new();
		private LineRenderer _lineRendererPrefab;

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
			_lineRendererPrefab = _assetsProvider.LoadAsset<LineRenderer>("Prefabs/Battle/Map/Path");
		}

		public void DrawPathFromSelectedTileTo(CustomTile tile)
		{
			Clear();
			DrawPath(_pathFinder.GetShortestPath(tile));
		}

		public void Clear()
		{
			foreach (LineRenderer lineRenderer in _lineRenderers)
				Object.Destroy(lineRenderer.gameObject);
			
			_lineRenderers = new List<LineRenderer>();
		}

		private void DrawPath(LinkedList<CustomTile> path)
		{
			LinkedListNode<CustomTile> currentNode = path.First;
			if(currentNode == null) return;

			while (currentNode.Next != null)
			{
				var lineRenderer = 
					_container.InstantiatePrefabForComponent<LineRenderer>(_lineRendererPrefab);
				lineRenderer.positionCount = 2;
				lineRenderer.SetPosition(0, currentNode.Value.transform.position);
				lineRenderer.SetPosition(1, currentNode.Next.Value.transform.position);
				
				SetParametersFor(lineRenderer, currentNode.Next.Value.Active);

				_lineRenderers.Add(lineRenderer);
				currentNode = currentNode.Next;
			}
		}

		private static void SetParametersFor(LineRenderer lineRenderer, bool tileActivity)
		{
			Gradient colorGradient = lineRenderer.colorGradient;
			var temp = new GradientColorKey[1];

			if (tileActivity)
				temp[0] = new GradientColorKey(Color.green, 0);
			else
				temp[0] = new GradientColorKey(Color.red, 0);

			var alphaTemp = new GradientAlphaKey[] { new(1, 0) };
			colorGradient.mode = GradientMode.Fixed;
			colorGradient.alphaKeys = alphaTemp;
			colorGradient.colorKeys = temp;
			lineRenderer.colorGradient = colorGradient;
		}
	}
}