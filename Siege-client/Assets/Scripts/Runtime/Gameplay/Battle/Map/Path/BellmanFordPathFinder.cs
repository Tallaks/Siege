using System.Collections.Generic;
using System.Linq;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Grid;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Tiles;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Path
{
	public class BellmanFordPathFinder : IPathFinder
	{
		private readonly IGridMap _map;
		private Dictionary<CustomTile, int> _distances;
		private Dictionary<CustomTile, CustomTile> _predecessors;
		private CustomTile _startTile;

		public BellmanFordPathFinder(IGridMap map) =>
			_map = map;

		public int Distance(CustomTile to)
		{
			return _distances[to];
		}

		public LinkedList<CustomTile> GetShortestPath(CustomTile tile)
		{
			var path = new LinkedList<CustomTile>();
			if (_predecessors[tile] == null)
				return path;

			path.AddLast(tile);

			CustomTile currentTile = tile;
			int tileCount = _map.AllTiles.Count();
			var counter = 0;

			while (currentTile != _startTile)
			{
				counter++;
				LinkedListNode<CustomTile> currentNode = path.Find(currentTile);
				path.AddBefore(currentNode, _predecessors[currentTile]);

				currentTile = _predecessors[currentTile];

				if (counter > tileCount)
					return new LinkedList<CustomTile>();
			}

			return path;
		}

		public IEnumerable<CustomTile> GetAvailableTilesByDistance(int distance)
		{
			return _map.AllTiles.Where(k => _distances[k] <= distance);
		}

		public void FindDistancesToAllTilesFrom(CustomTile startTile)
		{
			_startTile = startTile;
			int tileCount = _map.AllTiles.Count();
			_distances = new Dictionary<CustomTile, int>(tileCount);
			_predecessors = new Dictionary<CustomTile, CustomTile>();

			foreach (CustomTile tile in _map.AllTiles)
			{
				_distances[tile] = int.MaxValue;
				_predecessors[tile] = null;
			}

			_distances[startTile] = 0;
			CustomTile[] arrayOfTiles = _map.AllTiles.ToArray();
			for (var j = 0; j < tileCount; j++)
			for (var i = 0; i < tileCount; i++)
			{
				CustomTile currentTile = arrayOfTiles[i];
				foreach (CustomTile tile in currentTile.NeighboursWithDistances.Keys)
					if (_distances[currentTile] > AddDistances(_distances[tile], currentTile.NeighboursWithDistances[tile]))
					{
						_distances[currentTile] = AddDistances(_distances[tile], currentTile.NeighboursWithDistances[tile]);
						_predecessors[currentTile] = tile;
					}
			}
		}

		private static int AddDistances(int a, int b)
		{
			if (a == int.MaxValue || b == int.MaxValue)
				return int.MaxValue;

			return a + b;
		}
	}
}