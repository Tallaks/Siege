using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Data;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Maps;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Placer
{
  public interface ICharacterPlacer
  {
    void PlaceNewCharacterOnTile(Tile tileToPlace, CharacterConfig selectedPlayerConfig);
  }
}