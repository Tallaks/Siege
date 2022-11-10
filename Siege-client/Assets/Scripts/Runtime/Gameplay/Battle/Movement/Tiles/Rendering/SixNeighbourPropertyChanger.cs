using Kulinaria.Siege.Runtime.Infrastructure.Configs;
using UnityEngine;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Movement.Tiles.Rendering
{
	public class SixNeighbourPropertyChanger : IMaterialPropertyChanger
	{
		private readonly TileSpritesConfig _config;

		public SixNeighbourPropertyChanger(TileSpritesConfig config) => 
			_config = config;

		public void ChangeMaterial(CustomTile sourceTile, Material material)
		{
		}
	}
}