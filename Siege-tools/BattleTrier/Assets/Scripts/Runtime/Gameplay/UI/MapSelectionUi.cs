using Kulinaria.Tools.BattleTrier.Runtime.Network.Roles;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.UI
{
  public class MapSelectionUi : MonoBehaviour
  {
    [SerializeField] private GameObject _firstRoleUi;
    [SerializeField] private GameObject _otherRoleUi;
    [SerializeField] private Button _selectMapButton;

    private GameplayMediator _mediator;

    private NetworkVariable<bool> _mapSelectedState;

    [Inject]
    private void Construct(GameplayMediator mediator) =>
      _mediator = mediator;

    public void Initialize(RoleState stateValue, NetworkVariable<bool> mapSelectedState)
    {
      _mapSelectedState = mapSelectedState;
      _selectMapButton.onClick.AddListener(OnMapSelected);
      if (stateValue == RoleState.ChosenFirst)
        _firstRoleUi.SetActive(true);
      else
        _otherRoleUi.SetActive(true);
    }

    private void OnMapSelected() =>
      _mapSelectedState.Value = true;

    public void HideMapSelectionUi()
    {
      _firstRoleUi.SetActive(false);
      _otherRoleUi.SetActive(false);
    }
  }
}