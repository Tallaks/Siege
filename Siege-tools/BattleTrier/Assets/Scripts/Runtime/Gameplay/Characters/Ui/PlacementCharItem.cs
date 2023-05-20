using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Ui
{
  public class PlacementCharItem : MonoBehaviour
  {
    [SerializeField, Required, ChildGameObjectsOnly] private Image _icon;
    [SerializeField, Required, ChildGameObjectsOnly] private TMP_Text _count;
  }
}