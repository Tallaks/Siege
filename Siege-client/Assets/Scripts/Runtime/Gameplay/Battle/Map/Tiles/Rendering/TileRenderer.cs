using System.Linq;
using UnityEngine;
using Zenject;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Tiles.Rendering
{
	[RequireComponent(typeof(CustomTile), typeof(Renderer))]
	public class TileRenderer : MonoBehaviour
	{
		public static readonly int TileTex = Shader.PropertyToID("_TileTex");
		public static readonly int FlipProperty = Shader.PropertyToID("_Flip");
		public static readonly int AngleProperty = Shader.PropertyToID("_Angle");
		private CustomTile Tile => GetComponent<CustomTile>();

		private Material _material;
		private ITilesRenderingAggregator _renderingAggregator;

		[Inject]
		private void Construct(ITilesRenderingAggregator renderingAggregator)
		{
			_renderingAggregator = renderingAggregator;
		}

		private void Awake()
		{
			_material = GetComponent<Renderer>().material;
		}

		public void Repaint()
		{
			int count = Tile.ActiveNeighbours.Count();
			_material.SetInt(FlipProperty, 0);
			_renderingAggregator.ChangeMaterial(Tile, _material, count);
		}

#if UNITY_INCLUDE_TESTS
		public Texture CurrentTexture => _material.GetTexture(TileTex);
		public float TextureAngle => _material.GetFloat(AngleProperty);
		public int Flip => _material.GetInt(FlipProperty);
#endif
	}
}