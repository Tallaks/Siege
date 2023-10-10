using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Network;
using Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Services.Data;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Roles;
using Sirenix.OdinInspector;
using Unity.Netcode;
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

    private IInstantiator _instantiator;
    private IStaticDataProvider _staticDataProvider;
    private NetworkManager _networkManager;
    private CharacterSelectionNetwork _characterSelectionNetwork;

    [Inject]
    private void Construct(
      IInstantiator instantiator,
      IStaticDataProvider staticDataProvider,
      NetworkManager networkManager,
      CharacterSelectionNetwork characterSelectionNetwork)
    {
      _instantiator = instantiator;
      _staticDataProvider = staticDataProvider;
      _networkManager = networkManager;
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
          _instantiator.InstantiatePrefabForComponent<CharacterSelectionVariant>(_characterSelectionVariantPrefab,
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
      _characterSelectionNetwork.SubmitSelectionServerRpc((int)_networkManager.LocalClient.PlayerObject.GetComponent<NetworkPlayerObject>().State.Value);
    }
  }
}