using Kulinaria.Tools.BattleTrier.Runtime.Data;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Registry;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Roles;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Ui
{
  public class CharacterSelectionVariant : MonoBehaviour, IPointerClickHandler
  {
    [SerializeField, Required, ChildGameObjectsOnly] private Image _icon;
    [SerializeField, Required, ChildGameObjectsOnly] private Button _deselectAllButton;
    [SerializeField, Required, ChildGameObjectsOnly] private Button _addButton;
    [SerializeField, Required, ChildGameObjectsOnly] private Button _subButton;
    [SerializeField, Required, ChildGameObjectsOnly] private TMP_Text _amountText;

    private RoleBase _role;
    private ICharacterRegistry _characterRegistry;

    private CharacterConfig _config;

    [Inject]
    private void Construct(RoleBase role, ICharacterRegistry characterRegistry)
    {
      _role = role;
      _characterRegistry = characterRegistry;
    }

    public void Initialize(CharacterConfig config)
    {
      _config = config;
      _icon.sprite = config.Icon;

      HideSelectionUi();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
      if (!_characterRegistry.PlayerHasCharactersOfConfig(_config))
      {
        _characterRegistry.Select(_config, 1);
        ShowSelectionUi();
      }
    }

    private void ShowSelectionUi()
    {
      _deselectAllButton.gameObject.SetActive(true);
      _addButton.gameObject.SetActive(true);
      _subButton.gameObject.SetActive(true);
      _amountText.gameObject.SetActive(true);
    }

    private void HideSelectionUi()
    {
      _deselectAllButton.gameObject.SetActive(false);
      _addButton.gameObject.SetActive(false);
      _subButton.gameObject.SetActive(false);
      _amountText.gameObject.SetActive(false);
    }
  }
}