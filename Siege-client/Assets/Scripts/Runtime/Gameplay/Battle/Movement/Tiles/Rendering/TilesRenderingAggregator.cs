using System.Collections.Generic;
using Kulinaria.Siege.Runtime.Infrastructure.Configs;
using UnityEngine;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Movement.Tiles.Rendering
{
	public class TilesRenderingAggregator : ITilesRenderingAggregator
	{
#if UNITY_INCLUDE_TESTS
		public static TileSpritesConfig ConfigForTests;
#endif
		
		private TileSpritesConfig _config =
			Resources.Load<TileSpritesConfig>("Configs/TileRules");

		private readonly Dictionary<int, IMaterialPropertyChanger> _propertyChangers;

		public TilesRenderingAggregator()
		{
			_propertyChangers = new()
			{
				[0] = new NoNeighboursPropertyChanger(_config),
				[1] = new OneNeighbourPropertyChanger(_config),
				[2] = new TwoNeighbourPropertyChanger(_config),
				[7] = new SevenNeighboursPropertyChanger(_config),
				[8] = new EightNeighboursPropertyChanger(_config)
			};
			
#if UNITY_INCLUDE_TESTS
		ConfigForTests = _config;
#endif
		}


		public void ChangeMaterial(CustomTile tile, Material material, int neighboursNumber) => 
			_propertyChangers[neighboursNumber].ChangeMaterial(tile, material);
	}
}