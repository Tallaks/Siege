using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Data;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Network;
using Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Services.Data;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Roles;
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
    private IStaticDataProvider _staticDataProvider;
    private RoleBase _role;
    private CharacterSelectionNetwork _characterSelectionNetwork;

    [Inject]
    private void Construct(
      DiContainer container,
      IStaticDataProvider staticDataProvider,
      RoleBase role,
      CharacterSelectionNetwork characterSelectionNetwork)
    {
      _container = container;
      _staticDataProvider = staticDataProvider;
      _role = role;
      _characterSelectionNetwork = characterSelectionNetwork;
    }

    public void Initialize()
    {
      DisableCharacterSelectSubmitButton();
      _submitButton.onClick.AddListener(OnSubmitButton);
      _selectedConfigInfo.Initialize();
      foreach (CharacterConfig config in _staticDataProvider.GetAllCharacterConfigs())
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

    public void EnableCharacterSelectSubmitButton() =>
      _submitButton.interactable = true;

    public void DisableCharacterSelectSubmitButton() =>
      _submitButton.interactable = false;

    private void OnSubmitButton()
    {
      DisableCharacterSelectSubmitButton();
      _characterSelectionNetwork.SubmitSelectionServerRpc((int)_role.State.Value);
    }
  }
}