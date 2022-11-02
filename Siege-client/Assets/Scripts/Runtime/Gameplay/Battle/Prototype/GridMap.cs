using System.Collections.Generic;
using System.Linq;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Movement;
using UnityEngine;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Prototype
{
	public class GridMap : IGridMap
	{
		private readonly TilemapFactory _tilemapFactory;

		private List<CustomTile> _tiles = new();
		
		public GridMap(TilemapFactory tilemapFactory) => 
			_tilemapFactory = tilemapFactory;

		public void GenerateMap(int[,] grid)
		{
			int upperBound0 = grid.GetUpperBound(0);
			int upperBound1 = grid.GetUpperBound(1);

			for (var i = 0; i <= upperBound0; i++)
			for (var j = 0; j <= upperBound1; j++)
			{
				if (grid[i, j] == 1)
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