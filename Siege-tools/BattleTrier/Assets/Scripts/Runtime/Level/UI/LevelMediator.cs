using Kulinaria.Tools.BattleTrier.Runtime.Network.Authentication;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Data;
using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Runtime.Level.UI
{
  public class LevelMediator : MonoBehaviour
  {
    [SerializeField] private MapChoicePanel _mapChoicePanel;
    [SerializeField] private RoleChoicePanel _roleChoicePanel;

    public void ShowMapChoice() => _mapChoicePanel.Show();
    public void InitMaps() => _mapChoicePanel.Initialize();
    public void HideMapChoicePanel() => _mapChoicePanel.Hide();
    public void InitRoles() => _roleChoicePanel.Initialize();
    public void ShowRoleChoice() => _roleChoicePanel.Show();
    public void HideRoleChoice() => _roleChoicePanel.Hide();
    public void RegisterSecondPlayer() => FindObjectOfType<PlayerService>().RegisterPlayerServerRpc(Role.SecondPlayer);
  }
}