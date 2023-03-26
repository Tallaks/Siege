using Kulinaria.Tools.BattleTrier.Runtime.Data;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Ui
{
  public class CharacterSelectionPanel : MonoBehaviour
  {
    [SerializeField, Required, AssetSelector]
    private CharacterListItem _characterListItemPrefab;

    [SerializeField, Required, AssetSelector] 
    private CharacterSelectionVariant _characterSelectionVariantPrefab;

    [SerializeField, Required, ChildGameObjectsOnly]
    private GameObject _selectedCharacterInfo;

    [SerializeField, Required, ChildGameObjectsOnly]
    private GridLayoutGroup _gridLayout;

    private DiContainer _container;

    [Inject]
    private void Construct(DiContainer container) =>
      _container = container;

    public void Initialize()
    {
      _selectedCharacterInfo.SetActive(false);
      CharacterConfig[] characterConfigs = Resources.LoadAll<CharacterConfig>("Configs/Characters/");
      foreach (CharacterConfig config in characterConfigs)
      {
        var variant = _container.InstantiatePrefabForComponent<CharacterSelectionVariant>(_characterSelectionVariantPrefab, _gridLayout.transform);
        variant.Initialize(config);
      }
    }
  }
}