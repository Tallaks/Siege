using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Selection;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.UI;
using Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Services.Data;
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
    [SerializeField, Required, ChildGameObjectsOnly] private Button _addButton;
    [SerializeField, Required, ChildGameObjectsOnly] private TMP_Text _amountText;
    [SerializeField, Required, ChildGameObjectsOnly] private Button _deselectAllButton;
    [SerializeField, Required, ChildGameObjectsOnly] private Image _icon;
    [SerializeField, Required, ChildGameObjectsOnly] private Button _subButton;
    private ICharacterSelection _characterSelection;

    private IStaticDataProvider _dataProvider;

    private int _id;
    private GameplayMediator _mediator;

    [Inject]
    private void Construct(IStaticDataProvider dataProvider, ICharacterSelection characterSelection,
      GameplayMediator mediator)
    {
      _dataProvider = dataProvider;
      _characterSelection = characterSelection;
      _mediator = mediator;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
      if (!_characterSelection.PlayerHasCharactersOfConfig(_id))
      {
        _characterSelection.AddCharacter(_id, 1);
        ShowSelectionUi();
      }

      _mediator.ShowConfigInfo(_id);
      _mediator.ChangeCharacterList();
    }

    public void Initialize(int configId)
    {
      _id = configId;
      _icon.sprite = _dataProvider.ConfigById(configId).Icon;

      _addButton.onClick.AddListener(OnAddButtonClicked);
      _subButton.onClick.AddListener(OnSubButtonClicked);
      _deselectAllButton.onClick.AddListener(OnDeselectButtonClicked);

      HideSelectionUi();
    }

    private void ShowSelectionUi()
    {
      _deselectAllButton.gameObject.SetActive(true);
      _addButton.gameObject.SetActive(true);
      _subButton.gameObject.SetActive(true);
      _amountText.gameObject.SetActive(true);
      _amountText.text = _characterSelection.Characters[_id].ToString();
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
      _characterSelection.AddCharacter(_id, 1);
      _amountText.text = _characterSelection.Characters[_id].ToString();
      _mediator.ChangeCharacterList();
    }

    private void OnSubButtonClicked()
    {
      _characterSelection.RemoveCharacter(_id, 1);
      if (!_characterSelection.PlayerHasCharactersOfConfig(_id))
        HideSelectionUi();
      else
        _amountText.text = _characterSelection.Characters[_id].ToString();

      _mediator.ChangeCharacterList();
    }

    private void OnDeselectButtonClicked()
    {
      _characterSelection.RemoveCharacter(_id, _characterSelection.Characters[_id]);
      HideSelectionUi();
      _mediator.ChangeCharacterList();
    }
  }
}