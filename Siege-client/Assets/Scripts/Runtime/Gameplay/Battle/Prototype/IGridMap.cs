using System;
using System.Collections.Generic;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Level.Tiles;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Prototype
{
	public interface IGridMap
	{
		IEnumerable<CustomTile> AllTiles { get; }
		Action<CustomTile> OnTileSelection { get; set; }
		void GenerateMap();
		void Clear();
		CustomTile GetTile(int x, int y);
	}
}