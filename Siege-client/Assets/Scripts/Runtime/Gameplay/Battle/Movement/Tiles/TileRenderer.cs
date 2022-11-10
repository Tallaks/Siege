using Kulinaria.Siege.Runtime.Infrastructure.Configs;
using UnityEngine;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Movement.Tiles
{
	[RequireComponent(typeof(CustomTile), typeof(Renderer))]
	public class TileRenderer : MonoBehaviour
	{
		private static readonly int TileTex = Shader.PropertyToID("_TileTex");

#if UNITY_INCLUDE_TESTS
		public TileSpritesConfig ConfigForTest => _config;
		public Texture CurrentTexture => _material.GetTexture(TileTex);
#endif
		private CustomTile Tile => GetComponent<CustomTile>();
		
		private TileSpritesConfig _config;
		private Material _material;

		private void Awake()
		{
			_config = Resources.Load<TileSpritesConfig>("Configs/TileRules");
			_material = GetComponent<Renderer>().material;
		}

		private void Start()
		{
			if (Tile.NeighboursWithDistances.Count == 0)
				_material.SetTexture(TileTex, _config.Tile0_4_4);
		}
	}
}