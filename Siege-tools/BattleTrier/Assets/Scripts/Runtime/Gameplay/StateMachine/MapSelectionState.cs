using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.UI;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Roles;
using Unity.Netcode;
using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.StateMachine
{
  public class MapSelectionState : ParameterlessState
  {
    private GameplayMediator _mediator;
    private NetworkManager _networkManager;

    public MapSelectionState(NetworkManager networkManager, GameplayMediator mediator)
    {
      _networkManager = networkManager;
      _mediator = mediator;
    }

    public override void Enter()
    {
      Debug.Log("Entering Map Selection State");
      Debug.Log(_networkManager.LocalClient.PlayerObject.
        GetComponent<RoleBase>().State.Value);
      _mediator.InitializeMapSelectionUi();
    }

    public override void Exit()
    {
    }
  }
}