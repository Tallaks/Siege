using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
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

    private Dictionary<RoleUiMode, List<GameObject>> _lobbyUiElementsByMode;
    private RoleMediator _mediator;
    private NetworkManager _networkManager;
    private Dictionary<RoleUiMode, RoleSelectionButton> _roleButtonsByMode;
    private RoleSelectionService _roleSelectionService;

    [Inject]
    private void Construct(RoleMediator mediator, RoleSelectionService roleSelectionService,
      NetworkManager networkManager)
    {
      _mediator = mediator;
      _roleSelectionService = roleSelectionService;
      _networkManager = networkManager;
    }

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
          [RoleUiMode.SeatChosenFirst] = _uiElementsForSeatChosen,
          [RoleUiMode.SeatChosenSecond] = _uiElementsForSeatChosen,
          [RoleUiMode.SeatChosenSpectator] = _uiElementsForSeatChosen
        };

        _roleButtonsByMode = new Dictionary<RoleUiMode, RoleSelectionButton>
        {
          [RoleUiMode.SeatChosenFirst] = _roleButtons[0],
          [RoleUiMode.SeatChosenSecond] = _roleButtons[1],
          [RoleUiMode.SeatChosenSpectator] = _roleButtons[2]
        };

        _backButton.onClick.AddListener(_mediator.OnRequestedShutdown);
      }
    }

    public void UpdateLobbyUi()
    {
      Initialize();
      foreach (RoleSelectionButton button in _roleButtonsByMode.Values)
        button.SetInteractable(true);
      foreach (List<GameObject> gameObjects in _lobbyUiElementsByMode.Values)
        gameObjects.ForEach(go => go.SetActive(false));

      foreach (PlayerRoleState playerRole in _roleSelectionService.PlayerRoles)
      {
        if (playerRole.State == RoleState.ChosenFirst)
          _roleButtonsByMode[RoleUiMode.SeatChosenFirst].SetInteractable(false);
        if (playerRole.State == RoleState.ChosenSecond)
          _roleButtonsByMode[RoleUiMode.SeatChosenSecond].SetInteractable(false);
        if (playerRole.ClientId == _networkManager.LocalClientId)
        {
          if (playerRole.State == RoleState.ChosenSpectator)
            _roleButtonsByMode[RoleUiMode.SeatChosenSpectator].SetInteractable(false);

          if (playerRole.State == RoleState.NotChosen || playerRole.State == RoleState.None)
            _lobbyUiElementsByMode[RoleUiMode.ChooseSeat].ForEach(go => go.SetActive(true));
          else if (playerRole.State == RoleState.ChosenFirst ||
                   playerRole.State == RoleState.ChosenSecond ||
                   playerRole.State == RoleState.ChosenSpectator)
            _lobbyUiElementsByMode[RoleUiMode.SeatChosenFirst].ForEach(go => go.SetActive(true));
        }
      }
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