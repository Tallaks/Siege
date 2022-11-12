using System.Linq;
using Kulinaria.Siege.Runtime.Extensions;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Movement.Tiles.Rendering.UvRotators;
using Kulinaria.Siege.Runtime.Infrastructure.Configs;
using UnityEngine;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Movement.Tiles.Rendering
{
	public class TwoNeighboursPropertyChanger : IMaterialPropertyChanger
	{
		private readonly TileSpritesConfig _config;

		public TwoNeighboursPropertyChanger(TileSpritesConfig config) =>
			_config = config;

		public void ChangeMaterial(CustomTile sourceTile, Material material)
		{
			CustomTile[] allNeighbours = sourceTile.NeighboursWithDistances.Keys.ToArray();
			CustomTile neighbour1 = allNeighbours[0];
			CustomTile neighbour2 = allNeighbours[1];

			CustomTile[] nonDiagonalNeighbours =
				allNeighbours.Where(k => !k.IsDiagonalPositionTo(sourceTile).Value).ToArray();

			if (!nonDiagonalNeighbours.Any())
			{
				material.SetTexture(TileRenderer.TileTex, _config.Tile0_4_4);
				return;
			}

			if (nonDiagonalNeighbours.Length == 1)
			{
				material.SetTexture(TileRenderer.TileTex, _config.Tile1_3_4);

				Vector2Int tilePos = nonDiagonalNeighbours[0].CellPosition;
				var rotator = new Uv1_3_4Rotator(tilePos);
				material.SetFloat(TileRenderer.AngleProperty, rotator.AngleDeg(sourceTile));
				return;
			}

			if ((neighbour1.CellPosition - neighbour2.CellPosition).magnitude == 2)
			{
				material.SetTexture(TileRenderer.TileTex, _config.Tile2_2_4);
				if (neighbour1.CellPosition.x == sourceTile.CellPosition.x)
					material.SetFloat(TileRenderer.AngleProperty, 0f);
				else
					material.SetFloat(TileRenderer.AngleProperty, 90f);
				return;
			}

			material.SetTexture(TileRenderer.TileTex, _config.Tile2_3_3);
			Vector2Int delta = neighbour1.CellPosition + neighbour2.CellPosition - 2 * sourceTile.CellPosition;
			if (delta.x == -1)
				material.SetFloat(TileRenderer.AngleProperty, delta.y == 1 ? 270f : 0f);
			else
				material.SetFloat(TileRenderer.AngleProperty, delta.y == 1 ? 180f : 90f);
		}
	}
}