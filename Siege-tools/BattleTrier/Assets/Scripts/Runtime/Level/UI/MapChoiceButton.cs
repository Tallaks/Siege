using Kulinaria.Tools.BattleTrier.Runtime.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Kulinaria.Tools.BattleTrier.Runtime.Level.UI
{
  [RequireComponent(typeof(Button))]
  public class MapChoiceButton : MonoBehaviour
  {
    [SerializeField] private TMP_Text _description;
    [SerializeField] private Image _icon;
    [SerializeField] private Button _button;

    public void Initialize(BoardData data)
    {
      _description.text = data.Name;
      _icon.sprite = data.Icon;
    }
  }
}