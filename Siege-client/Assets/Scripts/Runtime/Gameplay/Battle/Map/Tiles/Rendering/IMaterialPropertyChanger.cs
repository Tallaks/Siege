using UnityEngine;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Tiles.Rendering
{
	public interface IMaterialPropertyChanger
	{
		void ChangeMaterial(CustomTile sourceTile, Material material);
	}
}