using Kulinaria.Tools.BattleTrier.Runtime.Data;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Registry;
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

    private ICharacterRegistry _characterRegistry;
    private CharacterConfig _config;

    [Inject]
    private void Construct(ICharacterRegistry characterRegistry) =>
      _characterRegistry = characterRegistry;

    public void Initialize(CharacterConfig config)
    {
      _config = config;
      _icon.sprite = config.Icon;
      _deselectAllButton.gameObject.SetActive(false);
      _addButton.gameObject.SetActive(false);
      _subButton.gameObject.SetActive(false);
      _amountText.gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
      
    }
  }
}