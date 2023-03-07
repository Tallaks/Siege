using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Runtime.Network.Roles.UI
{
  public class RoleMediator : MonoBehaviour
  {
    [SerializeField] private RoleUi _roleUi;

    public void Initialize() => 
      _roleUi.Initialize();

    public void ConfigureUIForLobbyMode(RoleUiMode mode) => 
      _roleUi.ConfigureUiForLobbyMode(mode);

    public void UpdatePlayerCount(int count) => 
      _roleUi.UpdatePlayerCount(count);

    public void DestroyButtons() => 
      _roleUi.DestroyButtons();
  }
}