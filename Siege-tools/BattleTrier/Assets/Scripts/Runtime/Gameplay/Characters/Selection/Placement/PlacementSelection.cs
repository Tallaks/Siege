using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Data;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Selection.Placement
{
  public class PlacementSelection : IPlacementSelection
  {
    public CharacterConfig SelectedPlayerConfig { get; private set; }
    public Character SelectedCharacter { get; private set; }

    public void SelectConfig(CharacterConfig configById) =>
      SelectedPlayerConfig = configById;

    public void Unselect() =>
      SelectedPlayerConfig = null;

    public void SelectPlacedCharacter(Character characterOnTile) =>
      SelectedCharacter = characterOnTile;
  }
}