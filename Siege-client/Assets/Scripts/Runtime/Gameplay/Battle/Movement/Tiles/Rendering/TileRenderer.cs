using UnityEngine;
using Zenject;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Movement.Tiles.Rendering
{
	[RequireComponent(typeof(CustomTile), typeof(Renderer))]
	public class TileRenderer : MonoBehaviour
	{
		public static readonly int TileTex = Shader.PropertyToID("_TileTex");
		public static readonly int AngleProperty = Shader.PropertyToID("_Angle");

#if UNITY_INCLUDE_TESTS
		public Texture CurrentTexture => _material.GetTexture(TileTex);
		public float TextureAngle => _material.GetFloat(AngleProperty);
#endif
		private CustomTile Tile => GetComponent<CustomTile>();
		
		private Material _material;
		private ITilesRenderingAggregator _renderingAggregator;

		[Inject]
		private void Construct(ITilesRenderingAggregator renderingAggregator) => 
			_renderingAggregator = renderingAggregator;

		private void Awake() => 
			_material = GetComponent<Renderer>().material;

		private void Start()
		{
			int count = Tile.NeighboursWithDistances.Count;
			_renderingAggregator.ChangeMaterial(Tile, _material, count);
		}
	}
}