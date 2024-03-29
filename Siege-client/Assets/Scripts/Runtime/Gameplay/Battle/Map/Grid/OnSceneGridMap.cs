using System.Collections.Generic;
using System.Linq;
using Kulinaria.Siege.Runtime.Extensions;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Tiles;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Grid
{
	public class OnSceneGridMap : IGridMap
	{
		public IEnumerable<CustomTile> AllTiles
		{
			get => _tiles;
			private set => _tiles = new List<CustomTile>(value);
		}

		public IEnumerable<CustomTile> EmptyTiles =>
			_tiles.Where(k => !k.HasVisitor);

		private List<CustomTile> _tiles = new();

		public void GenerateMap()
		{
			AllTiles = Object.FindObjectsOfType<CustomTile>();
			foreach (CustomTile tile in AllTiles)
				tile.Initialize(tile.transform.localPosition.ToCell());
		}

		public void Clear()
		{
			foreach (CustomTile tile in AllTiles)
				Object.Destroy(tile.gameObject);

			AllTiles = new List<CustomTile>();
		}

		public CustomTile GetTile(int x, int y)
		{
			return _tiles.FirstOrDefault(k => k.transform.localPosition == new Vector2Int(x, y).ToWorld());
		}
	}
}