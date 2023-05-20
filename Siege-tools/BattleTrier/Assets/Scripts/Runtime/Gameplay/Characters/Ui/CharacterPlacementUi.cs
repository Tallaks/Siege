using Sirenix.OdinInspector;
using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Ui
{
  public class CharacterPlacementUi : MonoBehaviour
  {
    [SerializeField, Required, AssetSelector] private PlacementCharItem _charItemPrefab;
    [SerializeField, Required, ChildGameObjectsOnly] private Transform _listContent;
  }
}