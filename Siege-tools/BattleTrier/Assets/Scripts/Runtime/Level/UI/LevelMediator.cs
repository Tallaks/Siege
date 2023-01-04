using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Runtime.Level.UI
{
  public class LevelMediator : MonoBehaviour
  {
    [SerializeField] private GameObject _mapChoicePanel;
    [SerializeField] private RoleChoicePanel _roleChoicePanel;
    
    public void ShowMapChoice() => _mapChoicePanel.SetActive(true);

    public void ShowRoleChoice() => _roleChoicePanel.Show();
  }
}