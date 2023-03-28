using Kulinaria.Tools.BattleTrier.Runtime.Data;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Ui
{
  public class CharacterListItem : MonoBehaviour
  {
    [SerializeField, Required, ChildGameObjectsOnly] private TMP_Text _amountText;
    [SerializeField, Required, ChildGameObjectsOnly] private TMP_Text _nameText;
    [SerializeField, Required, ChildGameObjectsOnly] private TMP_Text _hpText;
    [SerializeField, Required, ChildGameObjectsOnly] private TMP_Text _apText;
    [SerializeField, Required, ChildGameObjectsOnly] private Image _iconImage;

    public CharacterConfig Config { get; private set; }

    public void Initialize(CharacterConfig config)
    {
      Config = config;
      _amountText.text = 1.ToString();
      _iconImage.sprite = config.Icon;
      _nameText.text = config.Name;
      _hpText.text = config.HealthPoints.ToString();
      _apText.text = config.ActionPoints.ToString();
    }

    public void SetAmount(int amount) =>
      _amountText.text = amount.ToString();
  }
}