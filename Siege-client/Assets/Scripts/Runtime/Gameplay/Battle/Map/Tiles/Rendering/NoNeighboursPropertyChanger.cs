using Kulinaria.Siege.Runtime.Infrastructure.Configs;
using UnityEngine;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Tiles.Rendering
{
	public class NoNeighboursPropertyChanger : IMaterialPropertyChanger
	{
		private readonly TileSpritesConfig _config;

		public NoNeighboursPropertyChanger(TileSpritesConfig config) => 
			_config = config;

		public void ChangeMaterial(CustomTile sourceTile, Material material) => 
			material.SetTexture(TileRenderer.TileTex, _config.Tile0_4_4);
	}
}