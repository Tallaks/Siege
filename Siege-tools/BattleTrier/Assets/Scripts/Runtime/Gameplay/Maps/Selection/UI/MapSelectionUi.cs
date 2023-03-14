using Kulinaria.Tools.BattleTrier.Runtime.Data;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.UI;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Gameplay;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Roles;
using Unity.Netcode;
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

    private GameplayMediator _mediator;
    private DiContainer _container;

    private MapSelectionBehaviour _mapSelectionBehaviour;

    [Inject]
    private void Construct(DiContainer container, GameplayMediator mediator)
    {
      _container = container;
      _mediator = mediator;
    }

    public void Initialize(RoleState stateValue, MapSelectionBehaviour mapSelectionBehaviour)
    {
      _mapSelectionBehaviour = mapSelectionBehaviour;
      _selectMapButton.onClick.AddListener(OnMapSelectedServerRpc);
      if (stateValue == RoleState.ChosenFirst)
      {
        _firstRoleUi.SetActive(true);
        foreach (BoardConfig config in Resources.LoadAll<BoardConfig>("Configs/Boards/"))
        {
          var button =
            _container.InstantiatePrefabForComponent<MapSelectionButton>(_mapSelectionPrefab, _mapSelectionContainer);
          button.Initialize(config);
        }
      }
      else
        _otherRoleUi.SetActive(true);
    }

    private void OnMapSelectedServerRpc() =>
      _mapSelectionBehaviour.SetSelectedServerRpc();

    public void HideMapSelectionUi()
    {
      _firstRoleUi.SetActive(false);
      _otherRoleUi.SetActive(false);
    }
  }
}