using System.Collections.Generic;
using System.Linq;
using Kulinaria.Siege.Runtime.Extensions;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Tiles.Rendering.UvRotators;
using Kulinaria.Siege.Runtime.Infrastructure.Configs;
using UnityEngine;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Tiles.Rendering
{
	public class FourNeighbourPropertyChanger : IMaterialPropertyChanger
	{
		private readonly TileSpritesConfig _config;

		public FourNeighbourPropertyChanger(TileSpritesConfig config) =>
			_config = config;

		public void ChangeMaterial(CustomTile sourceTile, Material material)
		{
			IEnumerable<CustomTile> diagonalNeighbours =
				sourceTile.ActiveNeighbours.Where(k => k.IsDiagonalPositionTo(sourceTile).Value);

			IEnumerable<CustomTile> sideNeighbours =
				sourceTile.ActiveNeighbours.Where(k => !k.IsDiagonalPositionTo(sourceTile).Value);

			IEnumerable<Vector2Int> missingTiles = sourceTile.MissingNeighboursPositions();

			IUvRotator rotator;

			if (!diagonalNeighbours.Any())
			{
				material.SetTexture(TileRenderer.TileTex, _config.Tile4_4_0);
				return;
			}

			if (diagonalNeighbours.Count() == 4)
			{
				material.SetTexture(TileRenderer.TileTex, _config.Tile0_4_4);
				return;
			}

			if (diagonalNeighbours.Count() == 3)
			{
				material.SetTexture(TileRenderer.TileTex, _config.Tile1_3_4);
				Vector2Int sideTile = sideNeighbours.First().CellPosition;
				rotator = new Uv1_3_4Rotator(sideTile);
				material.SetFloat(TileRenderer.AngleProperty, rotator.AngleDeg(sourceTile));
				return;
			}

			CustomTile[] sideArray = sideNeighbours.ToArray();
			Vector2Int diagonalBetweenSides;
			if (diagonalNeighbours.Count() == 1)
			{
				Vector2Int missingSide = missingTiles
					.First(k => !k.IsDiagonalPositionTo(sourceTile.CellPosition).Value);

				var diagonalBetweenTiles = new Vector2Int[2];
				var diagonalIsBetweenSides = false;
				var j = 0;
				for (var i = 0; i < 3; i++)
				{
					int next = i != 2 ? i + 1 : 0;

					Vector2Int sum = sideArray[i].CellPosition + sideArray[next].CellPosition;
					if (sum == 2 * sourceTile.CellPosition)
						continue;

					diagonalBetweenSides = sum - sourceTile.CellPosition;

					diagonalBetweenTiles[j] = diagonalBetweenSides;
					j++;

					if (!missingTiles.Contains(diagonalBetweenSides))
						diagonalIsBetweenSides = true;
				}

				if (diagonalIsBetweenSides)
				{
					material.SetTexture(TileRenderer.TileTex, _config.Tile4_2_2);
					Vector2Int missingDiagonal
						= diagonalBetweenTiles.First(k => missingTiles.Contains(k));
					var transformer = new Uv4_2_2Transformer(missingDiagonal, missingSide);
					material.SetFloat(TileRenderer.AngleProperty, transformer.AngleDeg(sourceTile));
					material.SetInt(TileRenderer.FlipProperty, transformer.GetFlip(sourceTile));
					return;
				}

				material.SetTexture(TileRenderer.TileTex, _config.Tile3_3_2);
				rotator = new Uv3_3_2Rotator(missingSide);
				material.SetFloat(TileRenderer.AngleProperty, rotator.AngleDeg(sourceTile));
				return;
			}

			CustomTile sideTile0 = sideArray[0];
			CustomTile sideTile1 = sideArray[1];
			if (sideTile0.CellPosition + sideTile1.CellPosition == sourceTile.CellPosition * 2)
			{
				material.SetTexture(TileRenderer.TileTex, _config.Tile2_2_4);
				rotator = new Uv2_2_4Rotator(sideTile0.CellPosition);
				material.SetFloat(TileRenderer.AngleProperty, rotator.AngleDeg(sourceTile));
				return;
			}

			diagonalBetweenSides = sideTile0.CellPosition + sideTile1.CellPosition - sourceTile.CellPosition;
			if (missingTiles.Contains(diagonalBetweenSides))
			{
				material.SetTexture(TileRenderer.TileTex, _config.Tile2_3_3);
				rotator = new Uv2_3_3Rotator(diagonalBetweenSides);
				material.SetFloat(TileRenderer.AngleProperty, rotator.AngleDeg(sourceTile));
				return;
			}

			material.SetTexture(TileRenderer.TileTex, _config.Tile3_2_3);
			rotator = new Uv3_2_3Rotator(diagonalBetweenSides);
			material.SetFloat(TileRenderer.AngleProperty, rotator.AngleDeg(sourceTile));
		}
	}
}