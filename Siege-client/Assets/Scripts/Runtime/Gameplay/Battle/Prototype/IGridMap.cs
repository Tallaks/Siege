using System;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Movement;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Prototype
{
	public interface IGridMap
	{
		Action<CustomTile> OnTileSelection { get; set; }
		void GenerateMap();
		CustomTile GetTile(int x, int y);
	}
}