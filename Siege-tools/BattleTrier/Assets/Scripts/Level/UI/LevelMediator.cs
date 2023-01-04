using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Level.UI
{
  public class LevelMediator : MonoBehaviour
  {
    [SerializeField] private GameObject _mapChoicePanel;

    public void ShowMapChoice() => _mapChoicePanel.SetActive(true);
  }
}