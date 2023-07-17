using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Network;
using Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Services.Data;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Ui.Battle
{
  public class BattlePlayerListElement : MonoBehaviour
  {
    [SerializeField] [Required] [ChildGameObjectsOnly]
    private Image _characterIcon;

    [SerializeField] [Required] [ChildGameObjectsOnly]
    private TMP_Text _characterName;

    [SerializeField] [Required] [ChildGameObjectsOnly]
    private TMP_Text _characterHp;

    [SerializeField] [Required] [ChildGameObjectsOnly]
    private TMP_Text _characterAp;

    [SerializeField] [Required] [ChildGameObjectsOnly]
    private Button _characterActionsButton;

    private IStaticDataProvider _staticDataProvider;

    [Inject]
    private void Construct(IStaticDataProvider staticDataProvider) =>
      _staticDataProvider = staticDataProvider;

    public void Initialize(CharacterNetworkData characterNetworkData)
    {
      _characterIcon.sprite = _staticDataProvider.ConfigById(characterNetworkData.TypeId).Icon;
      _characterName.text = _staticDataProvider.ConfigById(characterNetworkData.TypeId).Name + "_" +
                            characterNetworkData.InstanceId;
      _characterHp.text = "HP: " + _staticDataProvider.ConfigById(characterNetworkData.TypeId).HealthPoints + "/" +
                          characterNetworkData.CurrentHp;
      _characterAp.text = "AP: " + _staticDataProvider.ConfigById(characterNetworkData.TypeId).ActionPoints + "/" +
                          _staticDataProvider.ConfigById(characterNetworkData.TypeId).ActionPoints;
      _characterActionsButton.onClick.AddListener(() => Debug.Log("Character actions button clicked"));
    }
  }
}