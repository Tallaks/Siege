using System.Collections.Generic;
using System.Linq;
using Kulinaria.Tools.BattleTrier.Runtime.Data;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Gameplay;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Roles;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Maps.Selection.UI
{
  public class MapSelectionUi : MonoBehaviour
  {
    [SerializeField] private GameObject _firstRoleUi;
    [SerializeField] private GameObject _otherRoleUi;
    [SerializeField] private Button _selectMapButton;
    [SerializeField] private Transform _mapSelectionContainer;
    [SerializeField] private MapSelectionButton _mapSelectionPrefab;

    private DiContainer _container;

    private MapSelectionNetwork _mapSelectionNetwork;
    private List<MapSelectionButton> _mapSelectionButtons = new();

    [Inject]
    private void Construct(DiContainer container) => 
      _container = container;

    public void Initialize(RoleState stateValue, MapSelectionNetwork mapSelectionNetwork)
    {
      _selectMapButton.interactable = false;
      _mapSelectionNetwork = mapSelectionNetwork;
      _selectMapButton.onClick.AddListener(OnMapSelectedServerRpc);
      if (stateValue == RoleState.ChosenFirst)
      {
        _firstRoleUi.SetActive(true);
        foreach (BoardConfig config in Resources.LoadAll<BoardConfig>("Configs/Boards/"))
        {
          var button =
            _container.InstantiatePrefabForComponent<MapSelectionButton>(_mapSelectionPrefab, _mapSelectionContainer);
          button.Initialize(config, mapSelectionNetwork);
          _mapSelectionButtons.Add(button);
        }
      }
      else
        _otherRoleUi.SetActive(true);
    }

    private void OnMapSelectedServerRpc() =>
      _mapSelectionNetwork.SetSelectedServerRpc();

    public void HideMapSelectionUi()
    {
      _firstRoleUi.SetActive(false);
      _otherRoleUi.SetActive(false);
    }

    public void EnableSubmitButton() =>
      _selectMapButton.interactable = true;

    public void SetMap(MapSelectionButton selected)
    {
      foreach (MapSelectionButton button in _mapSelectionButtons)
        button.Deselect();

      foreach (MapSelectionButton button in _mapSelectionButtons.Where(button => button == selected))
      {
        button.Select();
        return;
      }
    }
  }
}