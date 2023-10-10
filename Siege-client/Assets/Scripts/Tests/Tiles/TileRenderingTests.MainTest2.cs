using System.Collections;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Tiles;
using Kulinaria.Siege.Runtime.Infrastructure.Configs;
using UnityEngine;
using UnityEngine.TestTools;

namespace Kulinaria.Siege.Tests.Tiles
{
	public partial class TileRenderingTests
	{
		[UnityTest]
		public IEnumerator WhenBigMap2Generated_ThenTilesAreCorrect()
		{
			var config = Resources.Load<TileSpritesConfig>("Configs/TileRules");
			Runtime.Gameplay.Battle.Prototype.ArrayGridMap.GridArray = new[,]
			{
				{ 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 1, 0, 1, 0, 1, 0, 0, 0, 0, 1, 0, 1, 1, 0, 0, 1 },
				{ 0, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 0, 1, 1, 1, 1, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 1, 1 },
				{ 0, 1, 1, 0, 0, 0, 1, 0, 1, 0, 1, 0, 0, 0, 0, 1, 0, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 0 },
				{ 0, 1, 0, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 0, 0, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1 },
				{ 0, 0, 1, 0, 1, 1, 1, 0, 0, 0, 1, 1, 0, 1, 1, 1, 0, 0, 0, 0, 1, 0, 1, 0, 1, 0, 1, 0 },
				{ 0, 1, 1, 1, 0, 1, 0, 0, 1, 1, 0, 1, 1, 1, 1, 0, 1, 1, 0, 1, 0, 0, 0, 1, 0, 1, 0, 0 },
				{ 1, 0, 1, 1, 0, 1, 0, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 0, 1, 1, 0, 1 },
				{ 0, 1, 1, 1, 1, 0, 1, 1, 1, 0, 1, 1, 0, 0, 1, 0, 1, 0, 1, 1, 0, 1, 1, 0, 1, 0, 1, 1 },
				{ 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 0, 1, 1, 0, 1, 1, 0, 0, 1 },
				{ 0, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 0 },
				{ 1, 1, 1, 0, 1, 1, 0, 1, 1, 1, 0, 1, 0, 1, 1, 1, 0, 1, 1, 1, 0, 1, 1, 1, 0, 1, 1, 0 },
				{ 0, 0, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 0, 0, 0, 1, 1, 0, 1, 1, 1, 1, 1, 0, 1, 0, 0, 1 },
				{ 0, 0, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 0, 1, 0, 1, 0, 1, 1, 1, 0 },
				{ 0, 1, 0, 1, 1, 0, 0, 1, 0, 1, 0, 0, 1, 1, 0, 0, 1, 1, 1, 0, 1, 1, 0, 0, 1, 0, 1, 1 },
				{ 0, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 0, 1, 1, 0, 0 },
				{ 0, 0, 1, 1, 1, 0, 1, 0, 1, 0, 1, 1, 0, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 1, 0, 1, 0, 0 },
				{ 0, 1, 1, 0, 1, 0, 1, 0, 0, 1, 0, 1, 1, 0, 1, 0, 1, 0, 0, 1, 1, 0, 1, 1, 1, 1, 0, 0 },
				{ 0, 0, 1, 0, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 0, 0, 0, 1, 0, 1 },
				{ 0, 1, 1, 0, 1, 0, 1, 0, 0, 1, 1, 1, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 1, 1, 0, 0, 1, 1 },
				{ 0, 1, 1, 0, 1, 0, 1, 1, 1, 0, 0, 1, 1, 0, 1, 1, 1, 1, 0, 1, 1, 1, 1, 0, 1, 0, 1, 0 },
				{ 1, 0, 0, 1, 1, 1, 0, 1, 0, 0, 0, 0, 1, 1, 0, 0, 1, 1, 0, 1, 1, 0, 0, 1, 0, 1, 0, 1 }
			};

			PrepareTiles();
			_gridMap.GenerateMap();

			foreach (CustomTile tile in _gridMap.AllTiles)
				tile.Active = true;

			foreach (CustomTile tile in _gridMap.AllTiles)
				tile.Renderer.Repaint();

			yield return AssertTileParams(new Vector2Int(13, 11), config.Tile7_1_0, 90f);
			yield return AssertTileParams(new Vector2Int(14, 10), config.Tile5_1_2, 90f);
			yield return AssertTileParams(new Vector2Int(13, 12), config.Tile5_1_2, 270f);
			yield return AssertTileParams(new Vector2Int(14, 12), config.Tile6_2_0a);
			yield return AssertTileParams(new Vector2Int(15, 11), config.Tile6_2_0a, 270f);
			yield return AssertTileParams(new Vector2Int(11, 11), config.Tile6_2_0a, 180f);
			yield return AssertTileParams(new Vector2Int(9, 11), config.Tile6_2_0b, 90f);
			yield return AssertTileParams(new Vector2Int(14, 13), config.Tile2_2_4);
			yield return AssertTileParams(new Vector2Int(16, 11), config.Tile2_2_4, 90f);
			yield return AssertTileParams(new Vector2Int(9, 12), config.Tile3_2_3, 90f);
			yield return AssertTileParams(new Vector2Int(15, 12), config.Tile3_2_3);
			yield return AssertTileParams(new Vector2Int(19, 13), config.Tile3_2_3, 270f);
			yield return AssertTileParams(new Vector2Int(17, 10), config.Tile3_2_3, 180f);
			yield return AssertTileParams(new Vector2Int(18, 12), config.Tile4_2_2, 180f);
			yield return AssertTileParams(new Vector2Int(18, 14), config.Tile4_2_2, 270f);
			yield return AssertTileParams(new Vector2Int(18, 13), config.Tile4_2_2);
			yield return AssertTileParams(new Vector2Int(17, 14), config.Tile4_2_2, 90f);
			yield return AssertTileParams(new Vector2Int(15, 10), config.Tile4_2_2, 180f, 1);
			yield return AssertTileParams(new Vector2Int(19, 11), config.Tile4_2_2, 270f, 1);
			yield return AssertTileParams(new Vector2Int(15, 9), config.Tile4_2_2, 0f, 1);
			yield return AssertTileParams(new Vector2Int(21, 9), config.Tile4_2_2, 90f, 1);
			yield return AssertTileParams(new Vector2Int(7, 11), config.Tile5_3_0);
			yield return AssertTileParams(new Vector2Int(16, 14), config.Tile5_3_0, 90f);
			yield return AssertTileParams(new Vector2Int(19, 14), config.Tile5_3_0, 270f);
			yield return AssertTileParams(new Vector2Int(5, 12), config.Tile1_3_4);
			yield return AssertTileParams(new Vector2Int(6, 13), config.Tile1_3_4, 90f);
			yield return AssertTileParams(new Vector2Int(4, 13), config.Tile1_3_4, 270f);
			yield return AssertTileParams(new Vector2Int(23, 11), config.Tile4_2_2);
			yield return AssertTileParams(new Vector2Int(9, 8), config.Tile4_2_2);
			yield return AssertTileParams(new Vector2Int(11, 8), config.Tile4_2_2, 90f);
			yield return AssertTileParams(new Vector2Int(5, 8), config.Tile4_2_2, 90f);
			yield return AssertTileParams(new Vector2Int(18, 8), config.Tile4_2_2, 180f);
			yield return AssertTileParams(new Vector2Int(10, 6), config.Tile4_2_2, 270f);
			yield return AssertTileParams(new Vector2Int(20, 7), config.Tile4_2_2, 0f, 1);
			yield return AssertTileParams(new Vector2Int(16, 6), config.Tile4_2_2, 90f, 1);
			yield return AssertTileParams(new Vector2Int(22, 9), config.Tile4_2_2, 180f, 1);
			yield return AssertTileParams(new Vector2Int(21, 11), config.Tile4_2_2, 180f, 1);
			yield return AssertTileParams(new Vector2Int(11, 6), config.Tile4_2_2, 270f, 1);
			yield return AssertTileParams(new Vector2Int(2, 5), config.Tile3_3_2);
			yield return AssertTileParams(new Vector2Int(7, 6), config.Tile3_3_2, 90f);
			yield return AssertTileParams(new Vector2Int(20, 9), config.Tile3_3_2, 270f);
			yield return AssertTileParams(new Vector2Int(20, 8), config.Tile2_2_4);
			yield return AssertTileParams(new Vector2Int(11, 10), config.Tile2_2_4);
			yield return AssertTileParams(new Vector2Int(7, 7), config.Tile2_2_4);
			yield return AssertTileParams(new Vector2Int(9, 7), config.Tile2_2_4);
			yield return AssertTileParams(new Vector2Int(10, 14), config.Tile2_3_3);
			yield return AssertTileParams(new Vector2Int(21, 13), config.Tile2_3_3, 90f);
			yield return AssertTileParams(new Vector2Int(11, 15), config.Tile2_3_3, 180f);
			yield return AssertTileParams(new Vector2Int(15, 16), config.Tile2_3_3, 270f);
			yield return AssertTileParams(new Vector2Int(2, 8), config.Tile3_2_3, 180f);
			yield return AssertTileParams(new Vector2Int(12, 14), config.Tile3_2_3, 180f);
			yield return AssertTileParams(new Vector2Int(7, 14), config.Tile3_2_3, 90f);
			yield return AssertTileParams(new Vector2Int(13, 16), config.Tile3_2_3, 90f);
			yield return AssertTileParams(new Vector2Int(4, 11), config.Tile3_2_3, 90f);
			yield return AssertTileParams(new Vector2Int(9, 15), config.Tile3_2_3);
			yield return AssertTileParams(new Vector2Int(3, 15), config.Tile3_2_3);
			yield return AssertTileParams(new Vector2Int(6, 16), config.Tile3_2_3, 270f);
			yield return AssertTileParams(new Vector2Int(18, 7), config.Tile3_2_3, 270f);
			yield return AssertTileParams(new Vector2Int(17, 6), config.Tile3_2_3, 270f);
			yield return AssertTileParams(new Vector2Int(16, 4), config.Tile1_3_4);
			yield return AssertTileParams(new Vector2Int(14, 4), config.Tile1_3_4);
			yield return AssertTileParams(new Vector2Int(24, 9), config.Tile1_3_4);
			yield return AssertTileParams(new Vector2Int(9, 4), config.Tile1_3_4);
			yield return AssertTileParams(new Vector2Int(16, 17), config.Tile1_3_4, 270f);
			yield return AssertTileParams(new Vector2Int(9, 17), config.Tile1_3_4, 270f);
			yield return AssertTileParams(new Vector2Int(20, 4), config.Tile1_3_4, 270f);
			yield return AssertTileParams(new Vector2Int(20, 14), config.Tile1_3_4, 270f);
			yield return AssertTileParams(new Vector2Int(20, 16), config.Tile1_3_4, 180f);
			yield return AssertTileParams(new Vector2Int(8, 5), config.Tile1_3_4, 180f);
			yield return AssertTileParams(new Vector2Int(24, 16), config.Tile1_3_4, 180f);
			yield return AssertTileParams(new Vector2Int(26, 1), config.Tile1_3_4, 180f);
			yield return AssertTileParams(new Vector2Int(10, 16), config.Tile1_3_4, 90f);
			yield return AssertTileParams(new Vector2Int(22, 4), config.Tile1_3_4, 90f);
			yield return AssertTileParams(new Vector2Int(1, 15), config.Tile1_3_4, 90f);
			yield return AssertTileParams(new Vector2Int(26, 13), config.Tile1_3_4, 90f);
			yield return AssertTileParams(new Vector2Int(1, 18), config.Tile4_2_2);
			yield return AssertTileParams(new Vector2Int(4, 17), config.Tile4_2_2, 270f);
			yield return AssertTileParams(new Vector2Int(4, 19), config.Tile4_2_2, 90f, 1);
			yield return AssertTileParams(new Vector2Int(6, 19), config.Tile4_2_2, 180f, 1);
			yield return AssertTileParams(new Vector2Int(2, 19), config.Tile4_2_2, 270f, 1);
			yield return AssertTileParams(new Vector2Int(26, 17), config.Tile3_3_2);
			yield return AssertTileParams(new Vector2Int(15, 17), config.Tile3_3_2);
			yield return AssertTileParams(new Vector2Int(16, 3), config.Tile3_3_2, 90f);
			yield return AssertTileParams(new Vector2Int(14, 3), config.Tile3_3_2, 90f);
			yield return AssertTileParams(new Vector2Int(7, 8), config.Tile3_3_2, 180f);
			yield return AssertTileParams(new Vector2Int(9, 3), config.Tile3_3_2, 180f);
			yield return AssertTileParams(new Vector2Int(6, 6), config.Tile3_3_2, 270f);
			yield return AssertTileParams(new Vector2Int(7, 1), config.Tile3_3_2, 270f);
			yield return AssertTileParams(new Vector2Int(6, 5), config.Tile2_2_4);
			yield return AssertTileParams(new Vector2Int(4, 4), config.Tile2_2_4);
			yield return AssertTileParams(new Vector2Int(2, 3), config.Tile2_2_4);
			yield return AssertTileParams(new Vector2Int(24, 7), config.Tile2_2_4);
			yield return AssertTileParams(new Vector2Int(4, 1), config.Tile2_2_4);
			yield return AssertTileParams(new Vector2Int(24, 13), config.Tile2_2_4);
			yield return AssertTileParams(new Vector2Int(9, 19), config.Tile2_2_4, 90f);
			yield return AssertTileParams(new Vector2Int(13, 3), config.Tile2_2_4, 90f);
			yield return AssertTileParams(new Vector2Int(14, 6), config.Tile2_2_4, 90f);
			yield return AssertTileParams(new Vector2Int(7, 3), config.Tile2_2_4, 90f);
			yield return AssertTileParams(new Vector2Int(8, 3), config.Tile2_2_4, 90f);
			yield return AssertTileParams(new Vector2Int(12, 1), config.Tile2_3_3);
			yield return AssertTileParams(new Vector2Int(15, 19), config.Tile2_3_3);
			yield return AssertTileParams(new Vector2Int(11, 16), config.Tile2_3_3);
			yield return AssertTileParams(new Vector2Int(26, 19), config.Tile2_3_3, 90f);
			yield return AssertTileParams(new Vector2Int(26, 2), config.Tile2_3_3, 90f);
			yield return AssertTileParams(new Vector2Int(19, 4), config.Tile2_3_3, 90f);
			yield return AssertTileParams(new Vector2Int(26, 7), config.Tile2_3_3, 180f);
			yield return AssertTileParams(new Vector2Int(11, 1), config.Tile2_3_3, 180f);
			yield return AssertTileParams(new Vector2Int(24, 6), config.Tile2_3_3, 180f);
			yield return AssertTileParams(new Vector2Int(19, 3), config.Tile2_3_3, 270f);
			yield return AssertTileParams(new Vector2Int(22, 1), config.Tile2_3_3, 270f);
			yield return AssertTileParams(new Vector2Int(21, 19), config.Tile2_3_3, 270f);
			yield return AssertTileParams(new Vector2Int(17, 1), config.Tile3_2_3);
			yield return AssertTileParams(new Vector2Int(19, 1), config.Tile3_2_3, 90f);
			yield return AssertTileParams(new Vector2Int(1, 1), config.Tile3_2_3, 180f);
			yield return AssertTileParams(new Vector2Int(20, 0), config.Tile3_2_3, 270f);
			yield return AssertTileParams(new Vector2Int(26, 10), config.Tile3_2_3, 270f);
			yield return AssertTileParams(new Vector2Int(24, 1), config.Tile0_4_4);
			yield return AssertTileParams(new Vector2Int(18, 5), config.Tile0_4_4);
			yield return AssertTileParams(new Vector2Int(25, 3), config.Tile1_3_4, 180f);
			yield return AssertTileParams(new Vector2Int(10, 18), config.Tile1_3_4, 180f);
			yield return AssertTileParams(new Vector2Int(1, 17), config.Tile1_3_4, 180f);
			yield return AssertTileParams(new Vector2Int(26, 16), config.Tile1_3_4, 180f);
			yield return AssertTileParams(new Vector2Int(5, 14), config.Tile1_3_4, 180f);
			yield return AssertTileParams(new Vector2Int(3, 0), config.Tile1_3_4, 90f);
			yield return AssertTileParams(new Vector2Int(14, 1), config.Tile1_3_4, 90f);
			yield return AssertTileParams(new Vector2Int(17, 18), config.Tile1_3_4, 90f);
			yield return AssertTileParams(new Vector2Int(1, 7), config.Tile1_3_4);
			yield return AssertTileParams(new Vector2Int(13, 0), config.Tile1_3_4, 270f);
		}
	}
}