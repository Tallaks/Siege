using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Data;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Ui
{
  public class SelectedConfigInfo : MonoBehaviour
  {
    [SerializeField, Required, ChildGameObjectsOnly] private TMP_Text _nameLabel;
    [SerializeField, Required, ChildGameObjectsOnly] private TMP_Text _hpAmountLabel;
    [SerializeField, Required, ChildGameObjectsOnly] private TMP_Text _apAmountLabel;
    
    public void Initialize() =>
      gameObject.SetActive(false);

    public void ShowConfig(CharacterConfig config)
    {
      gameObject.SetActive(true);
      _nameLabel.text = config.Name;
      _hpAmountLabel.text = config.HealthPoints.ToString();
      _apAmountLabel.text = config.ActionPoints.ToString();
    }
  }
}