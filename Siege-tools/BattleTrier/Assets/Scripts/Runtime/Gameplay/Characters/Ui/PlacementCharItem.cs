using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Data;
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

    public void Initialize(CharacterConfig configById, int characterCount)
    {
      _icon.sprite = configById.Icon;
      _count.text = characterCount.ToString();
    }
  }
}