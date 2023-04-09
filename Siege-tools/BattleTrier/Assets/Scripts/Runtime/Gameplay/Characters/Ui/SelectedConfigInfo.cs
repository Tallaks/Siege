using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Data;
using Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Services.Data;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using Zenject;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Ui
{
  public class SelectedConfigInfo : MonoBehaviour
  {
    [SerializeField, Required, ChildGameObjectsOnly] private TMP_Text _nameLabel;
    [SerializeField, Required, ChildGameObjectsOnly] private TMP_Text _hpAmountLabel;
    [SerializeField, Required, ChildGameObjectsOnly] private TMP_Text _apAmountLabel;
    
    private IStaticDataProvider _dataProvider;

    [Inject]
    private void Construct(IStaticDataProvider dataProvider) =>
      _dataProvider = dataProvider;

    public void Initialize() =>
      gameObject.SetActive(false);

    public void ShowConfig(int configId)
    {
      CharacterConfig config = _dataProvider.ConfigById(configId);
      gameObject.SetActive(true);
      _nameLabel.text = config.Name;
      _hpAmountLabel.text = config.HealthPoints.ToString();
      _apAmountLabel.text = config.ActionPoints.ToString();
    }
  }
}