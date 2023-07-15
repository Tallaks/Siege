using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Data;
using Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Services.Data;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Ui
{
  public class CharacterListItem : MonoBehaviour
  {
    [SerializeField] [Required] [ChildGameObjectsOnly]
    private TMP_Text _amountText;

    [SerializeField] [Required] [ChildGameObjectsOnly]
    private TMP_Text _apText;

    [SerializeField] [Required] [ChildGameObjectsOnly]
    private TMP_Text _hpText;

    [SerializeField] [Required] [ChildGameObjectsOnly]
    private Image _iconImage;

    [SerializeField] [Required] [ChildGameObjectsOnly]
    private TMP_Text _nameText;

    public CharacterConfig Config { get; private set; }

    private IStaticDataProvider _dataProvider;

    [Inject]
    private void Construct(IStaticDataProvider dataProvider) =>
      _dataProvider = dataProvider;

    public void Initialize(int configId)
    {
      Config = _dataProvider.ConfigById(configId);
      _amountText.text = 1.ToString();
      _iconImage.sprite = Config.Icon;
      _nameText.text = Config.Name;
      _hpText.text = Config.HealthPoints.ToString();
      _apText.text = Config.ActionPoints.ToString();
    }

    public void SetAmount(int amount) =>
      _amountText.text = amount.ToString();
  }
}