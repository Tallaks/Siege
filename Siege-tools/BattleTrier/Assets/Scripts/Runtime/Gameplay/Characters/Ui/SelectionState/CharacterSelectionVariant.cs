using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Registry;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.UI;
using Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Services.Data;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Ui.SelectionState
{
  public class CharacterSelectionVariant : MonoBehaviour, IPointerClickHandler
  {
    [SerializeField] [Required] [ChildGameObjectsOnly]
    private Button _addButton;

    [SerializeField] [Required] [ChildGameObjectsOnly]
    private TMP_Text _amountText;

    [SerializeField] [Required] [ChildGameObjectsOnly]
    private Button _deselectAllButton;

    [SerializeField] [Required] [ChildGameObjectsOnly]
    private Image _icon;

    [SerializeField] [Required] [ChildGameObjectsOnly]
    private Button _subButton;

    private ICharacterRegistry _characterRegistry;

    private IStaticDataProvider _dataProvider;

    private int _id;
    private GameplayMediator _mediator;

    [Inject]
    private void Construct(IStaticDataProvider dataProvider, ICharacterRegistry characterRegistry,
      GameplayMediator mediator)
    {
      _dataProvider = dataProvider;
      _characterRegistry = characterRegistry;
      _mediator = mediator;
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

    public void OnPointerClick(PointerEventData eventData)
    {
      if (!_characterRegistry.PlayerHasCharactersOfConfig(_id))
      {
        _characterRegistry.AddCharacterGroup(_id, 1);
        ShowSelectionUi();
      }

      _mediator.ShowConfigInfo(_id);
      _mediator.ChangeCharacterList();
    }

    private void ShowSelectionUi()
    {
      _deselectAllButton.gameObject.SetActive(true);
      _addButton.gameObject.SetActive(true);
      _subButton.gameObject.SetActive(true);
      _amountText.gameObject.SetActive(true);
      _amountText.text = _characterRegistry.CharactersGroupsByConfigId[_id].ToString();
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
      _characterRegistry.AddCharacterGroup(_id, 1);
      _amountText.text = _characterRegistry.CharactersGroupsByConfigId[_id].ToString();
      _mediator.ChangeCharacterList();
    }

    private void OnSubButtonClicked()
    {
      _characterRegistry.RemoveCharacterGroup(_id, 1);
      if (!_characterRegistry.PlayerHasCharactersOfConfig(_id))
        HideSelectionUi();
      else
        _amountText.text = _characterRegistry.CharactersGroupsByConfigId[_id].ToString();

      _mediator.ChangeCharacterList();
    }

    private void OnDeselectButtonClicked()
    {
      _characterRegistry.RemoveCharacterGroup(_id, _characterRegistry.CharactersGroupsByConfigId[_id]);
      HideSelectionUi();
      _mediator.ChangeCharacterList();
    }
  }
}