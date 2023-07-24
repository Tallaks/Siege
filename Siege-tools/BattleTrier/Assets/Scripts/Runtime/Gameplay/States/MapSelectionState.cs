using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.UI;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Data;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Roles;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Session;
using Unity.Netcode;
using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.States
{
  public class MapSelectionState : ParameterlessState
  {
    private readonly GameplayMediator _mediator;
    private readonly Session<SessionPlayerData> _session;
    private readonly NetworkManager _networkManager;

    private RoleState _role;

    public MapSelectionState(
      Session<SessionPlayerData> session,
      NetworkManager networkManager,
      GameplayMediator mediator)
    {
      _session = session;
      _networkManager = networkManager;
      _mediator = mediator;
    }

    public override void Enter()
    {
      Debug.Log("Entering Map Selection State");
      _role = _networkManager.LocalClient.PlayerObject.GetComponent<NetworkPlayerObject>().State.Value;
      _session.OnSessionStarted();
      _mediator.InitializeMapSelectionUi(_role);
    }

    public override void Exit() =>
      _mediator.HideMapSelectionUi();
  }
}