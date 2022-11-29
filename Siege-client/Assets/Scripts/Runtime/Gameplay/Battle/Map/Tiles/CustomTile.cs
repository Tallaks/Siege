using System.Collections.Generic;
using System.Linq;
using Kulinaria.Siege.Runtime.Debugging.Logging;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Grid;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Selection;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Tiles.Rendering;
using Kulinaria.Siege.Runtime.Infrastructure.Inputs;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Tiles
{
	[RequireComponent(typeof(Renderer))]
	[RequireComponent(typeof(TileRenderer))]
	public class CustomTile : MonoBehaviour
	{
		public IReadOnlyDictionary<CustomTile, int> NeighboursWithDistances =>
			_neighboursWithDistances;

		[ShowInInspector]
		public IEnumerable<CustomTile> ActiveNeighbours => 
			_neighboursWithDistances.Keys.Where(k => k.Active);

		[ShowInInspector]
		public Vector2Int CellPosition { get; private set; }

		public bool Active
		{
			get => GetComponent<Renderer>().enabled;
			set => GetComponent<Renderer>().enabled = value;
		}

		public TileRenderer Renderer => GetComponent<TileRenderer>();

		private readonly Dictionary<CustomTile, int> _neighboursWithDistances = new();
		
		private Camera _camera;
		private IInputService _inputService;
		private IGridMap _map;
		private ITileSelector _selector;
		private ILoggerService _logger;

		private void OnEnable() =>
			_inputService.OnClick += Select;

		private void OnDisable() =>
			_inputService.OnClick -= Select;

		[Inject]
		private void Construct(ILoggerService logger, IInputService inputService, IGridMap map, ITileSelector selector, CameraMover cameraMover)
		{
			_logger = logger;
			_selector = selector;
			_camera = cameraMover.Camera;
			_inputService = inputService;
			_map = map;
		}

		public void Initialize(Vector2Int cellPos)
		{
			CellPosition = cellPos;
			AddNeighbours(cellPos);
			Active = false;
		}

		private void Select(Vector2 screenPosition)
		{
			Ray ray = _camera.ScreenPointToRay(screenPosition);
			if (Physics.Raycast(ray, out RaycastHit hit))
				if (hit.transform.GetComponent<CustomTile>() == this)
					_selector.Select(this, 10);
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

		public Vector2Int this[int x, int y]
			=> new(CellPosition.x + x, CellPosition.y + y);
	}
}