using Kulinaria.Siege.Runtime.Extensions;
using UnityEngine;
using Zenject;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Movement
{
	public class TilemapFactory : PlaceholderFactory<CustomTile>
	{
		private const string TilePrefabPath = "Prefabs/Battle/Tile";

		private readonly DiContainer _container;
		private readonly Transform _parent;

		public TilemapFactory(DiContainer container)
		{
			_container = container;
			_parent = new GameObject("Tilemap").transform;
		}

		public CustomTile Create(Vector2Int cellPosition)
		{
			var tile = _container.InstantiatePrefabResourceForComponent<CustomTile>(
				TilePrefabPath,
				cellPosition.ToWorld(),
				Quaternion.identity,
				_parent);

			tile.Initialize(cellPosition);
			return tile;
		}
	}
}