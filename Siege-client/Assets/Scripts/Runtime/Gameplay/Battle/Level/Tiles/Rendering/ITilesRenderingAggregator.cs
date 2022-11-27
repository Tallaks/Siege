using UnityEngine;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Level.Tiles.Rendering
{
	public interface ITilesRenderingAggregator
	{
		void ChangeMaterial(CustomTile tile, Material material, int neighboursNumber);
	}
}