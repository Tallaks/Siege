using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Network;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.UI;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Roles;
using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.States
{
  public class CharacterSelectionState : ParameterlessState
  {
    private readonly StateMachine _stateMachine;
    private readonly RoleBase _role;
    private readonly GameplayMediator _mediator;
    private readonly CharacterSelectionNetwork _characterSelectionNetwork;

    public CharacterSelectionState(
      StateMachine stateMachine,
      RoleBase role,
      GameplayMediator mediator,
      CharacterSelectionNetwork characterSelectionNetwork)
    {
      _stateMachine = stateMachine;
      _role = role;
      _mediator = mediator;
      _characterSelectionNetwork = characterSelectionNetwork;
    }

    public override void Enter()
    {
      Debug.Log("Entering character selection state");
      _mediator.InitializeCharacterSelectionUi(_role.State.Value, _characterSelectionNetwork);
    }

    public override void Exit()
    {
    }
  }
}