using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Ui.Battle;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Ui.PlacementState;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Ui.SelectionState;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Maps.Selection.UI;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Roles;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.UI
{
  public class GameplayMediator : MonoBehaviour
  {
    [SerializeField] [Required]
    private CharacterSelectionUi _characterSelectionUi;

    [SerializeField] [Required]
    private MapSelectionUi _mapSelectionUi;

    [SerializeField] [Required]
    private CharacterPlacementUi _characterPlacementUi;

    [SerializeField] [Required] [SceneObjectsOnly]
    private BattleUi _battleUi;

    public bool CharacterPlacementUiIsActive => _characterPlacementUi.IsActivePanel;
    public bool CharacterPlacementUiIsWaiting => _characterPlacementUi.IsWaitingPanel;

    public void ChangeCharacterList() =>
      _characterSelectionUi.ChangeCharacterList();

    public void ChangeCharacterSelectionUiOnFirstPlayerReady() =>
      _characterSelectionUi.ChangeCharacterSelectionUiOnFirstPlayerReady();

    public void ChangeCharacterSelectionUiOnSecondPlayerReady() =>
      _characterSelectionUi.ChangeCharacterSelectionUiOnSecondPlayerReady();

    public void DisableCharacterSelectSubmitButton() =>
      _characterSelectionUi.DisableCharacterSelectSubmitButton();

    public void EnableCharacterSelectSubmitButton() =>
      _characterSelectionUi.EnableCharacterSelectSubmitButton();

    public void ShowConfigInfo(int configId) =>
      _characterSelectionUi.ShowConfigInfo(configId);

    public void HideCharacterSelectionUi() =>
      _characterSelectionUi.HideCharacterSelectionUi();

    public void InitializeCharacterSelectionUi() =>
      _characterSelectionUi.Initialize();

    public void EnableMapSubmitButton() =>
      _mapSelectionUi.EnableSubmitButton();

    public void HideMapSelectionUi() =>
      _mapSelectionUi.HideMapSelectionUi();

    public void InitializeMapSelectionUi(RoleState stateValue) =>
      _mapSelectionUi.Initialize(stateValue);

    public void SetSelectedMap(MapSelectionButton selected) =>
      _mapSelectionUi.SetMap(selected);

    public void ShowPlacementActivePlayerUi() =>
      _characterPlacementUi.ShowPlacementActivePlayerUi();

    public void ShowPlacementWaitingPlayerUi() =>
      _characterPlacementUi.ShowPlacementWaitingPlayerUi();

    public void ShowPlacementSpectatorUi() =>
      _characterPlacementUi.ShowPlacementSpectatorUi();

    public void UpdatePlacementList() =>
      _characterPlacementUi.UpdatePlacementList();

    public void ShowSubmitButton() =>
      _characterPlacementUi.ShowSubmitButton();

    public void HidePlacementActivePlayerUi() =>
      _characterPlacementUi.HidePlacementActivePlayerUi();

    public void HidePlacementWaitingUi() =>
      _characterPlacementUi.HidePlacementWaitingUi();

    public void HideAllPlacementUi() =>
      _characterPlacementUi.HideAll();

    public void ShowActivePlayerBattleUi() =>
      _battleUi.ShowActivePlayerBattleUi();

    public void ShowSpectatorBattleUi() =>
      _battleUi.ShowSpectatorBattleUi();
  }
}