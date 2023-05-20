using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.UI;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Roles;
using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.States
{
  public class CharacterSelectionState : ParameterlessState
  {
    private readonly GameplayMediator _mediator;
    private readonly RoleBase _role;

    public CharacterSelectionState(
      RoleBase role,
      GameplayMediator mediator)
    {
      _role = role;
      _mediator = mediator;
    }

    public override void Enter()
    {
      Debug.Log("Entering character selection state");
      _mediator.InitializeCharacterSelectionUi(_role.State.Value);
    }

    public override void Exit()
    {
      Debug.Log("Exiting character selection state");
      _mediator.HideCharacterSelectionUi();
    }
  }
}