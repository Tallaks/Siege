using System.Collections;
using Kulinaria.Siege.Runtime.Infrastructure.Configs;
using UnityEngine;
using UnityEngine.TestTools;

namespace Kulinaria.Siege.Tests.Tiles
{
	public partial class TileRenderingTests
	{
		[UnityTest]
		public IEnumerator When5TilesGeneratedIn3x3_ThenItHas5_3_0or5_1_2or1_3_4or4_2_2or3_3_2or2_2_4or2_3_3()
		{
			var config = Resources.Load<TileSpritesConfig>("Configs/TileRules");
			PrepareTiles();

			var grid = new[,]
			{
				{ 0, 1, 0 },
				{ 1, 1, 1 },
				{ 0, 1, 1 },
			};

			yield return AssertTileTextureAndAngleFor(grid, 0, config.Tile5_3_0);

			grid = new[,]
			{
				{ 0, 1, 1 },
				{ 1, 1, 1 },
				{ 0, 1, 0 },
			};

			yield return AssertTileTextureAndAngleFor(grid, 90f, config.Tile5_3_0);

			grid = new[,]
			{
				{ 0, 1, 0 },
				{ 1, 1, 1 },
				{ 1, 1, 0 },
			};

			yield return AssertTileTextureAndAngleFor(grid, 270f, config.Tile5_3_0);

			/******************************/

			grid = new[,]
			{
				{ 0, 0, 0 },
				{ 1, 1, 1 },
				{ 1, 1, 1 },
			};

			yield return AssertTileTextureAndAngleFor(grid, 270f, config.Tile5_1_2);

			/******************************/

			grid = new[,]
			{
				{ 1, 0, 1 },
				{ 0, 1, 0 },
				{ 1, 1, 1 },
			};

			yield return AssertTileTextureAndAngleFor(grid, 0, config.Tile1_3_4);

			grid = new[,]
			{
				{ 1, 0, 1 },
				{ 0, 1, 1 },
				{ 1, 0, 1 },
			};

			yield return AssertTileTextureAndAngleFor(grid, 90f, config.Tile1_3_4);

			grid = new[,]
			{
				{ 1, 0, 1 },
				{ 1, 1, 0 },
				{ 1, 0, 1 },
			};

			yield return AssertTileTextureAndAngleFor(grid, 270f, config.Tile1_3_4);

			/******************************/

			grid = new[,]
			{
				{ 0, 1, 1 },
				{ 0, 1, 1 },
				{ 1, 1, 0 },
			};

			yield return AssertTileTextureAndAngleAndFlipFor(grid, 0, config.Tile4_2_2, 0);

			grid = new[,]
			{
				{ 1, 1, 1 },
				{ 0, 1, 1 },
				{ 0, 1, 0 },
			};

			yield return AssertTileTextureAndAngleAndFlipFor(grid, 0, config.Tile4_2_2, 0);

			grid = new[,]
			{
				{ 1, 1, 0 },
				{ 1, 1, 1 },
				{ 0, 0, 1 },
			};

			yield return AssertTileTextureAndAngleAndFlipFor(grid, 90, config.Tile4_2_2, 0);

			grid = new[,]
			{
				{ 1, 1, 0 },
				{ 1, 1, 1 },
				{ 1, 0, 0 },
			};

			yield return AssertTileTextureAndAngleAndFlipFor(grid, 90, config.Tile4_2_2, 0);

			grid = new[,]
			{
				{ 0, 1, 1 },
				{ 1, 1, 0 },
				{ 1, 1, 0 },
			};

			yield return AssertTileTextureAndAngleAndFlipFor(grid, 180, config.Tile4_2_2, 0);

			grid = new[,]
			{
				{ 1, 0, 0 },
				{ 1, 1, 1 },
				{ 0, 1, 1 },
			};

			yield return AssertTileTextureAndAngleAndFlipFor(grid, 270, config.Tile4_2_2, 0);

			grid = new[,]
			{
				{ 0, 1, 0 },
				{ 0, 1, 1 },
				{ 1, 1, 1 },
			};

			yield return AssertTileTextureAndAngleAndFlipFor(grid, 0, config.Tile4_2_2, 1);

			grid = new[,]
			{
				{ 0, 1, 1 },
				{ 1, 1, 1 },
				{ 0, 0, 1 },
			};

			yield return AssertTileTextureAndAngleAndFlipFor(grid, 90, config.Tile4_2_2, 1);

			grid = new[,]
			{
				{ 0, 1, 1 },
				{ 1, 1, 1 },
				{ 1, 0, 0 },
			};

			yield return AssertTileTextureAndAngleAndFlipFor(grid, 90, config.Tile4_2_2, 1);

			grid = new[,]
			{
				{ 1, 1, 1 },
				{ 1, 1, 0 },
				{ 0, 1, 0 },
			};

			yield return AssertTileTextureAndAngleAndFlipFor(grid, 180, config.Tile4_2_2, 1);

			grid = new[,]
			{
				{ 1, 1, 0 },
				{ 1, 1, 0 },
				{ 0, 1, 1 },
			};

			yield return AssertTileTextureAndAngleAndFlipFor(grid, 180, config.Tile4_2_2, 1);

			grid = new[,]
			{
				{ 0, 0, 1 },
				{ 1, 1, 1 },
				{ 1, 1, 0 },
			};

			yield return AssertTileTextureAndAngleAndFlipFor(grid, 270, config.Tile4_2_2, 1);

			/******************************/

			grid = new[,]
			{
				{ 1, 1, 0 },
				{ 0, 1, 1 },
				{ 1, 1, 0 },
			};

			yield return AssertTileTextureAndAngleFor(grid, 0, config.Tile3_3_2);

			grid = new[,]
			{
				{ 0, 1, 0 },
				{ 1, 1, 1 },
				{ 1, 0, 1 },
			};

			yield return AssertTileTextureAndAngleFor(grid, 90f, config.Tile3_3_2);

			grid = new[,]
			{
				{ 1, 0, 1 },
				{ 1, 1, 1 },
				{ 0, 1, 0 },
			};

			yield return AssertTileTextureAndAngleFor(grid, 270f, config.Tile3_3_2);

			/*************************************/

			grid = new[,]
			{
				{ 1, 1, 1 },
				{ 0, 1, 0 },
				{ 0, 1, 1 },
			};

			yield return AssertTileTextureAndAngleFor(grid, 0f, config.Tile2_2_4);

			grid = new[,]
			{
				{ 1, 1, 1 },
				{ 0, 1, 0 },
				{ 1, 1, 0 },
			};

			yield return AssertTileTextureAndAngleFor(grid, 0f, config.Tile2_2_4);

			grid = new[,]
			{
				{ 1, 1, 0 },
				{ 0, 1, 0 },
				{ 1, 1, 1 },
			};

			yield return AssertTileTextureAndAngleFor(grid, 0f, config.Tile2_2_4);

			grid = new[,]
			{
				{ 0, 1, 1 },
				{ 0, 1, 0 },
				{ 1, 1, 1 },
			};

			yield return AssertTileTextureAndAngleFor(grid, 0f, config.Tile2_2_4);

			/*****************************/

			grid = new[,]
			{
				{ 1, 0, 1 },
				{ 1, 1, 0 },
				{ 0, 1, 1 },
			};

			yield return AssertTileTextureAndAngleFor(grid, 0f, config.Tile2_3_3);

			grid = new[,]
			{
				{ 1, 0, 1 },
				{ 0, 1, 1 },
				{ 1, 1, 0 },
			};

			yield return AssertTileTextureAndAngleFor(grid, 90f, config.Tile2_3_3);

			grid = new[,]
			{
				{ 1, 1, 0 },
				{ 0, 1, 1 },
				{ 1, 0, 1 },
			};

			yield return AssertTileTextureAndAngleFor(grid, 180f, config.Tile2_3_3);

			grid = new[,]
			{
				{ 0, 1, 1 },
				{ 1, 1, 0 },
				{ 1, 0, 1 },
			};

			yield return AssertTileTextureAndAngleFor(grid, 270f, config.Tile2_3_3);

			/*****************************/

			grid = new[,]
			{
				{ 0, 1, 1 },
				{ 0, 1, 1 },
				{ 1, 0, 1 },
			};

			yield return AssertTileTextureAndAngleFor(grid, 180f, config.Tile3_2_3);

			grid = new[,]
			{
				{ 1, 1, 1 },
				{ 0, 1, 1 },
				{ 1, 0, 0 },
			};

			yield return AssertTileTextureAndAngleFor(grid, 180f, config.Tile3_2_3);

			grid = new[,]
			{
				{ 0, 0, 1 },
				{ 0, 1, 1 },
				{ 1, 1, 1 },
			};

			yield return AssertTileTextureAndAngleFor(grid, 90f, config.Tile3_2_3);

			grid = new[,]
			{
				{ 1, 0, 0 },
				{ 0, 1, 1 },
				{ 1, 1, 1 },
			};

			yield return AssertTileTextureAndAngleFor(grid, 90f, config.Tile3_2_3);

			grid = new[,]
			{
				{ 1, 0, 1 },
				{ 0, 1, 1 },
				{ 0, 1, 1 },
			};

			yield return AssertTileTextureAndAngleFor(grid, 90f, config.Tile3_2_3);

			grid = new[,]
			{
				{ 0, 0, 1 },
				{ 1, 1, 0 },
				{ 1, 1, 1 },
			};

			yield return AssertTileTextureAndAngleFor(grid, 0f, config.Tile3_2_3);

			grid = new[,]
			{
				{ 1, 0, 1 },
				{ 1, 1, 0 },
				{ 1, 1, 0 },
			};

			yield return AssertTileTextureAndAngleFor(grid, 0f, config.Tile3_2_3);

			grid = new[,]
			{
				{ 1, 1, 1 },
				{ 1, 1, 0 },
				{ 1, 0, 0 },
			};

			yield return AssertTileTextureAndAngleFor(grid, 270f, config.Tile3_2_3);

			grid = new[,]
			{
				{ 1, 1, 0 },
				{ 1, 1, 0 },
				{ 1, 0, 1 },
			};

			yield return AssertTileTextureAndAngleFor(grid, 270f, config.Tile3_2_3);

			grid = new[,]
			{
				{ 1, 1, 1 },
				{ 1, 1, 0 },
				{ 0, 0, 1 },
			};

			yield return AssertTileTextureAndAngleFor(grid, 270f, config.Tile3_2_3);
		}
	}
}