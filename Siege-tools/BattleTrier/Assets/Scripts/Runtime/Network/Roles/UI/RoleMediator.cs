using Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Services.Applications;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Connection.States;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Kulinaria.Tools.BattleTrier.Runtime.Network.Roles.UI
{
  public class RoleMediator : MonoBehaviour
  {
    [SerializeField] private RoleUi _roleUi;
    [SerializeField] private Button _quitButton;

    [Inject] private IConnectionStateMachine _connectionStateMachine;
    [Inject] private IApplicationService _applicationService;

    public void Initialize()
    {
      _roleUi.Initialize();
      _quitButton.onClick.AddListener(_applicationService.QuitApplication);
    }

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