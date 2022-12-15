using Kulinaria.Siege.Runtime.Extensions;
using UnityEngine;
using Zenject;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Tiles
{
	public class TilemapFactory : PlaceholderFactory<CustomTile>
	{
		private const string TilePrefabPath = "Prefabs/Battle/Map/Tile";

		private readonly DiContainer _container;
		private Transform _parent;

		public TilemapFactory(DiContainer container) => 
			_container = container;

		public void Initialize(Transform parent) => 
			_parent = parent;

		public CustomTile Create(Vector2Int cellPosition)
		{
			var tile = _container.InstantiatePrefabResourceForComponent<CustomTile>(
				TilePrefabPath,
				cellPosition.ToWorld(),
				Quaternion.identity,
				null);
			tile.transform.SetParent(_parent, false);

			tile.Initialize(cellPosition);
			return tile;
		}
	}
}