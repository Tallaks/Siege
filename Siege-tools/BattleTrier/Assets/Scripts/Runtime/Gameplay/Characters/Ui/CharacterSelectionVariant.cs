using Kulinaria.Tools.BattleTrier.Runtime.Data;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Ui
{
  public class CharacterSelectionVariant : MonoBehaviour
  {
    [SerializeField, Required, ChildGameObjectsOnly] private Image _icon;
    [SerializeField, Required, ChildGameObjectsOnly] private Button _deselectAllButton;
    [SerializeField, Required, ChildGameObjectsOnly] private Button _addButton;
    [SerializeField, Required, ChildGameObjectsOnly] private Button _subButton;
    [SerializeField, Required, ChildGameObjectsOnly] private TMP_Text _amountText;

    public void Initialize(CharacterConfig config)
    {
      _icon.sprite = config.Icon;
      _deselectAllButton.gameObject.SetActive(false);
      _addButton.gameObject.SetActive(false);
      _subButton.gameObject.SetActive(false);
      _amountText.gameObject.SetActive(false);
    }
  }
}