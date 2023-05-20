using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

namespace Kulinaria.Tools.BattleTrier.Runtime.Network.Roles.UI
{
  [RequireComponent(typeof(Button))]
  public class RoleSelectionButton : MonoBehaviour
  {
    [FormerlySerializedAs("_buttonIndex"), SerializeField] public RoleState ButtonIndex;
    private Button _button;

    private RoleSelectionClient _roleSelection;
    private RoleSelectionService _selectionService;

    [Inject]
    private void Construct(RoleSelectionClient roleSelection, RoleSelectionService selectionService)
    {
      _selectionService = selectionService;
      _roleSelection = roleSelection;
    }

    private void Awake()
    {
      _button = GetComponent<Button>();
      _button.onClick.AddListener(OnRoleChosen);
    }

    private void Update()
    {
      if (ButtonIndex == RoleState.ChosenSpectator)
        return;
      foreach (PlayerRoleState playerRole in _selectionService.PlayerRoles)
        if (playerRole.State == ButtonIndex)
        {
          _button.interactable = false;
          return;
        }

      _button.interactable = true;
    }

    private void OnRoleChosen() =>
      _roleSelection.OnPlayerChosenRole(ButtonIndex);
  }
}