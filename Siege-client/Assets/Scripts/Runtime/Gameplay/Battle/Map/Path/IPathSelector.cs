using System;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Tiles;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Path
{
	public interface IPathSelector : IDisposable
	{
		bool HasPath { get; }
		bool HasFirstSelectedTile { get; }
		void SelectFirstTile(CustomTile tile);
		void SelectSecondTile(CustomTile tile);
		void Deselect();
	}
}