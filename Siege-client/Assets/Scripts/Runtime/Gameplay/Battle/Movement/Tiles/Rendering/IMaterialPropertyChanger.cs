using UnityEngine;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Movement.Tiles.Rendering
{
	public interface IMaterialPropertyChanger
	{
		void ChangeMaterial(CustomTile sourceTile, Material material);
	}
}