using System;
using System.Collections.Generic;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Tiles;
using Zenject;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Selection
{
	public interface ITileSelector : IInitializable, IDisposable
	{
		void Select(CustomTile tile, IEnumerable<CustomTile> availableTiles);
		void Deselect();
	}
}