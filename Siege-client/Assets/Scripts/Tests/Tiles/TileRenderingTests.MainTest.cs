using System.Collections;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Tiles;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Tiles.Rendering;
using Kulinaria.Siege.Runtime.Infrastructure.Configs;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Kulinaria.Siege.Tests.Tiles
{
	public partial class TileRenderingTests
	{
		private CustomTile _targetTile;
		private TileRenderer Renderer => _targetTile.GetComponent<TileRenderer>();

		[UnityTest]
		public IEnumerator WhenBigMapGenerated_ThenTilesAreCorrect()
		{
			var config = Resources.Load<TileSpritesConfig>("Configs/TileRules");
			Runtime.Gameplay.Battle.Prototype.ArrayGridMap.GridArray = new[,]
			{
				{ 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 0, 1, 1, 1, 1, 0, 1, 0, 0, 1, 0, 1, 0 },
				{ 0, 1, 1, 0, 1, 1, 1, 0, 0, 1, 1, 1, 1, 0, 1, 1 },
				{ 0, 0, 0, 0, 1, 1, 1, 1, 0, 1, 0, 1, 1, 0, 1, 1 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 1, 0, 1, 1, 0, 0, 1, 0, 0, 0, 1, 0, 0 },
				{ 0, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 1, 1, 1, 0 },
				{ 1, 1, 0, 0, 0, 1, 1, 1, 1, 0, 1, 0, 0, 1, 0, 0 },
				{ 0, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 0, 1, 0, 1, 0 },
				{ 0, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 0, 1, 1 },
				{ 0, 0, 1, 1, 0, 1, 1, 1, 1, 0, 0, 0, 0, 1, 0, 0 },
				{ 0, 1, 1, 1, 0, 0, 1, 0, 0, 0, 0, 1, 1, 0, 1, 1 },
				{ 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0 }
			};

			PrepareTiles();
			_gridMap.GenerateMap();

			foreach (CustomTile tile in _gridMap.AllTiles)
				tile.Active = true;

			yield return AssertTileParams(new Vector2Int(2 , 0 ), config.Tile3_2_3,  180f);
			yield return AssertTileParams(new Vector2Int(3 , 0 ), config.Tile3_2_3,  270f);
			yield return AssertTileParams(new Vector2Int(12, 0 ), config.Tile1_3_4,  180f);
			yield return AssertTileParams(new Vector2Int(14, 0 ), config.Tile1_3_4,  180f);
			yield return AssertTileParams(new Vector2Int(1 , 1 ), config.Tile1_3_4,   90f);
			yield return AssertTileParams(new Vector2Int(2 , 1 ), config.Tile6_2_0a,  90f);
			yield return AssertTileParams(new Vector2Int(3 , 1 ), config.Tile5_1_2,  180f);
			yield return AssertTileParams(new Vector2Int(6 , 1 ), config.Tile1_3_4,  180f);
			yield return AssertTileParams(new Vector2Int(11, 1 ), config.Tile1_3_4,   90f);
			yield return AssertTileParams(new Vector2Int(12, 1 ), config.Tile2_3_3                  );
			yield return AssertTileParams(new Vector2Int(14, 1 ), config.Tile2_3_3,   90f);
			yield return AssertTileParams(new Vector2Int(15, 1 ), config.Tile1_3_4,  270f);
			yield return AssertTileParams(new Vector2Int(2 , 2 ), config.Tile5_1_2                  );
			yield return AssertTileParams(new Vector2Int(3 , 2 ), config.Tile5_1_2,  180f);
			yield return AssertTileParams(new Vector2Int(5 , 2 ), config.Tile3_2_3,  180f);
			yield return AssertTileParams(new Vector2Int(6 , 2 ), config.Tile5_3_0,  180f);
			yield return AssertTileParams(new Vector2Int(7 , 2 ), config.Tile2_2_4,   90f);
			yield return AssertTileParams(new Vector2Int(8 , 2 ), config.Tile2_3_3,  270f);
			yield return AssertTileParams(new Vector2Int(13, 2 ), config.Tile0_4_4                  );
			yield return AssertTileParams(new Vector2Int(1 , 3 ), config.Tile3_2_3,  180f);
			yield return AssertTileParams(new Vector2Int(2 , 3 ), config.Tile6_2_0b                 );
			yield return AssertTileParams(new Vector2Int(3 , 3 ), config.Tile4_2_2, 270f,1);
			yield return AssertTileParams(new Vector2Int(4 , 3 ), config.Tile2_2_4,   90f);
			yield return AssertTileParams(new Vector2Int(6 , 3 ), config.Tile5_1_2,  180f);
			yield return AssertTileParams(new Vector2Int(8 , 3 ), config.Tile4_2_2                  );
			yield return AssertTileParams(new Vector2Int(9 , 3 ), config.Tile5_1_2,   90f);
			yield return AssertTileParams(new Vector2Int(10, 3 ), config.Tile4_2_2,   90f);
			yield return AssertTileParams(new Vector2Int(11, 3 ), config.Tile2_2_4,   90f);
			yield return AssertTileParams(new Vector2Int(12, 3 ), config.Tile2_3_3,  270f);
			yield return AssertTileParams(new Vector2Int(14, 3 ), config.Tile2_3_3,  180f);
			yield return AssertTileParams(new Vector2Int(15, 3 ), config.Tile1_3_4,  270f);
			yield return AssertTileParams(new Vector2Int(1 , 4 ), config.Tile4_2_2,    0f,1);
			yield return AssertTileParams(new Vector2Int(2 , 4 ), config.Tile3_2_3                  );
			yield return AssertTileParams(new Vector2Int(5 , 4 ), config.Tile5_1_2                  );
			yield return AssertTileParams(new Vector2Int(6 , 4 ), config.Tile7_1_0,  180f);
			yield return AssertTileParams(new Vector2Int(7 , 4 ), config.Tile5_1_2,   90f);
			yield return AssertTileParams(new Vector2Int(9 , 4 ), config.Tile5_1_2,  270f);
			yield return AssertTileParams(new Vector2Int(10, 4 ), config.Tile4_2_2,  180f);
			yield return AssertTileParams(new Vector2Int(12, 4 ), config.Tile1_3_4                  );
			yield return AssertTileParams(new Vector2Int(14, 4 ), config.Tile1_3_4                  );
			yield return AssertTileParams(new Vector2Int(1 , 5 ), config.Tile3_3_2,  180f);
			yield return AssertTileParams(new Vector2Int(5 , 5 ), config.Tile5_1_2                  );
			yield return AssertTileParams(new Vector2Int(6 , 5 ), config.Tile7_1_0,  270f);
			yield return AssertTileParams(new Vector2Int(7 , 5 ), config.Tile5_1_2,  270f);
			yield return AssertTileParams(new Vector2Int(8 , 5 ), config.Tile3_2_3                  );
			yield return AssertTileParams(new Vector2Int(10, 5 ), config.Tile1_3_4                  );
			yield return AssertTileParams(new Vector2Int(13, 5 ), config.Tile1_3_4,  180f);
			yield return AssertTileParams(new Vector2Int(1 , 6 ), config.Tile2_3_3,   90f);
			yield return AssertTileParams(new Vector2Int(2 , 6 ), config.Tile2_2_4,   90f);
			yield return AssertTileParams(new Vector2Int(3 , 6 ), config.Tile3_3_2,   90f);
			yield return AssertTileParams(new Vector2Int(4 , 6 ), config.Tile2_2_4,   90f);
			yield return AssertTileParams(new Vector2Int(6 , 6 ), config.Tile5_1_2,  180f);
			yield return AssertTileParams(new Vector2Int(13, 6 ), config.Tile4_4_0                  );
			yield return AssertTileParams(new Vector2Int(14, 6 ), config.Tile1_3_4,  270f);
			yield return AssertTileParams(new Vector2Int(3 , 7 ), config.Tile1_3_4                  );
			yield return AssertTileParams(new Vector2Int(5 , 7 ), config.Tile3_2_3,   90f);
			yield return AssertTileParams(new Vector2Int(6 , 7 ), config.Tile3_2_3                  );
			yield return AssertTileParams(new Vector2Int(9 , 7 ), config.Tile0_4_4                  );
			yield return AssertTileParams(new Vector2Int(4 , 9 ), config.Tile3_2_3,  180f);
			yield return AssertTileParams(new Vector2Int(5 , 9 ), config.Tile5_1_2,   90f);
			yield return AssertTileParams(new Vector2Int(1 , 10), config.Tile1_3_4,   90f);
			yield return AssertTileParams(new Vector2Int(2 , 10), config.Tile1_3_4,  270f);
			yield return AssertTileParams(new Vector2Int(4 , 10), config.Tile5_1_2                  );
			yield return AssertTileParams(new Vector2Int(5 , 10), config.Tile8_0_0                  );
			yield return AssertTileParams(new Vector2Int(10, 10), config.Tile2_2_4,   90f);
			yield return AssertTileParams(new Vector2Int(11, 10), config.Tile4_2_2,  270f);
			yield return AssertTileParams(new Vector2Int(12, 10), config.Tile4_2_2,  180f);
			yield return AssertTileParams(new Vector2Int(14, 10), config.Tile4_2_2,    0f, 1);
			yield return AssertTileParams(new Vector2Int(15, 10), config.Tile3_2_3                  );
			yield return AssertTileParams(new Vector2Int(4 , 11), config.Tile3_2_3,   90f);
			yield return AssertTileParams(new Vector2Int(5 , 11), config.Tile7_1_0                  );
			yield return AssertTileParams(new Vector2Int(7 , 11), config.Tile3_2_3,  270f);
			yield return AssertTileParams(new Vector2Int(6 , 12), config.Tile5_1_2,  270f);
			
		}

		private IEnumerator AssertTileParams(Vector2Int pos, Texture2D textureAssertion, float angleAssertion = 0f,
			int flipAssertion = 0)
		{
			_targetTile = _gridMap.GetTile(pos.x, pos.y);
			yield return new WaitForSeconds(0.01f);
			Assert.AreEqual(textureAssertion, Renderer.CurrentTexture, $"{pos}");
			Assert.AreEqual(angleAssertion, Renderer.TextureAngle, $"{pos}");
			Assert.AreEqual(flipAssertion, Renderer.Flip, $"{pos}");
		}
	}
}