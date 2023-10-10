using System.Collections;
using Kulinaria.Siege.Runtime.Infrastructure.Configs;
using UnityEngine;
using UnityEngine.TestTools;

namespace Kulinaria.Siege.Tests.Tiles
{
	public partial class TileRenderingTests
	{
		[UnityTest]
		public IEnumerator When1TileGeneratedIn3x3_ThenItHas0_4_4or1_3_4()
		{
			var config = Resources.Load<TileSpritesConfig>("Configs/TileRules");
			PrepareTiles();

			var grid = new[,]
			{
				{ 0, 1, 0 },
				{ 0, 1, 0 },
				{ 0, 0, 0 }
			};

			yield return AssertTileTextureAndAngleFor(grid, 180f, config.Tile1_3_4);

			grid = new[,]
			{
				{ 0, 0, 0 },
				{ 0, 1, 0 },
				{ 0, 1, 0 }
			};

			yield return AssertTileTextureAndAngleFor(grid, 0f, config.Tile1_3_4);

			grid = new[,]
			{
				{ 1, 0, 0 },
				{ 0, 1, 0 },
				{ 0, 0, 0 }
			};

			yield return AssertTileTextureFor(grid, config.Tile0_4_4);

			grid = new[,]
			{
				{ 0, 0, 1 },
				{ 0, 1, 0 },
				{ 0, 0, 0 }
			};

			yield return AssertTileTextureFor(grid, config.Tile0_4_4);

			grid = new[,]
			{
				{ 0, 0, 0 },
				{ 0, 1, 0 },
				{ 0, 0, 1 }
			};

			yield return AssertTileTextureFor(grid, config.Tile0_4_4);

			grid = new[,]
			{
				{ 0, 0, 0 },
				{ 0, 1, 0 },
				{ 1, 0, 0 }
			};

			yield return AssertTileTextureFor(grid, config.Tile0_4_4);
		}
	}
}