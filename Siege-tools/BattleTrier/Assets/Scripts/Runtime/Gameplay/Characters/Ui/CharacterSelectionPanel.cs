using Kulinaria.Tools.BattleTrier.Runtime.Data;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Ui
{
  public class CharacterSelectionPanel : MonoBehaviour
  {
    [SerializeField, Required, AssetSelector] private CharacterSelectionVariant _characterSelectionVariantPrefab;
    [SerializeField, Required, ChildGameObjectsOnly] private SelectedConfigInfo _selectedConfigInfo;
    [SerializeField, Required, ChildGameObjectsOnly] private GridLayoutGroup _gridLayout;
    [SerializeField, Required, ChildGameObjectsOnly] private CharacterList _characterList;
    [SerializeField, Required, ChildGameObjectsOnly] private Button _submitButton;

    private DiContainer _container;

    [Inject]
    private void Construct(DiContainer container) =>
      _container = container;

    public void Initialize()
    {
      _submitButton.onClick.AddListener(OnSubmitButton);
      _selectedConfigInfo.Initialize();
      CharacterConfig[] characterConfigs = Resources.LoadAll<CharacterConfig>("Configs/Characters/");
      foreach (CharacterConfig config in characterConfigs)
      {
        var variant =
          _container.InstantiatePrefabForComponent<CharacterSelectionVariant>(_characterSelectionVariantPrefab,
            _gridLayout.transform);
        variant.Initialize(config);
      }
    }

    public void ShowConfigInfo(CharacterConfig config) =>
      _selectedConfigInfo.ShowConfig(config);

    public void ChangeCharacterList() =>
      _characterList.ChangeCharacterList();

    private void OnSubmitButton()
    {
      
    }
  }
}