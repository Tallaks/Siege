using System.Linq;
using Kulinaria.Siege.Runtime.Extensions;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Level.Tiles.Rendering.UvRotators;
using Kulinaria.Siege.Runtime.Infrastructure.Configs;
using UnityEngine;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Level.Tiles.Rendering
{
	public class SevenNeighboursPropertyChanger : IMaterialPropertyChanger
	{
		private readonly TileSpritesConfig _config;

		public SevenNeighboursPropertyChanger(TileSpritesConfig config) =>
			_config = config;

		public void ChangeMaterial(CustomTile sourceTile, Material material)
		{
			Vector2Int missingTilePos = sourceTile.MissingNeighboursPositions().First();
			if (missingTilePos.IsDiagonalPositionTo(sourceTile.CellPosition).HasValue)
			{
				if (missingTilePos.IsDiagonalPositionTo(sourceTile.CellPosition).Value)
				{
					material.SetTexture(TileRenderer.TileTex, _config.Tile7_1_0);
					var rotator = new Uv7_1_0Rotator(missingTilePos);
					material.SetFloat(TileRenderer.AngleProperty, rotator.AngleDeg(sourceTile));
				}
				else
				{
					material.SetTexture(TileRenderer.TileTex, _config.Tile5_1_2);
					var rotator = new Uv5_1_2Rotator(missingTilePos);
					material.SetFloat(TileRenderer.AngleProperty, rotator.AngleDeg(sourceTile));
				}
			}
		}
	}
}