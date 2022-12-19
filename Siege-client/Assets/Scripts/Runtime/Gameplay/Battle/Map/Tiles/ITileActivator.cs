using Kulinaria.Siege.Runtime.Gameplay.Battle.Characters;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Tiles
{
	public interface ITileActivator
	{
		void ActivateTilesAround(BaseCharacter character);
		void DeactivateAllTiles();
	}
}