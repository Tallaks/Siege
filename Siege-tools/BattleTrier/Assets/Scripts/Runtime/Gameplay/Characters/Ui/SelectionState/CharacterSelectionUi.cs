using Kulinaria.Tools.BattleTrier.Runtime.Network.Roles;
using Sirenix.OdinInspector;
using Unity.Netcode;
using UnityEngine;
using Zenject;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Ui.SelectionState
{
  public class CharacterSelectionUi : MonoBehaviour
  {
    [SerializeField] [Required] [Searchable]
    private CharacterSelectionPanel _characterSelectionPanel;

    [SerializeField] [Required]
    private GameObject _waitingAsPlayerPanel;

    [SerializeField] [Required]
    private GameObject _waitingAsSpectatorPanel;

    private RoleState _roleState;
    private NetworkManager _networkManager;

    [Inject]
    private void Construct(NetworkManager networkManager) =>
      _networkManager = networkManager;

    public void Initialize()
    {
      _roleState = _networkManager.LocalClient.PlayerObject.GetComponent<NetworkPlayerObject>().State.Value;
      if (_roleState is RoleState.ChosenFirst or RoleState.ChosenSecond)
        ShowCharacterSelectionPanel();
      else
        _waitingAsSpectatorPanel.SetActive(true);
    }

    public void ChangeCharacterList() =>
      _characterSelectionPanel.ChangeCharacterList();

    public void ChangeCharacterSelectionUiOnFirstPlayerReady()
    {
      if (_roleState == RoleState.ChosenFirst)
      {
        _characterSelectionPanel.gameObject.SetActive(false);
        _waitingAsPlayerPanel.SetActive(true);
      }
    }

    public void ChangeCharacterSelectionUiOnSecondPlayerReady()
    {
      if (_roleState == RoleState.ChosenSecond)
      {
        _characterSelectionPanel.gameObject.SetActive(false);
        _waitingAsPlayerPanel.SetActive(true);
      }
    }

    public void DisableCharacterSelectSubmitButton() =>
      _characterSelectionPanel.DisableCharacterSelectSubmitButton();

    public void EnableCharacterSelectSubmitButton() =>
      _characterSelectionPanel.EnableCharacterSelectSubmitButton();

    public void HideCharacterSelectionUi()
    {
      _characterSelectionPanel.gameObject.SetActive(false);
      _waitingAsPlayerPanel.SetActive(false);
      _waitingAsSpectatorPanel.SetActive(false);
    }

    public void ShowConfigInfo(int configId) =>
      _characterSelectionPanel.ShowConfigInfo(configId);

    private void ShowCharacterSelectionPanel()
    {
      _characterSelectionPanel.gameObject.SetActive(true);
      _characterSelectionPanel.Initialize();
    }
  }
}