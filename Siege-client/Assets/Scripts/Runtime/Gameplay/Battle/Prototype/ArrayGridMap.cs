using System;
using System.Collections.Generic;
using System.Linq;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Level;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Level.Tiles;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Prototype
{
	public class ArrayGridMap : IGridMap
	{
		public static int[,] GridArray;

		private readonly TilemapFactory _tilemapFactory;
		private List<CustomTile> _tiles = new();

		public ArrayGridMap(TilemapFactory tilemapFactory) => 
			_tilemapFactory = tilemapFactory;

		public IEnumerable<CustomTile> AllTiles => _tiles;
		public Action<CustomTile> OnTileSelection { get; set; }

		public void GenerateMap()
		{
			int upperBound0 = GridArray.GetUpperBound(0);
			int upperBound1 = GridArray.GetUpperBound(1);

			for (var i = 0; i <= upperBound0; i++)
			for (var j = 0; j <= upperBound1; j++)
				if (GridArray[i, j] == 1)
				{
					var cellPosition = new Vector2Int(j, upperBound0 - i);
					_tiles.Add(_tilemapFactory.Create(cellPosition));
				}
		}

		public void Clear()
		{
			foreach (CustomTile tile in _tiles)
				Object.Destroy(tile.gameObject);

			_tiles = new List<CustomTile>();
		}

		public CustomTile GetTile(int x, int y)
		{
			return _tiles.FirstOrDefault(k => k.CellPosition == new Vector2Int(x, y));
		}
	}
}