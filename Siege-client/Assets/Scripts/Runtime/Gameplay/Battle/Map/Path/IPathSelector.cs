using System;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Tiles;
using Zenject;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Path
{
	public interface IPathSelector : IInitializable, IDisposable
	{
		bool HasPath { get; }
		bool HasFirstSelectedTile { get; }
		void SelectFirstTile(CustomTile tile);
		void SelectSecondTile(CustomTile tile);
		void Deselect();
	}
}