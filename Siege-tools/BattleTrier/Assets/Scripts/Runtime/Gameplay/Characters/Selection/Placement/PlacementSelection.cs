using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Data;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Selection.Placement
{
  public class PlacementSelection : IPlacementSelection
  {
    public CharacterConfig SelectedPlayer { get; private set; }

    public void Select(CharacterConfig configById) =>
      SelectedPlayer = configById;

    public void Unselect() =>
      SelectedPlayer = null;
  }
}