using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Data;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Selection.Placement
{
  public interface IPlacementSelection
  {
    CharacterConfig SelectedPlayerConfig { get; }
    Character SelectedCharacter { get; }
    void SelectConfig(CharacterConfig configById);
    void UnselectConfig();
    void SelectCharacter(Character characterOnTile);
    void UnselectCharacter();
  }
}