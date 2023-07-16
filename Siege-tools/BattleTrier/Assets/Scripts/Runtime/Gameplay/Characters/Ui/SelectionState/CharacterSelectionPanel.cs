using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Network;
using Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Services.Data;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Roles;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Ui.SelectionState
{
  public class CharacterSelectionPanel : MonoBehaviour
  {
    [SerializeField] [Required] [ChildGameObjectsOnly]
    private CharacterSelectionList _characterSelectionList;

    [SerializeField] [Required] [AssetSelector]
    private CharacterSelectionVariant _characterSelectionVariantPrefab;

    [SerializeField] [Required] [ChildGameObjectsOnly]
    private GridLayoutGroup _gridLayout;

    [SerializeField] [Required] [ChildGameObjectsOnly]
    private SelectedConfigInfo _selectedConfigInfo;

    [SerializeField] [Required] [ChildGameObjectsOnly]
    private Button _submitButton;

    private CharacterSelectionNetwork _characterSelectionNetwork;

    private DiContainer _container;
    private RoleBase _role;
    private IStaticDataProvider _staticDataProvider;

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
      foreach (int configId in _staticDataProvider.GetAllCharacterConfigIds())
      {
        var variant =
          _container.InstantiatePrefabForComponent<CharacterSelectionVariant>(_characterSelectionVariantPrefab,
            _gridLayout.transform);
        variant.Initialize(configId);
      }
    }

    public void ChangeCharacterList() =>
      _characterSelectionList.ChangeCharacterList();

    public void DisableCharacterSelectSubmitButton() =>
      _submitButton.interactable = false;

    public void EnableCharacterSelectSubmitButton() =>
      _submitButton.interactable = true;

    public void ShowConfigInfo(int configId) =>
      _selectedConfigInfo.ShowConfig(configId);

    private void OnSubmitButton()
    {
      DisableCharacterSelectSubmitButton();
      _characterSelectionNetwork.SubmitSelectionServerRpc((int)_role.State.Value);
    }
  }
}