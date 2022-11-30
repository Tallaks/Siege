using System.Linq;
using Kulinaria.Siege.Runtime.Extensions;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Tiles.Rendering.UvRotators;
using Kulinaria.Siege.Runtime.Infrastructure.Configs;
using UnityEngine;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Tiles.Rendering
{
	public class OneNeighbourPropertyChanger : IMaterialPropertyChanger
	{
		private readonly TileSpritesConfig _config;

		public OneNeighbourPropertyChanger(TileSpritesConfig config) =>
			_config = config;

		public void ChangeMaterial(CustomTile sourceTile, Material material)
		{
			Vector2Int tilePos = sourceTile.ActiveNeighbours.First().CellPosition;

			if (tilePos.IsDiagonalPositionTo(sourceTile.CellPosition).HasValue)
				if (tilePos.IsDiagonalPositionTo(sourceTile.CellPosition).Value)
				{
					material.SetTexture(TileRenderer.TileTex, _config.Tile0_4_4);
					return;
				}

			material.SetTexture(TileRenderer.TileTex, _config.Tile1_3_4);

			var rotator = new Uv1_3_4Rotator(tilePos);
			material.SetFloat(TileRenderer.AngleProperty, rotator.AngleDeg(sourceTile));
		}
	}
}