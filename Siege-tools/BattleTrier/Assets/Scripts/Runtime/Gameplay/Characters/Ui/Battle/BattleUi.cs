using Sirenix.OdinInspector;
using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Ui.Battle
{
  public class BattleUi : MonoBehaviour
  {
    [SerializeField] [Required] [ChildGameObjectsOnly(IncludeInactive = true)]
    private GameObject _activePlayerBattleUi;

    [SerializeField] [Required] [ChildGameObjectsOnly(IncludeInactive = true)]
    private GameObject _spectatorPlayerBattleUi;

    public void ShowActivePlayerBattleUi() =>
      _activePlayerBattleUi.SetActive(true);

    public void ShowSpectatorBattleUi() =>
      _spectatorPlayerBattleUi.SetActive(true);

    public void HideActivePlayerBattleUi() =>
      _activePlayerBattleUi.SetActive(false);

    public void HideWaitingPlayerBattleUi() =>
      _spectatorPlayerBattleUi.SetActive(false);
  }
}