using Kulinaria.Tools.BattleTrier.Runtime.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Maps.Selection.UI
{
  public class MapSelectionButton : MonoBehaviour
  {
    [SerializeField] private Image _icon;
    
    public void Initialize(BoardConfig config) => 
      _icon.sprite = config.Icon;
  }
}