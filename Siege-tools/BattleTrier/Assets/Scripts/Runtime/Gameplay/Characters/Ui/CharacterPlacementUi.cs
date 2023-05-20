using Sirenix.OdinInspector;
using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Ui
{
  public class CharacterPlacementUi : MonoBehaviour
  {
    [SerializeField, Required, AssetSelector] private PlacementCharItem _charItemPrefab;
    [SerializeField, Required, ChildGameObjectsOnly] private Transform _listContent;
    [SerializeField, Required, ChildGameObjectsOnly] private GameObject _activePlayerPanel;
    [SerializeField, Required, ChildGameObjectsOnly] private GameObject _waitingPlayerPanel;
    [SerializeField, Required, ChildGameObjectsOnly] private GameObject _spectatorPanel;

    public void ShowPlacementActivePlayerUi() =>
      _activePlayerPanel.SetActive(true);

    public void ShowPlacementWaitingPlayerUi() =>
      _waitingPlayerPanel.SetActive(true);

    public void ShowPlacementSpectatorUi() =>
      _spectatorPanel.SetActive(true);
  }
}