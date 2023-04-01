using Kulinaria.Tools.BattleTrier.Runtime.Network.Connection.States;
using UnityEngine;
using Zenject;

namespace Kulinaria.Tools.BattleTrier.Runtime.Network.Roles.UI
{
  public class RoleMediator : MonoBehaviour
  {
    [SerializeField] private RoleUi _roleUi;

    [Inject] private IConnectionStateMachine _connectionStateMachine;

    public void Initialize() =>
      _roleUi.Initialize();

    public void ConfigureUIForLobbyMode(RoleUiMode mode) => 
      _roleUi.ConfigureUiForLobbyMode(mode);

    public void UpdatePlayerCount(int count) => 
      _roleUi.UpdatePlayerCount(count);

    public void DestroyButtons() => 
      _roleUi.DestroyButtons();

    public void OnRequestedShutdown() =>
      (_connectionStateMachine.CurrentState as IRequestShutdown)?.OnUserRequestedShutdown();
  }
}