using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Data;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Selection.Placement
{
  public interface IPlacementSelection
  {
    CharacterConfig SelectedPlayer { get; }
    void Select(CharacterConfig configById);
    void Unselect();
  }
}