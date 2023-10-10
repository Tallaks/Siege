using System.Collections.Generic;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Network;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Registry;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Selection.Placement;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.UI;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Roles;
using Unity.Netcode;
using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.States
{
  public class CharacterSelectionState : ParameterlessState
  {
    private readonly ICharacterRegistry _characterRegistry;
    private readonly IPlacementSelection _placementSelection;
    private readonly CharacterRegistryNetwork _characterRegistryNetwork;
    private readonly GameplayMediator _mediator;
    private readonly NetworkManager _networkManager;

    public CharacterSelectionState(
      ICharacterRegistry characterRegistry,
      IPlacementSelection placementSelection,
      NetworkManager networkManager,
      CharacterRegistryNetwork characterRegistryNetwork,
      GameplayMediator mediator)
    {
      _characterRegistry = characterRegistry;
      _placementSelection = placementSelection;
      _networkManager = networkManager;
      _characterRegistryNetwork = characterRegistryNetwork;
      _mediator = mediator;
    }

    public override void Enter()
    {
      Debug.Log("Entering character selection state");
      _mediator.InitializeCharacterSelectionUi();
    }

    public override void Exit()
    {
      Debug.Log("Exiting character selection state");
      _mediator.HideCharacterSelectionUi();
      _placementSelection.UnselectConfig();

      foreach (KeyValuePair<int, int> characterGroup in _characterRegistry.CharactersGroupsByConfigId)
        for (var i = 0; i < characterGroup.Value; i++)
          _characterRegistryNetwork.RegisterByIdServerRpc(characterGroup.Key,
            _networkManager.LocalClient.PlayerObject.GetComponent<NetworkPlayerObject>().State.Value);
    }
  }
}