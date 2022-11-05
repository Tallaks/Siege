using System;
using System.Collections.Generic;
using System.Linq;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Movement;
using UnityEngine;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Prototype
{
	public class GridMap : IGridMap
	{
		public static int[,] GridArray;

		private readonly TilemapFactory _tilemapFactory;
		private readonly List<CustomTile> _tiles = new();

		public IEnumerable<CustomTile> AllTiles => _tiles;
		public Action<CustomTile> OnTileSelection { get; set; }

		public GridMap(TilemapFactory tilemapFactory) =>
			_tilemapFactory = tilemapFactory;

		public void GenerateMap()
		{
			int upperBound0 = GridArray.GetUpperBound(0);
			int upperBound1 = GridArray.GetUpperBound(1);

			for (var i = 0; i <= upperBound0; i++)
			for (var j = 0; j <= upperBound1; j++)
			{
				if (GridArray[i, j] == 1)
				{
					var cellPosition = new Vector2Int(j, upperBound0 - i);
					_tiles.Add(_tilemapFactory.Create(cellPosition));
				}
			}
		}

		public CustomTile GetTile(int x, int y) =>
			_tiles.FirstOrDefault(k => k.CellPosition == new Vector2Int(x, y));
	}
}