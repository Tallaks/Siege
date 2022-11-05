using System.Collections.Generic;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Movement
{
	public interface IPathFinder
	{
		int Distance(CustomTile tileB);
		LinkedList<CustomTile> GetShortestPath(CustomTile tile32);
	}
}