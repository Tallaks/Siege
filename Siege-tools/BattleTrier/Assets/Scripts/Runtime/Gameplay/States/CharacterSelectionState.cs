using System.Collections.Generic;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Network;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Registry;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.UI;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Roles;
using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.States
{
  public class CharacterSelectionState : ParameterlessState
  {
    private readonly CharacterRegistryNetwork _characterRegistryNetwork;
    private readonly ICharacterRegistry _characterRegistry;
    private readonly GameplayMediator _mediator;
    private readonly RoleBase _role;

    public CharacterSelectionState(
      ICharacterRegistry characterRegistry,
      CharacterRegistryNetwork characterRegistryNetwork,
      RoleBase role,
      GameplayMediator mediator)
    {
      _characterRegistry = characterRegistry;
      _characterRegistryNetwork = characterRegistryNetwork;
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

      foreach (KeyValuePair<int, int> characterGroup in _characterRegistry.Characters)
        for (var i = 0; i < characterGroup.Value; i++)
          _characterRegistryNetwork.RegisterByIdServerRpc(characterGroup.Key, _role.State.Value);
    }
  }
}