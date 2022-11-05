using System;
using System.Collections.Generic;
using System.Linq;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Prototype;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Movement
{
	public class BellmanFordPathFinder : IPathFinder
	{
		private readonly IGridMap _map;
		private Dictionary<CustomTile,int> _distances;
		private Dictionary<CustomTile,CustomTile> _predecessors;

		public BellmanFordPathFinder(IGridMap map)
		{
			_map = map;
			_map.OnTileSelection += FindDistancesToAllTilesFrom;
		}

		public int Distance(CustomTile tileA, CustomTile tileB) => 
			_distances[tileB];

		private void FindDistancesToAllTilesFrom(CustomTile startTile)
		{
			int tileCount = _map.AllTiles.Count();
			_distances = new Dictionary<CustomTile, int>(tileCount);
			_predecessors = new Dictionary<CustomTile, CustomTile>(tileCount);

			foreach (CustomTile tile in _map.AllTiles) 
				_distances[tile] = int.MaxValue;

			_distances[startTile] = 0;
			CustomTile[] arrayOfTiles = _map.AllTiles.ToArray();
			
			for (var i = 0; i < tileCount; i++)
			{
				CustomTile currentTile = arrayOfTiles[i];
				foreach (KeyValuePair<CustomTile, int> tileWithEdge in currentTile.NeighboursWithDistances)
				{
					if (_distances[currentTile] > AddDistances(_distances[tileWithEdge.Key],tileWithEdge.Value))
					{
						_distances[currentTile] = AddDistances(_distances[tileWithEdge.Key],tileWithEdge.Value);
						_predecessors[currentTile] = tileWithEdge.Key;
					}
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