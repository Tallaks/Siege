using Unity.Netcode;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

namespace Kulinaria.Tools.BattleTrier.Runtime.Network.Roles.UI
{
  [RequireComponent(typeof(Button))]
  public class RoleSelectionButton : MonoBehaviour
  {
    [FormerlySerializedAs("_buttonIndex")] [SerializeField]
    public RoleState ButtonIndex;

    private Button _button;
    private NetworkManager _networkManager;
    private RoleSelectionService _selectionService;

    [Inject]
    private void Construct(RoleSelectionService selectionService, NetworkManager networkManager)
    {
      _selectionService = selectionService;
      _networkManager = networkManager;
    }

    private void Awake()
    {
      _button = GetComponent<Button>();
      _button.onClick.AddListener(OnRoleChosen);
    }

    public void SetInteractable(bool interactable)
    {
      if (_button == null)
        _button = GetComponent<Button>();
      _button.interactable = interactable;
    }

    private void OnRoleChosen() =>
      _selectionService.ChangeSeatServerRpc(_networkManager.LocalClientId, (int)ButtonIndex);
  }
}