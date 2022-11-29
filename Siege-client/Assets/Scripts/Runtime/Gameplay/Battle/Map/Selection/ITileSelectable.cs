using Kulinaria.Siege.Runtime.Gameplay.Battle.Characters;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Tiles;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Selection
{
	public interface ITileSelectable
	{
		CustomTile Tile { get; }
		BaseCharacter Visitor { get; }
	}
}