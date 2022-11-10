using System.Linq;
using Kulinaria.Siege.Runtime.Extensions;
using Kulinaria.Siege.Runtime.Infrastructure.Configs;
using UnityEngine;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Movement.Tiles.Rendering
{
	public class OneNeighbourPropertyChanger : IMaterialPropertyChanger
	{
		private readonly TileSpritesConfig _config;

		public OneNeighbourPropertyChanger(TileSpritesConfig config) =>
			_config = config;

		public void ChangeMaterial(CustomTile sourceTile, Material material)
		{
			Vector2Int tilePos = sourceTile.NeighboursWithDistances.Keys.First().CellPosition;

			if (tilePos.IsDiagonalPositionTo(sourceTile.CellPosition).HasValue)
				if (tilePos.IsDiagonalPositionTo(sourceTile.CellPosition).Value)
				{
					material.SetTexture(TileRenderer.TileTex, _config.Tile0_4_4);
					return;
				}

			material.SetTexture(TileRenderer.TileTex, _config.Tile1_3_4);
			if (sourceTile[0, 1] == tilePos)
				material.SetFloat(TileRenderer.AngleProperty, 180f);

			if (sourceTile[0, -1] == tilePos)
				material.SetFloat(TileRenderer.AngleProperty, 0f);

			if (sourceTile[-1, 0] == tilePos)
				material.SetFloat(TileRenderer.AngleProperty, 270f);

			if (sourceTile[1, 0] == tilePos)
				material.SetFloat(TileRenderer.AngleProperty, 90f);
		}
	}
}