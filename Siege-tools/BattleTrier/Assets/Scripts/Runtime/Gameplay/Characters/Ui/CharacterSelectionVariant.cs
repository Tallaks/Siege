using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Data;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Selection;
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

    private ICharacterSelection _characterSelection;
    private GameplayMediator _mediator;

    private CharacterConfig _config;

    [Inject]
    private void Construct(ICharacterSelection characterSelection, GameplayMediator mediator)
    {
      _characterSelection = characterSelection;
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
      if (!_characterSelection.PlayerHasCharactersOfConfig(_config))
      {
        _characterSelection.AddCharacter(_config, 1);
        ShowSelectionUi();
      }

      _mediator.ShowConfigInfo(_config);
      _mediator.ChangeCharacterList();
    }

    private void ShowSelectionUi()
    {
      _deselectAllButton.gameObject.SetActive(true);
      _addButton.gameObject.SetActive(true);
      _subButton.gameObject.SetActive(true);
      _amountText.gameObject.SetActive(true);
      _amountText.text = _characterSelection.Characters[_config].ToString();
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
      _characterSelection.AddCharacter(_config, 1);
      _amountText.text = _characterSelection.Characters[_config].ToString();
      _mediator.ChangeCharacterList();
    }

    private void OnSubButtonClicked()
    {
      _characterSelection.RemoveCharacter(_config, 1);
      if (!_characterSelection.PlayerHasCharactersOfConfig(_config))
        HideSelectionUi();
      else
        _amountText.text = _characterSelection.Characters[_config].ToString();

      _mediator.ChangeCharacterList();
    }

    private void OnDeselectButtonClicked()
    {
      _characterSelection.RemoveCharacter(_config, _characterSelection.Characters[_config]);
      HideSelectionUi();
      _mediator.ChangeCharacterList();
    }
  }
}