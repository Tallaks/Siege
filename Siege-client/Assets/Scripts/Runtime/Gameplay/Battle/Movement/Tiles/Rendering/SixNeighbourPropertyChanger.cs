using System.Collections.Generic;
using System.Linq;
using Kulinaria.Siege.Runtime.Extensions;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Movement.Tiles.Rendering.UvRotators;
using Kulinaria.Siege.Runtime.Infrastructure.Configs;
using UnityEngine;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Movement.Tiles.Rendering
{
	public class SixNeighbourPropertyChanger : IMaterialPropertyChanger
	{
		private readonly TileSpritesConfig _config;

		public SixNeighbourPropertyChanger(TileSpritesConfig config) => 
			_config = config;

		public void ChangeMaterial(CustomTile sourceTile, Material material)
		{
			IEnumerable<CustomTile> diagonalNeighbours =
				sourceTile.NeighboursWithDistances.Keys.Where(k => k.IsDiagonalPositionTo(sourceTile).Value);

			int diagonalCount = diagonalNeighbours.Count();

			IEnumerable<Vector2Int> missingTilePositions = sourceTile.OfMissingNeighboursPositions();
			if (diagonalCount == 3)
			{
				Vector2Int missingSide = missingTilePositions.
					First(k => !k.IsDiagonalPositionTo(sourceTile.CellPosition).Value);

				Vector2Int missingDiagonal = missingTilePositions
					.First(k => k.IsDiagonalPositionTo(sourceTile.CellPosition).Value);

				if (missingDiagonal.x == missingSide.x || missingDiagonal.y == missingSide.y)
				{
					material.SetTexture(TileRenderer.TileTex, _config.Tile5_1_2);
					var rotator = new Uv5_1_2Rotator(missingSide);
					material.SetFloat(TileRenderer.AngleProperty, rotator.AngleDeg(sourceTile));
					return;
				}
				else
				{
					material.SetTexture(TileRenderer.TileTex, _config.Tile4_2_2);

					var transformer = new Uv4_2_2Transformer(missingDiagonal, missingSide);
					material.SetFloat(TileRenderer.AngleProperty, transformer.AngleDeg(sourceTile));
					material.SetInt(TileRenderer.FlipProperty, transformer.GetFlip(sourceTile));
					return;
				}
			}

			if (diagonalCount == 2)
			{
				Vector2Int[] diagonalPositions = missingTilePositions.ToArray();
				Vector2Int neighbour0 = diagonalPositions[0];
				Vector2Int neighbour1 = diagonalPositions[1];

				if ((neighbour0 + neighbour1 - 2 * sourceTile.CellPosition).magnitude == 0)
				{
					material.SetTexture(TileRenderer.TileTex, _config.Tile6_2_0b);
					var rotator = new Uv6_2_0bRotator(neighbour0);
					material.SetFloat(TileRenderer.AngleProperty, rotator.AngleDeg(sourceTile));
					return;
				}
				else
				{
					material.SetTexture(TileRenderer.TileTex, _config.Tile6_2_0a);
					var rotator = new Uv6_2_0aRotator(neighbour0, neighbour1);
					material.SetFloat(TileRenderer.AngleProperty, rotator.AngleDeg(sourceTile));
					return;
				}
			}

			if (diagonalCount == 4)
			{
				Vector2Int[] diagonalPositions = missingTilePositions.ToArray();
				Vector2Int missingNeighbour0 = diagonalPositions[0];
				Vector2Int missingNeighbour1 = diagonalPositions[1];

				Vector2Int missingNeighbourPosSum = missingNeighbour0 + missingNeighbour1 - 2 * sourceTile.CellPosition;
				if (missingNeighbourPosSum.magnitude == 0)
				{
					CustomTile nonDiagonalNeighbour =
						sourceTile.NeighboursWithDistances.Keys.First(k => !k.IsDiagonalPositionTo(sourceTile).Value);
					
					material.SetTexture(TileRenderer.TileTex, _config.Tile2_2_4);
					var rotator = new Uv2_2_4Rotator(nonDiagonalNeighbour.CellPosition);
					material.SetFloat(TileRenderer.AngleProperty, rotator.AngleDeg(sourceTile));
					return;
				}
				else
				{
					material.SetTexture(TileRenderer.TileTex, _config.Tile3_2_3);
					var rotator = new Uv3_2_3Rotator(sourceTile[-missingNeighbourPosSum.x, -missingNeighbourPosSum.y]);
					material.SetFloat(TileRenderer.AngleProperty, rotator.AngleDeg(sourceTile));
					return;
				}
			}
		}
	}
}