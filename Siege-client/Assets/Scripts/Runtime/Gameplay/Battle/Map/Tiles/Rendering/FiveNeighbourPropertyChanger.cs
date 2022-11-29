using System.Collections.Generic;
using System.Linq;
using Kulinaria.Siege.Runtime.Extensions;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Tiles.Rendering.UvRotators;
using Kulinaria.Siege.Runtime.Infrastructure.Configs;
using UnityEngine;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Tiles.Rendering
{
	public class FiveNeighbourPropertyChanger : IMaterialPropertyChanger
	{
		private readonly TileSpritesConfig _config;

		public FiveNeighbourPropertyChanger(TileSpritesConfig config) =>
			_config = config;

		public void ChangeMaterial(CustomTile sourceTile, Material material)
		{
			IEnumerable<CustomTile> diagonalNeighbours =
				sourceTile.ActiveNeighbours.Where(k => k.IsDiagonalPositionTo(sourceTile).Value);

			IEnumerable<CustomTile> sideNeighbours =
				sourceTile.ActiveNeighbours.Where(k => !k.IsDiagonalPositionTo(sourceTile).Value);

			if (diagonalNeighbours.Count() == 1)
			{
				Vector2Int diagonalPos = diagonalNeighbours.First().CellPosition;

				material.SetTexture(TileRenderer.TileTex, _config.Tile5_3_0);
				var rotator = new Uv5_3_0Rotator(diagonalPos);
				material.SetFloat(TileRenderer.AngleProperty, rotator.AngleDeg(sourceTile));
				return;
			}

			if (diagonalNeighbours.Count() == 4)
			{
				Vector2Int sidePos = sideNeighbours.First().CellPosition;

				material.SetTexture(TileRenderer.TileTex, _config.Tile1_3_4);
				var rotator = new Uv1_3_4Rotator(sidePos);
				material.SetFloat(TileRenderer.AngleProperty, rotator.AngleDeg(sourceTile));
				return;
			}

			if (diagonalNeighbours.Count() == 2)
			{
				IEnumerable<Vector2Int> missingTiles = sourceTile.MissingNeighboursPositions();
				IEnumerable<Vector2Int> missingDiagonalTiles =
					missingTiles.Where(k => k.IsDiagonalPositionTo(sourceTile.CellPosition).Value);
				Vector2Int missingSideTile = missingTiles.First(k => !missingDiagonalTiles.Contains(k));

				if (missingTiles.Select(k => k.x).Distinct().Count() == 1 ||
				    missingTiles.Select(k => k.y).Distinct().Count() == 1)
				{
					material.SetTexture(TileRenderer.TileTex, _config.Tile5_1_2);

					var rotator = new Uv5_1_2Rotator(missingSideTile);
					material.SetFloat(TileRenderer.AngleProperty, rotator.AngleDeg(sourceTile));
					return;
				}

				if (missingDiagonalTiles.Any(k => k.x == missingSideTile.x || k.y == missingSideTile.y))
				{
					material.SetTexture(TileRenderer.TileTex, _config.Tile4_2_2);

					Vector2Int notNearDiagonal =
						missingDiagonalTiles.First(k => k.x != missingSideTile.x && k.y != missingSideTile.y);
					var transformer = new Uv4_2_2Transformer(notNearDiagonal, missingSideTile);
					material.SetFloat(TileRenderer.AngleProperty, transformer.AngleDeg(sourceTile));
					material.SetInt(TileRenderer.FlipProperty, transformer.GetFlip(sourceTile));
					return;
				}

				material.SetTexture(TileRenderer.TileTex, _config.Tile3_3_2);
				var uvRotator = new Uv3_3_2Rotator(missingSideTile);
				material.SetFloat(TileRenderer.AngleProperty, uvRotator.AngleDeg(sourceTile));
				return;
			}

			if (diagonalNeighbours.Count() == 3)
			{
				CustomTile[] sideArray = sideNeighbours.ToArray();
				CustomTile sideNeighbour0 = sideArray[0];
				CustomTile sideNeighbour1 = sideArray[1];

				if (!sideNeighbour0.IsDiagonalPositionTo(sideNeighbour1).HasValue)
				{
					material.SetTexture(TileRenderer.TileTex, _config.Tile2_2_4);
					var rotator = new Uv2_2_4Rotator(sideNeighbour0.CellPosition);
					material.SetFloat(TileRenderer.AngleProperty, rotator.AngleDeg(sourceTile));
					return;
				}

				Vector2Int diagonalBetweenSidesPos =
					sideNeighbour0.CellPosition + sideNeighbour1.CellPosition - sourceTile.CellPosition;

				if (!diagonalNeighbours.Select(k => k.CellPosition).Contains(diagonalBetweenSidesPos))
				{
					material.SetTexture(TileRenderer.TileTex, _config.Tile2_3_3);
					var rotator = new Uv2_3_3Rotator(diagonalBetweenSidesPos);
					material.SetFloat(TileRenderer.AngleProperty, rotator.AngleDeg(sourceTile));
					return;
				}

				material.SetTexture(TileRenderer.TileTex, _config.Tile3_2_3);
				var uvRotator = new Uv3_2_3Rotator(diagonalBetweenSidesPos);
				material.SetFloat(TileRenderer.AngleProperty, uvRotator.AngleDeg(sourceTile));
				return;
			}
		}
	}
}