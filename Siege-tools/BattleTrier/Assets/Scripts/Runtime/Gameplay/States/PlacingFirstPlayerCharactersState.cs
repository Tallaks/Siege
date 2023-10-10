using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Network;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Selection.Placement;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Maps;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.UI;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Roles;
using Unity.Netcode;
using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.States
{
  public class PlacingFirstPlayerCharactersState : ParameterlessState
  {
    private readonly IPlacementSelection _placementSelection;
    private readonly GameplayMediator _mediator;
    private readonly MapNetwork _mapNetwork;
    private readonly CharacterRegistryNetwork _characterRegistryNetwork;
    private readonly NetworkManager _networkManager;

    public PlacingFirstPlayerCharactersState(
      IPlacementSelection placementSelection,
      GameplayMediator mediator,
      NetworkManager networkManager,
      MapNetwork mapNetwork,
      CharacterRegistryNetwork characterRegistryNetwork)
    {
      _placementSelection = placementSelection;
      _mediator = mediator;
      _networkManager = networkManager;
      _mapNetwork = mapNetwork;
      _characterRegistryNetwork = characterRegistryNetwork;
    }

    public override void Enter()
    {
      switch (_networkManager.LocalClient.PlayerObject.GetComponent<NetworkPlayerObject>().State.Value)
      {
        case RoleState.ChosenFirst:
          _mediator.ShowPlacementActivePlayerUi();
          _mediator.UpdatePlacementList();
          _characterRegistryNetwork.FirstPlayerCharacters.OnListChanged += OnFirstPlayerCharactersPlaced;
          break;
        case RoleState.ChosenSecond:
          _mediator.ShowPlacementWaitingPlayerUi();
          break;
        default:
          _mediator.ShowPlacementSpectatorUi();
          break;
      }
    }

    public override void Exit()
    {
      _characterRegistryNetwork.FirstPlayerCharacters.OnListChanged -= OnFirstPlayerCharactersPlaced;
      _mapNetwork.Refresh();
      _placementSelection.UnselectCharacter();
    }

    private void OnFirstPlayerCharactersPlaced(NetworkListEvent<CharacterNetworkData> changeEvent)
    {
      Debug.Log("OnFirstPlayerCharactersPlaced");
      for (var i = 0; i < _characterRegistryNetwork.FirstPlayerCharacters.Count; i++)
        if (_characterRegistryNetwork.FirstPlayerCharacters[i].TilePosition == Vector2.one * -100)
        {
          _mediator.UpdatePlacementList();
          return;
        }

      _mediator.UpdatePlacementList();
      _mediator.ShowSubmitButton();
    }
  }
}