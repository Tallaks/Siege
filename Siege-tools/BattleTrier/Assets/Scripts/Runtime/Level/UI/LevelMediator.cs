using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Runtime.Level.UI
{
  public class LevelMediator : MonoBehaviour
  {
    [SerializeField] private MapChoicePanel _mapChoicePanel;
    [SerializeField] private RoleChoicePanel _roleChoicePanel;
    
    public void ShowMapChoice() => _mapChoicePanel.Show();
    public void InitMaps() => _mapChoicePanel.Initialize();
    public void ShowRoleChoice() => _roleChoicePanel.Show();
  }
}