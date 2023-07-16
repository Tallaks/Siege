using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Network;
using Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Services.Data;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Ui.Battle
{
  public class BattleEnemyListElement : MonoBehaviour
  {
    [SerializeField] [Required] [ChildGameObjectsOnly(IncludeInactive = true)]
    private Image _enemyIcon;

    [SerializeField] [Required] [ChildGameObjectsOnly(IncludeInactive = true)]
    private TMP_Text _enemyName;

    [SerializeField] [Required] [ChildGameObjectsOnly(IncludeInactive = true)]
    private TMP_Text _enemyHp;

    [SerializeField] [Required] [ChildGameObjectsOnly(IncludeInactive = true)]
    private TMP_Text _enemyAp;

    private IStaticDataProvider _staticDataProvider;

    [Inject]
    private void Construct(IStaticDataProvider staticDataProvider) =>
      _staticDataProvider = staticDataProvider;

    public void Initialize(CharacterNetworkData secondPlayerCharacter)
    {
      _enemyIcon.sprite = _staticDataProvider.ConfigById(secondPlayerCharacter.TypeId).Icon;
      _enemyAp.text = "Max AP: " + _staticDataProvider.ConfigById(secondPlayerCharacter.TypeId).ActionPoints;
      _enemyHp.text = "HP: " + secondPlayerCharacter.CurrentHp + "/" +
                      _staticDataProvider.ConfigById(secondPlayerCharacter.TypeId).HealthPoints;
      _enemyName.text = _staticDataProvider.ConfigById(secondPlayerCharacter.TypeId).Name + "_" +
                        secondPlayerCharacter.InstanceId;
    }
  }
}