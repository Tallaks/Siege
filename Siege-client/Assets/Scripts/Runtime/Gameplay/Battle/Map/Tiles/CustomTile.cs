using System.Collections.Generic;
using System.Linq;
using Kulinaria.Siege.Runtime.Debugging.Logging;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Characters;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Grid;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Selection;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Tiles.Rendering;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Tiles
{
	[RequireComponent(typeof(Renderer))]
	[RequireComponent(typeof(TileRenderer))]
	[RequireComponent(typeof(TileInteraction))]
	public class CustomTile : MonoBehaviour
	{
		public IReadOnlyDictionary<CustomTile, int> NeighboursWithDistances =>
			_neighboursWithDistances;

		[ShowInInspector]
		public IEnumerable<CustomTile> ActiveNeighbours =>
			_neighboursWithDistances.Keys.Where(k => k.Active);

		[ShowInInspector] public Vector2Int CellPosition { get; private set; }

		public bool Active
		{
			get => GetComponent<Renderer>().enabled;
			set => GetComponent<Renderer>().enabled = value;
		}

		public BaseCharacter Visitor =>
			_visitor;

		public bool HasVisitor =>
			_visitor != null;

		public TileRenderer Renderer =>
			GetComponent<TileRenderer>();

		private readonly Dictionary<CustomTile, int> _neighboursWithDistances = new();

		private IGridMap _map;
		private ILoggerService _logger;
		private BaseCharacter _visitor;

		[Inject]
		private void Construct(ILoggerService logger, IGridMap map, IClickInteractor selector)
		{
			_logger = logger;
			_map = map;
		}

		public void Initialize(Vector2Int cellPos)
		{
			CellPosition = cellPos;
			AddNeighbours(cellPos);
			Active = false;
		}

		public void RegisterVisitor(BaseCharacter visitor)
		{
			_visitor = visitor;
			_visitor.Interaction.Assign(this);
		}

		public void UnregisterVisitor() =>
			_visitor = null;

		public Vector2Int this[int x, int y] =>
			new(CellPosition.x + x, CellPosition.y + y);

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