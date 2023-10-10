using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Network;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Placer;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Selection.Placement;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Maps;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.UI;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Roles;
using Unity.Netcode;
using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.States
{
  public class PlacingSecondPlayerCharactersState : ParameterlessState
  {
    private readonly IPlacementSelection _placementSelection;
    private readonly GameplayMediator _mediator;
    private readonly NetworkManager _networkManager;
    private readonly MapNetwork _mapNetwork;
    private readonly CharacterRegistryNetwork _characterRegistryNetwork;
    private readonly ICharacterPlacer _characterPlacer;

    public PlacingSecondPlayerCharactersState(
      IPlacementSelection placementSelection,
      GameplayMediator mediator,
      NetworkManager networkManager,
      MapNetwork mapNetwork,
      CharacterRegistryNetwork characterRegistryNetwork,
      ICharacterPlacer characterPlacer)
    {
      _placementSelection = placementSelection;
      _mediator = mediator;
      _networkManager = networkManager;
      _mapNetwork = mapNetwork;
      _characterRegistryNetwork = characterRegistryNetwork;
      _characterPlacer = characterPlacer;
    }

    public override void Enter()
    {
      switch (_networkManager.LocalClient.PlayerObject.GetComponent<NetworkPlayerObject>().State.Value)
      {
        case RoleState.ChosenFirst:
          _mediator.HidePlacementActivePlayerUi();
          _mediator.ShowPlacementWaitingPlayerUi();
          break;
        case RoleState.ChosenSecond:
          _mediator.HidePlacementWaitingUi();
          _mediator.ShowPlacementActivePlayerUi();
          _mediator.UpdatePlacementList();
          _characterPlacer.PlaceEnemiesOnTheirPositions();
          _characterRegistryNetwork.SecondPlayerCharacters.OnListChanged += OnSecondPlayerCharactersPlaced;
          break;
        default:
          _mediator.ShowPlacementSpectatorUi();
          break;
      }
    }

    public override void Exit()
    {
      if (_networkManager.LocalClient.PlayerObject.GetComponent<NetworkPlayerObject>().State.Value == RoleState.ChosenSecond)
        _characterRegistryNetwork.SecondPlayerCharacters.OnListChanged -= OnSecondPlayerCharactersPlaced;
      else
        _characterPlacer.PlaceEnemiesOnTheirPositions();
      _mediator.HideAllPlacementUi();
      _mapNetwork.Refresh();
      _placementSelection.UnselectCharacter();
    }

    private void OnSecondPlayerCharactersPlaced(NetworkListEvent<CharacterNetworkData> changeEvent)
    {
      Debug.Log("OnSecondPlayerCharactersPlaced");
      for (var i = 0; i < _characterRegistryNetwork.SecondPlayerCharacters.Count; i++)
        if (_characterRegistryNetwork.SecondPlayerCharacters[i].TilePosition == Vector2.one * -100)
        {
          _mediator.UpdatePlacementList();
          _characterPlacer.PlaceEnemiesOnTheirPositions();
          return;
        }

      _mediator.UpdatePlacementList();
      _mediator.ShowSubmitButton();
    }
  }
}