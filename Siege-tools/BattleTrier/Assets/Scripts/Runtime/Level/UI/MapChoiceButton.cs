using Kulinaria.Tools.BattleTrier.Runtime.Data;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Kulinaria.Tools.BattleTrier.Runtime.Level.UI
{
  [RequireComponent(typeof(Button))]
  public class MapChoiceButton : MonoBehaviour
  {
    [SerializeField] private TMP_Text _description;
    [SerializeField] private Image _icon;
    [SerializeField] private Button _button;

    private LevelMediator _levelMediator;

    [Inject]
    private void Construct(LevelMediator levelMediator) => 
      _levelMediator = levelMediator;

    public void Initialize(BoardData data)
    {
      _description.text = data.Name;
      _icon.sprite = data.Icon;
      _button.onClick.AddListener(() =>
      {
        NetworkObject board = Instantiate(data.Prefab);
        board.Spawn();
        _levelMediator.HideMapChoicePanel();
      });
    }
  }
}