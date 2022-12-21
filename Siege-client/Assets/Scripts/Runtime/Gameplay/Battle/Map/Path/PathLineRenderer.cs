using System.Collections.Generic;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Tiles;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Utilities;
using UnityEngine;
using Zenject;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Path
{
	public class PathLineRenderer : IPathRenderer
	{
		private readonly IPathFinder _pathFinder;

		private List<LineRenderer> _lineRenderers = new();

		[Inject] private Pool<LineRenderer> _pathRendererPool;

		public PathLineRenderer(IPathFinder pathFinder) =>
			_pathFinder = pathFinder;

		public void DrawPathFromSelectedTileTo(CustomTile tile)
		{
			Clear();
			DrawPath(_pathFinder.GetShortestPath(tile));
		}

		public void Clear()
		{
			foreach (LineRenderer lineRenderer in _lineRenderers)
				lineRenderer.gameObject.SetActive(false);

			_lineRenderers = new List<LineRenderer>();
		}

		private void DrawPath(LinkedList<CustomTile> path)
		{
			LinkedListNode<CustomTile> currentNode = path.First;
			if (currentNode == null) return;

			while (currentNode.Next != null)
			{
				LineRenderer lineRenderer =
					_pathRendererPool.GetFreeElement(Vector3.zero, Quaternion.identity);

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