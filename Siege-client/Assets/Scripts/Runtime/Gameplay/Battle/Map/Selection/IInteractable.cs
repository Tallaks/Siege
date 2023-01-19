using Kulinaria.Siege.Runtime.Gameplay.Battle.Characters;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Tiles;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Selection
{
	public interface IInteractable
	{
		CustomTile Tile { get; }
		BaseCharacter Visitor { get; }
		void Interact();
	}
}