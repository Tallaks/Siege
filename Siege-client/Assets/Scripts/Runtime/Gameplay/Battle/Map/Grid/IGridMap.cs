using System.Collections.Generic;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Tiles;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Grid
{
	public interface IGridMap
	{
		IEnumerable<CustomTile> AllTiles { get; }
		IEnumerable<CustomTile> EmptyTiles { get; }
		void GenerateMap();
		void Clear();
		CustomTile GetTile(int x, int y);
	}
}