using Kulinaria.Tools.BattleTrier.Runtime.Data;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Registry;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.UI;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Ui
{
  public class CharacterSelectionVariant : MonoBehaviour, IPointerClickHandler
  {
    [SerializeField, Required, ChildGameObjectsOnly] private Image _icon;
    [SerializeField, Required, ChildGameObjectsOnly] private Button _deselectAllButton;
    [SerializeField, Required, ChildGameObjectsOnly] private Button _addButton;
    [SerializeField, Required, ChildGameObjectsOnly] private Button _subButton;
    [SerializeField, Required, ChildGameObjectsOnly] private TMP_Text _amountText;

    private ICharacterRegistry _characterRegistry;
    private GameplayMediator _mediator;

    private CharacterConfig _config;

    [Inject]
    private void Construct(ICharacterRegistry characterRegistry, GameplayMediator mediator)
    {
      _characterRegistry = characterRegistry;
      _mediator = mediator;
    }

    public void Initialize(CharacterConfig config)
    {
      _config = config;
      _icon.sprite = config.Icon;

      _addButton.onClick.AddListener(OnAddButtonClicked);
      _subButton.onClick.AddListener(OnSubButtonClicked);
      _deselectAllButton.onClick.AddListener(OnDeselectButtonClicked);
      
      HideSelectionUi();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
      if (!_characterRegistry.PlayerHasCharactersOfConfig(_config))
      {
        _characterRegistry.AddCharacter(_config, 1);
        ShowSelectionUi();
      }

      _mediator.ShowConfigInfo(_config);
    }

    private void ShowSelectionUi()
    {
      _deselectAllButton.gameObject.SetActive(true);
      _addButton.gameObject.SetActive(true);
      _subButton.gameObject.SetActive(true);
      _amountText.gameObject.SetActive(true);
      _amountText.text = _characterRegistry.Characters[_config].ToString();
    }

    private void HideSelectionUi()
    {
      _deselectAllButton.gameObject.SetActive(false);
      _addButton.gameObject.SetActive(false);
      _subButton.gameObject.SetActive(false);
      _amountText.gameObject.SetActive(false);
    }

    private void OnAddButtonClicked()
    {
      _characterRegistry.AddCharacter(_config, 1);
      _amountText.text = _characterRegistry.Characters[_config].ToString();
    }

    private void OnSubButtonClicked()
    {
      _characterRegistry.RemoveCharacter(_config, 1);
      if(!_characterRegistry.PlayerHasCharactersOfConfig(_config))
        HideSelectionUi();
      else
        _amountText.text = _characterRegistry.Characters[_config].ToString();
    }

    private void OnDeselectButtonClicked()
    {
      _characterRegistry.RemoveCharacter(_config, _characterRegistry.Characters[_config]);
      HideSelectionUi();
    }
  }
}