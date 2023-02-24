using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Runtime.Network.Roles.UI
{
  public class RoleMediator : MonoBehaviour
  {
    [SerializeField] private RoleSelectionButton _firstRoleButton;
    [SerializeField] private RoleUi _roleUi;

    public void Initialize() => 
      _roleUi.Initialize();

    public void ConfigureUIForLobbyMode(RoleUiMode mode) => 
      _roleUi.ConfigureUiForLobbyMode(mode);

    public void UpdatePlayerCount(int count)
    {
    }

    public void SerFirstRoleState(PlayerRoleState playerState) => 
      _firstRoleButton.SetState(playerState);
  }
}