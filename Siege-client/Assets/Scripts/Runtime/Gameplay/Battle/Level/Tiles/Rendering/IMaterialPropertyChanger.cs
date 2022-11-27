using UnityEngine;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Level.Tiles.Rendering
{
	public interface IMaterialPropertyChanger
	{
		void ChangeMaterial(CustomTile sourceTile, Material material);
	}
}