using System.Collections.Generic;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Prototype;
using UnityEngine;
using Zenject;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Movement
{
	public class CustomTile : MonoBehaviour
	{
		private readonly HashSet<CustomTile> _neighbours = new();

		private IGridMap _map;

		public IEnumerable<CustomTile> Neighbours =>
			_neighbours;

		public Vector2Int CellPosition { get; private set; }

		[Inject]
		private void Construct(IGridMap map) =>
			_map = map;

		public void Initialize(Vector2Int cellPos)
		{
			CellPosition = cellPos;

			AddNeighbours(cellPos);
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
			if(a == null || b == null)
				return;
			
			a._neighbours.Add(b);
			b._neighbours.Add(a);
		}
	}
}