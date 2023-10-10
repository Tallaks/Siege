using Kulinaria.Siege.Runtime.Infrastructure.Configs;
using UnityEngine;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Tiles.Rendering
{
	public class EightNeighboursPropertyChanger : IMaterialPropertyChanger
	{
		private readonly TileSpritesConfig _config;

		public EightNeighboursPropertyChanger(TileSpritesConfig config) =>
			_config = config;

		public void ChangeMaterial(CustomTile sourceTile, Material material)
		{
			material.SetTexture(TileRenderer.TileTex, _config.Tile8_0_0);
		}
	}
}