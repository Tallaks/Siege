using System.Collections.Generic;
using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Runtime.Network.Roles.UI
{
  public class RoleUi : MonoBehaviour
  {
    [SerializeField] private List<GameObject> _uiElementsForChooseSeat;
    [SerializeField] private List<GameObject> _uiElementsForFatalError;
    [SerializeField] private List<GameObject> _uiElementsForLobbyEnding;
    [SerializeField] private List<GameObject> _uiElementsForSeatChosen;

    private Dictionary<RoleUiMode, List<GameObject>> _lobbyUiElementsByMode;
    private int _lastSeatSelected;

    public void Initialize()
    {
      _lobbyUiElementsByMode = new Dictionary<RoleUiMode, List<GameObject>>
      {
        [RoleUiMode.ChooseSeat]  = _uiElementsForChooseSeat,
        [RoleUiMode.FatalError]  = _uiElementsForFatalError,
        [RoleUiMode.LobbyEnding] = _uiElementsForLobbyEnding,
        [RoleUiMode.SeatChosen]  = _uiElementsForSeatChosen
      };
    }

    public void ConfigureUiForLobbyMode(RoleUiMode mode)
    {
      foreach (List<GameObject> list in _lobbyUiElementsByMode.Values)
      foreach (GameObject uiElement in list)
        uiElement.SetActive(false);

      foreach (GameObject element in _lobbyUiElementsByMode[mode])
        element.SetActive(true);
    }
  }
}