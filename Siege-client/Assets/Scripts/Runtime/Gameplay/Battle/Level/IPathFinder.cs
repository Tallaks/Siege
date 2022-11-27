using System.Collections.Generic;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Level.Tiles;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Level
{
	public interface IPathFinder
	{
		int Distance(CustomTile tileB);
		LinkedList<CustomTile> GetShortestPath(CustomTile tile32);
		IEnumerable<CustomTile> GetAvailableTilesByDistance(int distance);
	}
}