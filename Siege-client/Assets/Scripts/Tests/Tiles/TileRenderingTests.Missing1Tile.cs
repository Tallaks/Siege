using System.Collections;
using Kulinaria.Siege.Runtime.Infrastructure.Configs;
using UnityEngine;
using UnityEngine.TestTools;

namespace Kulinaria.Siege.Tests.Tiles
{
	public partial class TileRenderingTests
	{
		[UnityTest]
		public IEnumerator WhenOneNeighbourTileNotGeneratedIn3x3_ThenItHas7_1_0or5_1_2()
		{
			var config = Resources.Load<TileSpritesConfig>("Configs/TileRules");
			PrepareTiles();

			var grid = new[,]
			{
				{ 1, 1, 0 },
				{ 1, 1, 1 },
				{ 1, 1, 1 },
			};

			yield return AssertTileTextureAndAngleFor(grid, 270f, config.Tile7_1_0);

			grid = new[,]
			{
				{ 0, 1, 1 },
				{ 1, 1, 1 },
				{ 1, 1, 1 },
			};

			yield return AssertTileTextureAndAngleFor(grid, 0f, config.Tile7_1_0);

			grid = new[,]
			{
				{ 1, 1, 1 },
				{ 1, 1, 1 },
				{ 0, 1, 1 },
			};

			yield return AssertTileTextureAndAngleFor(grid, 90f, config.Tile7_1_0);
		}
	}
}