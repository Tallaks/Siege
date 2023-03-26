using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.UI;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Gameplay;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Roles;
using Unity.Netcode;
using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.StateMachine
{
  public class CharacterSelectionState : ParameterlessState
  {
    private readonly StateMachine _stateMachine;
    private readonly NetworkManager _networkManager;
    private readonly GameplayMediator _mediator;
    private readonly CharacterSelectionNetwork _characterSelectionNetwork;

    public CharacterSelectionState(
      StateMachine stateMachine,
      NetworkManager networkManager,
      GameplayMediator mediator,
      CharacterSelectionNetwork characterSelectionNetwork)
    {
      _stateMachine = stateMachine;
      _networkManager = networkManager;
      _mediator = mediator;
      _characterSelectionNetwork = characterSelectionNetwork;
    }

    public override void Enter()
    {
      Debug.Log("Entering character selection state");
      _mediator.InitializeCharacterSelectionUi(
        _networkManager.LocalClient.PlayerObject.GetComponent<RoleBase>().State.Value, _characterSelectionNetwork);
    }

    public override void Exit()
    {
    }
  }
}