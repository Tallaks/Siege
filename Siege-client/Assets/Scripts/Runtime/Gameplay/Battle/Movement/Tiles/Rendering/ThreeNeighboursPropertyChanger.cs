using System.Linq;
using Kulinaria.Siege.Runtime.Extensions;
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
				Vector2Int sum = allNeighbours.Aggregate(Vector2Int.zero, (current, tile) => current + tile.CellPosition - sourceTile.CellPosition);
				if (sum.y == 0)
				{
					material.SetFloat(TileRenderer.AngleProperty, sum.x == 1 ? 0f : 180f);
					return;
				}

				if (sum.x == 0)
				{
					material.SetFloat(TileRenderer.AngleProperty, sum.y == 1 ? 90f : 270f);
					return;
				}
			}

			if (nonDiagonalNeighbours.Length == 1)
			{
				material.SetTexture(TileRenderer.TileTex, _config.Tile1_3_4);
				Vector2Int tilePos = nonDiagonalNeighbours[0].CellPosition;

				if (sourceTile[0, 1] == tilePos)
					material.SetFloat(TileRenderer.AngleProperty, 180f);

				if (sourceTile[0, -1] == tilePos)
					material.SetFloat(TileRenderer.AngleProperty, 0f);

				if (sourceTile[-1, 0] == tilePos)
					material.SetFloat(TileRenderer.AngleProperty, 270f);

				if (sourceTile[1, 0] == tilePos)
					material.SetFloat(TileRenderer.AngleProperty, 90f);
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
					if(neighbour1.CellPosition.x == sourceTile.CellPosition.x)
						material.SetFloat(TileRenderer.AngleProperty, 0f);
					else
						material.SetFloat(TileRenderer.AngleProperty, 90f);
					return;
				}

				Vector2Int diagonalCellPosition = diagonalNeighbour.CellPosition;
				if (diagonalCellPosition == neighbour1.CellPosition + neighbour2.CellPosition - sourceTile.CellPosition)
				{
					material.SetTexture(TileRenderer.TileTex, _config.Tile3_2_3);
					
					if(diagonalCellPosition == sourceTile[-1, -1])
						material.SetFloat(TileRenderer.AngleProperty, 0f);
					if(diagonalCellPosition == sourceTile[1, -1])
						material.SetFloat(TileRenderer.AngleProperty, 90f);
					if(diagonalCellPosition == sourceTile[1, 1])
						material.SetFloat(TileRenderer.AngleProperty, 180f);
					if(diagonalCellPosition == sourceTile[-1, 1])
						material.SetFloat(TileRenderer.AngleProperty, 270f);
					
					return;
				}

				Vector2Int missingBetweenTilesPosition = neighbour1.CellPosition + neighbour2.CellPosition - sourceTile.CellPosition;
				material.SetTexture(TileRenderer.TileTex, _config.Tile2_3_3);
					
				if(missingBetweenTilesPosition == sourceTile[-1, -1])
					material.SetFloat(TileRenderer.AngleProperty, 0f);
				if(missingBetweenTilesPosition == sourceTile[1, -1])
					material.SetFloat(TileRenderer.AngleProperty, 90f);
				if(missingBetweenTilesPosition == sourceTile[1, 1])
					material.SetFloat(TileRenderer.AngleProperty, 180f);
				if(missingBetweenTilesPosition == sourceTile[-1, 1])
					material.SetFloat(TileRenderer.AngleProperty, 270f);
			}
		}
	}
}