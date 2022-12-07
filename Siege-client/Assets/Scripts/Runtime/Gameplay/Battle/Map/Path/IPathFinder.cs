using System.Collections.Generic;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Tiles;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Path
{
	public interface IPathFinder
	{
		int Distance(CustomTile tileB);
		void FindDistancesToAllTilesFrom(CustomTile tile);
		LinkedList<CustomTile> GetShortestPath(CustomTile to);
		IEnumerable<CustomTile> GetAvailableTilesByDistance(int distance);
	}
}