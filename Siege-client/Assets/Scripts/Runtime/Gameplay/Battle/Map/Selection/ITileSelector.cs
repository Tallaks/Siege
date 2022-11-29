using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Tiles;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Selection
{
	public interface ITileSelector
	{
		void Select(CustomTile tile, int distancePoints);
	}
}