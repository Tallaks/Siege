using Kulinaria.Siege.Runtime.Infrastructure.Configs;
using UnityEngine;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Movement.Tiles.Rendering
{
	public class FiveNeighbourPropertyChanger : IMaterialPropertyChanger
	{
		private readonly TileSpritesConfig _config;

		public FiveNeighbourPropertyChanger(TileSpritesConfig config) => 
			_config = config;

		public void ChangeMaterial(CustomTile sourceTile, Material material)
		{
			
		}
	}
}