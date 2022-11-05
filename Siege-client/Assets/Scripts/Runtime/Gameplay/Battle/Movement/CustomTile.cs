using System.Collections.Generic;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Prototype;
using Kulinaria.Siege.Runtime.Infrastructure.Inputs;
using UnityEngine;
using Zenject;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Movement
{
	public class CustomTile : MonoBehaviour
	{
		private readonly Dictionary<CustomTile, int> _neighboursWithDistances = new();

		private IGridMap _map;
		private IInputService _inputService;
		private Camera _camera;

		public IReadOnlyDictionary<CustomTile, int> NeighboursWithDistances =>
			_neighboursWithDistances;

		public Vector2Int CellPosition { get; private set; }

		[Inject]
		private void Construct(IInputService inputService, IGridMap map, Camera camera)
		{
			_camera = camera;
			_inputService = inputService;
			_map = map;
		}

		private void OnEnable() =>
			_inputService.OnClick += Select;

		private void OnDisable() =>
			_inputService.OnClick -= Select;

		public void Initialize(Vector2Int cellPos)
		{
			CellPosition = cellPos;

			AddNeighbours(cellPos);
		}

		private void Select(Vector2 screenPosition)
		{
			Ray ray = _camera.ScreenPointToRay(screenPosition);
			if (Physics.Raycast(ray, out RaycastHit hit))
			{
				if (hit.transform.GetComponent<CustomTile>() == this)
					_map.OnTileSelection?.Invoke(this);
			}
		}

		private void AddNeighbours(Vector2Int cellPos)
		{
			AddNeighbours(this, _map.GetTile(cellPos.x + 1, cellPos.y));
			AddNeighbours(this, _map.GetTile(cellPos.x - 1, cellPos.y));
			AddNeighbours(this, _map.GetTile(cellPos.x, cellPos.y + 1));
			AddNeighbours(this, _map.GetTile(cellPos.x, cellPos.y - 1));
			AddNeighbours(this, _map.GetTile(cellPos.x + 1, cellPos.y - 1));
			AddNeighbours(this, _map.GetTile(cellPos.x - 1, cellPos.y - 1));
			AddNeighbours(this, _map.GetTile(cellPos.x + 1, cellPos.y + 1));
			AddNeighbours(this, _map.GetTile(cellPos.x - 1, cellPos.y + 1));
		}

		private static void AddNeighbours(CustomTile a, CustomTile b)
		{
			if (a == null || b == null)
				return;

			int deltaX = Mathf.Abs(a.CellPosition.x - b.CellPosition.x);
			int deltaY = Mathf.Abs(a.CellPosition.y - b.CellPosition.y);

			if (deltaX + deltaY < 2)
			{
				a._neighboursWithDistances[b] = 2;
				b._neighboursWithDistances[a] = 2;
			}
			else
			{
				a._neighboursWithDistances[b] = 3;
				b._neighboursWithDistances[a] = 3;
			}
		}
	}
}