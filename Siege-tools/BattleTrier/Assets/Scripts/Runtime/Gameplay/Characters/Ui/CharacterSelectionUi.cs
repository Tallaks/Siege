using Kulinaria.Tools.BattleTrier.Runtime.Network.Gameplay;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Roles;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Ui
{
  public class CharacterSelectionUi : MonoBehaviour
  {
    [SerializeField, Required, Searchable] private CharacterSelectionPanel _characterSelectionPanel;
    [SerializeField, Required] private GameObject _waitingAsPlayerPanel;
    [SerializeField, Required] private GameObject _waitingAsSpectatorPanel;

    private CharacterSelectionNetwork _characterSelectionNetwork;

    public void Initialize(RoleState stateValue, CharacterSelectionNetwork characterSelectionNetwork)
    {
      _characterSelectionNetwork = characterSelectionNetwork;
      if (stateValue is RoleState.ChosenFirst or RoleState.ChosenSecond)
        ShowCharacterSelectionPanel();
      else
        _waitingAsSpectatorPanel.SetActive(true);
    }

    private void ShowCharacterSelectionPanel()
    {
      _characterSelectionPanel.gameObject.SetActive(true);
      _characterSelectionPanel.Initialize();
    }
  }
}