using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Data;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Ui;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Maps.Selection.UI;
using Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Services.Applications;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Roles;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.UI
{
  public class GameplayMediator : MonoBehaviour
  {
    [SerializeField] private MapSelectionUi _mapSelectionUi;
    [SerializeField] private Button _quitButton;
    [SerializeField] private CharacterSelectionUi _characterSelectionUi;

    [Inject] private IApplicationService _applicationService;

    private void Awake() =>
      _quitButton.onClick.AddListener(_applicationService.QuitApplication);

    public void InitializeMapSelectionUi(RoleState stateValue) =>
      _mapSelectionUi.Initialize(stateValue);

    public void InitializeCharacterSelectionUi(RoleState stateValue) =>
      _characterSelectionUi.Initialize(stateValue);

    public void HideMapSelectionUi() =>
      _mapSelectionUi.HideMapSelectionUi();

    public void EnableMapSubmitButton() =>
      _mapSelectionUi.EnableSubmitButton();

    public void SetSelectedMap(MapSelectionButton selected) =>
      _mapSelectionUi.SetMap(selected);

    public void ShowConfigInfo(CharacterConfig config) =>
      _characterSelectionUi.ShowConfigInfo(config);

    public void ChangeCharacterList() =>
      _characterSelectionUi.ChangeCharacterList();

    public void HideCharacterSelectionUi() =>
      _characterSelectionUi.HideCharacterSelectionUi();

    public void DisableCharacterSelectSubmitButton() =>
      _characterSelectionUi.DisableCharacterSelectSubmitButton();

    public void EnableCharacterSelectSubmitButton() =>
      _characterSelectionUi.EnableCharacterSelectSubmitButton();

    public void ChangeCharacterSelectionUiOnFirstPlayerReady() =>
      _characterSelectionUi.ChangeCharacterSelectionUiOnFirstPlayerReady();

    public void ChangeCharacterSelectionUiOnSecondPlayerReady() =>
      _characterSelectionUi.ChangeCharacterSelectionUiOnSecondPlayerReady();
  }
}