using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Ui.Battle
{
  public class BattleLog : MonoBehaviour
  {
    [SerializeField] [Required] [ChildGameObjectsOnly(IncludeInactive = true)]
    private ScrollRect _scrollRect;

    [SerializeField] [Required] [ChildGameObjectsOnly(IncludeInactive = true)]
    private TMP_Text _logElementPrefab;

    public void AddLog(string log)
    {
      Debug.Log(log);
      Instantiate(_logElementPrefab, _scrollRect.content).text = log;
      LayoutRebuilder.ForceRebuildLayoutImmediate(_scrollRect.content);
      _scrollRect.verticalNormalizedPosition = 0;
    }
  }
}