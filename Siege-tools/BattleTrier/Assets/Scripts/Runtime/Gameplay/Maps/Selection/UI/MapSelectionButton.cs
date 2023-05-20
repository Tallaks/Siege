using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Maps.Data;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Maps.Selection.Network;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.UI;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Maps.Selection.UI
{
  public class MapSelectionButton : MonoBehaviour
  {
    [SerializeField] private Image _background;
    [SerializeField] private Button _button;
    [SerializeField] private Image _icon;
    private MapSelectionNetwork _mapSelectionNetwork;

    private GameplayMediator _mediator;

    [Inject]
    private void Construct(GameplayMediator mediator, MapSelectionNetwork mapSelectionNetwork)
    {
      _mediator = mediator;
      _mapSelectionNetwork = mapSelectionNetwork;
    }

    public void Initialize(BoardConfig config)
    {
      _icon.sprite = config.Icon;
      _button.onClick.AddListener(() => OnMapSelected(config.name));
    }

    public void Deselect() =>
      _background.color = new Color(0.4745098f, 0.8377303f, 1, 1);

    public void Select() =>
      _background.color = Color.yellow;

    private void OnMapSelected(string config)
    {
      _mapSelectionNetwork.Select(config);
      _mediator.SetSelectedMap(this);
    }
  }
}