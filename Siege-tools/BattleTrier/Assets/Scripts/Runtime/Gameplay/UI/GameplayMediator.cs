using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Ui;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Maps.Selection.UI;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Roles;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.UI
{
  public class GameplayMediator : MonoBehaviour
  {
    [SerializeField, Required] private CharacterSelectionUi _characterSelectionUi;
    [SerializeField, Required] private MapSelectionUi _mapSelectionUi;
    [SerializeField, Required] private CharacterPlacementUi _characterPlacementUi;

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

    public void EnableMapSubmitButton() =>
      _mapSelectionUi.EnableSubmitButton();

    public void HideCharacterSelectionUi() =>
      _characterSelectionUi.HideCharacterSelectionUi();

    public void HideMapSelectionUi() =>
      _mapSelectionUi.HideMapSelectionUi();

    public void InitializeCharacterSelectionUi(RoleState stateValue) =>
      _characterSelectionUi.Initialize(stateValue);

    public void InitializeMapSelectionUi(RoleState stateValue) =>
      _mapSelectionUi.Initialize(stateValue);

    public void SetSelectedMap(MapSelectionButton selected) =>
      _mapSelectionUi.SetMap(selected);

    public void ShowConfigInfo(int configId) =>
      _characterSelectionUi.ShowConfigInfo(configId);

    public void ShowPlacementActivePlayerUi() =>
      _characterPlacementUi.ShowPlacementActivePlayerUi();

    public void ShowPlacementWaitingPlayerUi() =>
      _characterPlacementUi.ShowPlacementWaitingPlayerUi();

    public void ShowPlacementSpectatorUi() =>
      _characterPlacementUi.ShowPlacementSpectatorUi();
  }
}