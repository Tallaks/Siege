using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Kulinaria.Tools.BattleTrier.Runtime.Network.Roles.UI
{
  public class RoleUi : MonoBehaviour
  {
    [SerializeField] private Button _backButton;
    [SerializeField] private TMP_Text _playerCounter;
    [SerializeField] private RoleSelectionButton[] _roleButtons;
    [SerializeField] private List<GameObject> _uiElementsForChooseSeat;
    [SerializeField] private List<GameObject> _uiElementsForFatalError;
    [SerializeField] private List<GameObject> _uiElementsForLobbyEnding;
    [SerializeField] private List<GameObject> _uiElementsForSeatChosen;
    private int _lastSeatSelected;

    private Dictionary<RoleUiMode, List<GameObject>> _lobbyUiElementsByMode;
    private RoleMediator _mediator;

    [Inject]
    private void Construct(RoleMediator mediator) =>
      _mediator = mediator;

    public void Initialize()
    {
      if (_lobbyUiElementsByMode == null)
      {
        Debug.Log("Role Ui Initialization");
        _lobbyUiElementsByMode = new Dictionary<RoleUiMode, List<GameObject>>
        {
          [RoleUiMode.ChooseSeat] = _uiElementsForChooseSeat,
          [RoleUiMode.FatalError] = _uiElementsForFatalError,
          [RoleUiMode.LobbyEnding] = _uiElementsForLobbyEnding,
          [RoleUiMode.SeatChosen] = _uiElementsForSeatChosen
        };

        _backButton.onClick.AddListener(_mediator.OnRequestedShutdown);
      }
    }

    public void ConfigureUiForLobbyMode(RoleUiMode mode)
    {
      Initialize();
      foreach (List<GameObject> list in _lobbyUiElementsByMode.Values)
      foreach (GameObject uiElement in list)
        uiElement.SetActive(false);

      foreach (GameObject element in _lobbyUiElementsByMode[mode])
        element.SetActive(true);
    }

    public void DestroyButtons()
    {
      foreach (RoleSelectionButton button in _roleButtons)
        Destroy(button.gameObject);
    }

    public void UpdatePlayerCount(int count) =>
      _playerCounter.text = count.ToString();
  }
}