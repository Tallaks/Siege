using Kulinaria.Siege.Runtime.Gameplay.Battle.Movement;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Prototype
{
	public interface IGridMap
	{
		void GenerateMap(int[,] grid);
		CustomTile GetTile(int x, int y);
	}
}