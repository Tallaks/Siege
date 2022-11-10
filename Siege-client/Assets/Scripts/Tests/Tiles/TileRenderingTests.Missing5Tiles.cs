using System.Collections;
using Kulinaria.Siege.Runtime.Infrastructure.Configs;
using UnityEngine;
using UnityEngine.TestTools;

namespace Kulinaria.Siege.Tests.Tiles
{
	public partial class TileRenderingTests
	{
		[UnityTest]
		public IEnumerator When3TilesGeneratedIn3x3_ThenItHas0_4_4or3_3_2or1_3_4or2_2_4or3_2_3or2_3_3()
		{
			var config = Resources.Load<TileSpritesConfig>("Configs/TileRules");
			PrepareTiles();
			
			var grid = new[,]
			{
				{ 1, 0, 1 },
				{ 0, 1, 0 },
				{ 0, 0, 1 },
			};
			
			yield return AssertTileTextureFor(grid, config.Tile0_4_4);

			grid = new[,]
			{
				{ 0, 0, 1 },
				{ 0, 1, 0 },
				{ 1, 0, 1 },
			};
			
			yield return AssertTileTextureFor(grid, config.Tile0_4_4);
			
			grid = new[,]
			{
				{ 1, 0, 0 },
				{ 0, 1, 0 },
				{ 1, 0, 1 },
			};
			
			yield return AssertTileTextureFor(grid, config.Tile0_4_4);
			
			grid = new[,]
			{
				{ 1, 0, 1 },
				{ 0, 1, 0 },
				{ 1, 0, 0 },
			};
			
			yield return AssertTileTextureFor(grid, config.Tile0_4_4);
			
			grid = new[,]
			{
				{ 1, 1, 1 },
				{ 0, 1, 0 },
				{ 0, 0, 0 },
			};
			
			yield return AssertTileTextureAndAngleFor(grid, 180f, config.Tile1_3_4);
			
			grid = new[,]
			{
				{ 1, 1, 0 },
				{ 0, 1, 0 },
				{ 0, 0, 1 },
			};
			
			yield return AssertTileTextureAndAngleFor(grid, 180f, config.Tile1_3_4);
			
			grid = new[,]
			{
				{ 1, 1, 0 },
				{ 0, 1, 0 },
				{ 1, 0, 0 },
			};
			
			yield return AssertTileTextureAndAngleFor(grid, 180f, config.Tile1_3_4);
			
			grid = new[,]
			{
				{ 0, 1, 1 },
				{ 0, 1, 0 },
				{ 0, 0, 1 },
			};
			
			yield return AssertTileTextureAndAngleFor(grid, 180f, config.Tile1_3_4);
			
			grid = new[,]
			{
				{ 0, 1, 1 },
				{ 0, 1, 0 },
				{ 1, 0, 0 },
			};
			
			yield return AssertTileTextureAndAngleFor(grid, 180f, config.Tile1_3_4);
			
			grid = new[,]
			{
				{ 0, 1, 0 },
				{ 0, 1, 0 },
				{ 1, 0, 1 },
			};
			
			yield return AssertTileTextureAndAngleFor(grid, 180f, config.Tile1_3_4);
			
			grid = new[,]
			{
				{ 1, 0, 1 },
				{ 0, 1, 1 },
				{ 0, 0, 0 },
			};
			
			yield return AssertTileTextureAndAngleFor(grid, 90f, config.Tile1_3_4);
			
			grid = new[,]
			{
				{ 1, 0, 0 },
				{ 0, 1, 1 },
				{ 0, 0, 1 },
			};
			
			yield return AssertTileTextureAndAngleFor(grid, 90f, config.Tile1_3_4);
			
			grid = new[,]
			{
				{ 1, 0, 0 },
				{ 0, 1, 1 },
				{ 1, 0, 0 },
			};
			
			yield return AssertTileTextureAndAngleFor(grid, 90f, config.Tile1_3_4);
			
			grid = new[,]
			{
				{ 0, 0, 1 },
				{ 0, 1, 1 },
				{ 0, 0, 1 },
			};
			
			yield return AssertTileTextureAndAngleFor(grid, 90f, config.Tile1_3_4);
			
			grid = new[,]
			{
				{ 0, 0, 1 },
				{ 0, 1, 1 },
				{ 1, 0, 0 },
			};
			
			yield return AssertTileTextureAndAngleFor(grid, 90f, config.Tile1_3_4);
			
			grid = new[,]
			{
				{ 0, 0, 0 },
				{ 0, 1, 1 },
				{ 1, 0, 1 },
			};
			
			yield return AssertTileTextureAndAngleFor(grid, 90f, config.Tile1_3_4);

			grid = new[,]
			{
				{ 1, 0, 1 },
				{ 0, 1, 0 },
				{ 0, 1, 0 },
			};
			
			yield return AssertTileTextureAndAngleFor(grid, 0, config.Tile1_3_4);
			
			grid = new[,]
			{
				{ 1, 0, 0 },
				{ 0, 1, 0 },
				{ 0, 1, 1 },
			};
			
			yield return AssertTileTextureAndAngleFor(grid, 0, config.Tile1_3_4);
			
			grid = new[,]
			{
				{ 1, 0, 0 },
				{ 0, 1, 0 },
				{ 1, 1, 0 },
			};
			
			yield return AssertTileTextureAndAngleFor(grid, 0, config.Tile1_3_4);
			
			grid = new[,]
			{
				{ 0, 0, 1 },
				{ 0, 1, 0 },
				{ 0, 1, 1 },
			};
			
			yield return AssertTileTextureAndAngleFor(grid, 0, config.Tile1_3_4);
			
			grid = new[,]
			{
				{ 0, 0, 1 },
				{ 0, 1, 0 },
				{ 1, 1, 0 },
			};
			
			yield return AssertTileTextureAndAngleFor(grid, 0, config.Tile1_3_4);
			
			grid = new[,]
			{
				{ 0, 0, 0 },
				{ 0, 1, 0 },
				{ 1, 1, 1 },
			};
			
			yield return AssertTileTextureAndAngleFor(grid, 0, config.Tile1_3_4);

			grid = new[,]
			{
				{ 1, 0, 1 },
				{ 1, 1, 0 },
				{ 0, 0, 0 },
			};
			
			yield return AssertTileTextureAndAngleFor(grid, 270f, config.Tile1_3_4);
			
			grid = new[,]
			{
				{ 1, 0, 0 },
				{ 1, 1, 0 },
				{ 0, 0, 1 },
			};
			
			yield return AssertTileTextureAndAngleFor(grid, 270f, config.Tile1_3_4);
			
			grid = new[,]
			{
				{ 1, 0, 0 },
				{ 1, 1, 0 },
				{ 1, 0, 0 },
			};
			
			yield return AssertTileTextureAndAngleFor(grid, 270f, config.Tile1_3_4);
			
			grid = new[,]
			{
				{ 0, 0, 1 },
				{ 1, 1, 0 },
				{ 0, 0, 1 },
			};
			
			yield return AssertTileTextureAndAngleFor(grid, 270f, config.Tile1_3_4);
			
			grid = new[,]
			{
				{ 0, 0, 1 },
				{ 1, 1, 0 },
				{ 1, 0, 0 },
			};
			
			yield return AssertTileTextureAndAngleFor(grid, 270f, config.Tile1_3_4);
			
			grid = new[,]
			{
				{ 0, 0, 0 },
				{ 1, 1, 0 },
				{ 1, 0, 1 },
			};
			
			yield return AssertTileTextureAndAngleFor(grid, 270f, config.Tile1_3_4);
			
			/***********************************/
			
			grid = new[,]
			{
				{ 0, 1, 0 },
				{ 1, 1, 0 },
				{ 0, 1, 0 },
			};
			
			yield return AssertTileTextureAndAngleFor(grid, 180f, config.Tile3_3_2);
			
			grid = new[,]
			{
				{ 0, 1, 0 },
				{ 1, 1, 1 },
				{ 0, 0, 0 },
			};
			
			yield return AssertTileTextureAndAngleFor(grid, 90f, config.Tile3_3_2);
			
			grid = new[,]
			{
				{ 0, 1, 0 },
				{ 0, 1, 1 },
				{ 0, 1, 0 },
			};
			
			yield return AssertTileTextureAndAngleFor(grid, 0, config.Tile3_3_2);
			
			grid = new[,]
			{
				{ 0, 0, 0 },
				{ 1, 1, 1 },
				{ 0, 1, 0 },
			};
			
			yield return AssertTileTextureAndAngleFor(grid, 270f, config.Tile3_3_2);
			
			/***********************************/
			
			grid = new[,]
			{
				{ 0, 1, 0 },
				{ 1, 1, 0 },
				{ 0, 1, 0 },
			};
			
			yield return AssertTileTextureAndAngleFor(grid, 180f, config.Tile3_3_2);
			
			grid = new[,]
			{
				{ 0, 1, 0 },
				{ 1, 1, 1 },
				{ 0, 0, 0 },
			};
			
			yield return AssertTileTextureAndAngleFor(grid, 90f, config.Tile3_3_2);
			
			grid = new[,]
			{
				{ 0, 1, 0 },
				{ 0, 1, 1 },
				{ 0, 1, 0 },
			};
			
			yield return AssertTileTextureAndAngleFor(grid, 0, config.Tile3_3_2);
			
			grid = new[,]
			{
				{ 0, 0, 0 },
				{ 1, 1, 1 },
				{ 0, 1, 0 },
			};
			
			yield return AssertTileTextureAndAngleFor(grid, 270f, config.Tile3_3_2);
			
			/***********************************/
			
			grid = new[,]
			{
				{ 1, 1, 0 },
				{ 1, 1, 0 },
				{ 0, 0, 0 },
			};
			
			yield return AssertTileTextureAndAngleFor(grid, 270f, config.Tile3_2_3);
			
			grid = new[,]
			{
				{ 0, 1, 1 },
				{ 0, 1, 1 },
				{ 0, 0, 0 },
			};
			
			yield return AssertTileTextureAndAngleFor(grid, 180f, config.Tile3_2_3);
			
			grid = new[,]
			{
				{ 0, 0, 0 },
				{ 0, 1, 1 },
				{ 0, 1, 1 },
			};
			
			yield return AssertTileTextureAndAngleFor(grid, 90f, config.Tile3_2_3);
			
			grid = new[,]
			{
				{ 0, 0, 0 },
				{ 1, 1, 0 },
				{ 1, 1, 0 },
			};
			
			yield return AssertTileTextureAndAngleFor(grid, 0, config.Tile3_2_3);
			
			/***********************************/
			
			grid = new[,]
			{
				{ 0, 1, 1 },
				{ 1, 1, 0 },
				{ 0, 0, 0 },
			};
			
			yield return AssertTileTextureAndAngleFor(grid, 270f, config.Tile2_3_3);
			
			grid = new[,]
			{
				{ 0, 1, 0 },
				{ 1, 1, 0 },
				{ 0, 0, 1 },
			};
			
			yield return AssertTileTextureAndAngleFor(grid, 270f, config.Tile2_3_3);
			
			grid = new[,]
			{
				{ 0, 1, 0 },
				{ 1, 1, 0 },
				{ 1, 0, 0 },
			};
			
			yield return AssertTileTextureAndAngleFor(grid, 270f, config.Tile2_3_3);
			
			grid = new[,]
			{
				{ 0, 1, 0 },
				{ 0, 1, 1 },
				{ 0, 0, 1 },
			};
			
			yield return AssertTileTextureAndAngleFor(grid, 180f, config.Tile2_3_3);
			
			grid = new[,]
			{
				{ 0, 1, 0 },
				{ 0, 1, 1 },
				{ 1, 0, 0 },
			};
			
			yield return AssertTileTextureAndAngleFor(grid, 180f, config.Tile2_3_3);
			
			grid = new[,]
			{
				{ 1, 1, 0 },
				{ 0, 1, 1 },
				{ 0, 0, 0 },
			};
			
			yield return AssertTileTextureAndAngleFor(grid, 180f, config.Tile2_3_3);
			
			grid = new[,]
			{
				{ 0, 0, 0 },
				{ 0, 1, 1 },
				{ 1, 1, 0 },
			};
			
			yield return AssertTileTextureAndAngleFor(grid, 90f, config.Tile2_3_3);
			
			grid = new[,]
			{
				{ 1, 0, 0 },
				{ 0, 1, 1 },
				{ 0, 1, 0 },
			};
			
			yield return AssertTileTextureAndAngleFor(grid, 90f, config.Tile2_3_3);
			
			grid = new[,]
			{
				{ 0, 0, 1 },
				{ 0, 1, 1 },
				{ 0, 1, 0 },
			};
			
			yield return AssertTileTextureAndAngleFor(grid, 90f, config.Tile2_3_3);
			
			grid = new[,]
			{
				{ 1, 0, 0 },
				{ 1, 1, 0 },
				{ 0, 1, 0 },
			};
			
			yield return AssertTileTextureAndAngleFor(grid, 0, config.Tile2_3_3);
			
			grid = new[,]
			{
				{ 0, 0, 1 },
				{ 1, 1, 0 },
				{ 0, 1, 0 },
			};
			
			yield return AssertTileTextureAndAngleFor(grid, 0, config.Tile2_3_3);
			
			grid = new[,]
			{
				{ 0, 0, 0 },
				{ 1, 1, 0 },
				{ 0, 1, 1 },
			};
			
			yield return AssertTileTextureAndAngleFor(grid, 0, config.Tile2_3_3);
			
			/***********************************/
			
			grid = new[,]
			{
				{ 0, 1, 1 },
				{ 0, 1, 0 },
				{ 0, 1, 0 },
			};
			
			yield return AssertTileTextureAndAngleFor(grid, 0, config.Tile2_2_4);
			
			grid = new[,]
			{
				{ 0, 1, 0 },
				{ 0, 1, 0 },
				{ 0, 1, 1 },
			};
			
			yield return AssertTileTextureAndAngleFor(grid, 0, config.Tile2_2_4);
			
			grid = new[,]
			{
				{ 1, 1, 0 },
				{ 0, 1, 0 },
				{ 0, 1, 0 },
			};
			
			yield return AssertTileTextureAndAngleFor(grid, 0, config.Tile2_2_4);
			
			grid = new[,]
			{
				{ 0, 1, 0 },
				{ 0, 1, 0 },
				{ 1, 1, 0 },
			};
			
			yield return AssertTileTextureAndAngleFor(grid, 0, config.Tile2_2_4);
			
			grid = new[,]
			{
				{ 0, 0, 1 },
				{ 1, 1, 1 },
				{ 0, 0, 0 },
			};
			
			yield return AssertTileTextureAndAngleFor(grid, 90f, config.Tile2_2_4);
			
			grid = new[,]
			{
				{ 0, 0, 0 },
				{ 1, 1, 1 },
				{ 0, 0, 1 },
			};
			
			yield return AssertTileTextureAndAngleFor(grid, 90f, config.Tile2_2_4);
			
			grid = new[,]
			{
				{ 1, 0, 0 },
				{ 1, 1, 1 },
				{ 0, 0, 0 },
			};
			
			yield return AssertTileTextureAndAngleFor(grid, 90f, config.Tile2_2_4);
			
			grid = new[,]
			{
				{ 0, 0, 0 },
				{ 1, 1, 1 },
				{ 1, 0, 0 },
			};
			
			yield return AssertTileTextureAndAngleFor(grid, 90f, config.Tile2_2_4);
		}
	}
}