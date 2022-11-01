using UnityEngine;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Movement
{
	public class TileMovementService : IMovementService
	{
		private readonly TilemapFactory _tilemapFactory;

		public TileMovementService(TilemapFactory tilemapFactory) => 
			_tilemapFactory = tilemapFactory;

		public void GenerateMap(int[,] grid)
		{
			int upperBound0 = grid.GetUpperBound(0);
			int upperBound1 = grid.GetUpperBound(1);

			for(var i = 0; i <= upperBound0; i++)
			for (var j = 0; j <= upperBound1; j++)
			{
				if(grid[i,j] == 1)
					_tilemapFactory.Create(new Vector2Int(j, upperBound0 - i));
			}
		}
	}
}