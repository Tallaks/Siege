using System.Linq;
using Kulinaria.Siege.Runtime.Extensions;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Movement.Tiles.Rendering.UvRotators;
using Kulinaria.Siege.Runtime.Infrastructure.Configs;
using UnityEngine;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Movement.Tiles.Rendering
{
	public class ThreeNeighboursPropertyChanger : IMaterialPropertyChanger
	{
		private readonly TileSpritesConfig _config;

		public ThreeNeighboursPropertyChanger(TileSpritesConfig config) =>
			_config = config;

		public void ChangeMaterial(CustomTile sourceTile, Material material)
		{
			CustomTile[] allNeighbours = sourceTile.NeighboursWithDistances.Keys.ToArray();
			CustomTile[] nonDiagonalNeighbours =
				allNeighbours.Where(k => !k.IsDiagonalPositionTo(sourceTile).Value).ToArray();

			if (!nonDiagonalNeighbours.Any())
			{
				material.SetTexture(TileRenderer.TileTex, _config.Tile0_4_4);
				return;
			}

			if (allNeighbours.All(k => !k.IsDiagonalPositionTo(sourceTile).Value))
			{
				material.SetTexture(TileRenderer.TileTex, _config.Tile3_3_2);
				var rotator = new Uv3_3_2Rotator(allNeighbours);
				material.SetFloat(TileRenderer.AngleProperty, rotator.AngleDeg(sourceTile));
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

			if (nonDiagonalNeighbours.Length == 2)
			{
				CustomTile neighbour1 = nonDiagonalNeighbours[0];
				CustomTile neighbour2 = nonDiagonalNeighbours[1];
				CustomTile diagonalNeighbour = allNeighbours.First(k => k != neighbour1 && k != neighbour2);

				if ((neighbour1.CellPosition - neighbour2.CellPosition).magnitude == 2)
				{
					material.SetTexture(TileRenderer.TileTex, _config.Tile2_2_4);
					var rotator = new Uv2_2_4Rotator(neighbour1.CellPosition);
					material.SetFloat(TileRenderer.AngleProperty, rotator.AngleDeg(sourceTile));
					return;
				}

				Vector2Int diagonalCellPosition = diagonalNeighbour.CellPosition;
				if (diagonalCellPosition == neighbour1.CellPosition + neighbour2.CellPosition - sourceTile.CellPosition)
				{
					material.SetTexture(TileRenderer.TileTex, _config.Tile3_2_3);
					var rotator = new Uv3_2_3Rotator(diagonalCellPosition);
					material.SetFloat(TileRenderer.AngleProperty, rotator.AngleDeg(sourceTile));
					return;
				}

				material.SetTexture(TileRenderer.TileTex, _config.Tile2_3_3);
				var uvRotator = new Uv2_3_3Rotator(neighbour1.CellPosition, neighbour2.CellPosition);
				material.SetFloat(TileRenderer.AngleProperty, uvRotator.AngleDeg(sourceTile));
			}
		}
	}
}