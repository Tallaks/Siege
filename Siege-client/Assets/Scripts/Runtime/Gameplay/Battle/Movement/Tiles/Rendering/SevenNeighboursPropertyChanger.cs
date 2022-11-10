using System.Linq;
using Kulinaria.Siege.Runtime.Extensions;
using Kulinaria.Siege.Runtime.Infrastructure.Configs;
using UnityEngine;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Movement.Tiles.Rendering
{
	public class SevenNeighboursPropertyChanger : IMaterialPropertyChanger
	{
		private readonly TileSpritesConfig _config;

		public SevenNeighboursPropertyChanger(TileSpritesConfig config) => 
			_config = config;

		public void ChangeMaterial(CustomTile sourceTile, Material material)
		{
			Vector2Int missingTilePos = sourceTile.OfMissingNeighboursPositions().First();

			if (sourceTile[-1, -1] == missingTilePos)
			{
				material.SetTexture(TileRenderer.TileTex, _config.Tile7_1_0);
				material.SetFloat(TileRenderer.AngleProperty, 90f);
			}

			if (sourceTile[-1, 1] == missingTilePos)
			{
				material.SetTexture(TileRenderer.TileTex, _config.Tile7_1_0);
				material.SetFloat(TileRenderer.AngleProperty, 0f);
			}

			if (sourceTile[1, 1] == missingTilePos)
			{
				material.SetTexture(TileRenderer.TileTex, _config.Tile7_1_0);
				material.SetFloat(TileRenderer.AngleProperty, 270f);
			}

			if (sourceTile[1, -1] == missingTilePos)
			{
				material.SetTexture(TileRenderer.TileTex, _config.Tile7_1_0);
				material.SetFloat(TileRenderer.AngleProperty, 180f);
			}

			if (sourceTile[0, 1] == missingTilePos)
			{
				material.SetTexture(TileRenderer.TileTex, _config.Tile5_1_2);
				material.SetFloat(TileRenderer.AngleProperty, 270f);
			}

			if (sourceTile[0, -1] == missingTilePos)
			{
				material.SetTexture(TileRenderer.TileTex, _config.Tile5_1_2);
				material.SetFloat(TileRenderer.AngleProperty, 90f);
			}

			if (sourceTile[-1, 0] == missingTilePos)
			{
				material.SetTexture(TileRenderer.TileTex, _config.Tile5_1_2);
				material.SetFloat(TileRenderer.AngleProperty, 0f);
			}

			if (sourceTile[1, 0] == missingTilePos)
			{
				material.SetTexture(TileRenderer.TileTex, _config.Tile5_1_2);
				material.SetFloat(TileRenderer.AngleProperty, 180f);
			}
		}
	}
}