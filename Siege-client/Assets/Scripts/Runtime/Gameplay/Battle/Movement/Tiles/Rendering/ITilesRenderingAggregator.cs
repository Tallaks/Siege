using UnityEngine;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Movement.Tiles.Rendering
{
	public interface ITilesRenderingAggregator
	{
		void ChangeMaterial(CustomTile tile, Material material, int neighboursNumber);
	}
}