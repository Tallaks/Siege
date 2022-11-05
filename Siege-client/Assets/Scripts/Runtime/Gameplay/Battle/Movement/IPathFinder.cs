namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Movement
{
	public interface IPathFinder
	{
		int Distance(CustomTile tileA, CustomTile tileB);
	}
}