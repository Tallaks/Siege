using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Maps.Selection.UI;
using Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Services.Applications;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Gameplay;
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

    [Inject] private IApplicationService _applicationService;

    private void Awake() =>
      _quitButton.onClick.AddListener(_applicationService.QuitApplication);

    public void InitializeMapSelectionUi(RoleState stateValue, MapSelectionNetwork mapSelectionNetwork) =>
      _mapSelectionUi.Initialize(stateValue, mapSelectionNetwork);

    public void HideMapSelectionUi() =>
      _mapSelectionUi.HideMapSelectionUi();

    public void EnableSubmitButton() =>
      _mapSelectionUi.EnableSubmitButton();

    public void SetSelectedMap(MapSelectionButton selected) =>
      _mapSelectionUi.SetMap(selected);
  }
}