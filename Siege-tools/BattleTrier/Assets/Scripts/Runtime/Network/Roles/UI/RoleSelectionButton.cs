using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Kulinaria.Tools.BattleTrier.Runtime.Network.Roles.UI
{
  [RequireComponent(typeof(Button))]
  public class RoleSelectionButton : MonoBehaviour
  {
    [SerializeField] private RoleState _buttonIndex;

    private RoleSelectionClient _roleSelection;
    private Button _button;
    private PlayerRoleState _player;

    [Inject]
    private void Construct(RoleSelectionClient roleSelection) =>
      _roleSelection = roleSelection;

    private void Awake()
    {
      _button = GetComponent<Button>();
      _button.onClick.AddListener(OnRoleChosen);
    }

    public void SetState(PlayerRoleState playerState)
    {
      if (playerState.State == _buttonIndex)
        _button.interactable = false;
      else
        _button.interactable = true;
    }

    private void OnRoleChosen() =>
      _roleSelection.OnPlayerChosenRole(_buttonIndex);
  }
}