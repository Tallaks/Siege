using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Network;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Placer;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.UI;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Roles;
using Unity.Netcode;
using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.States
{
  public class PlacingSecondPlayerCharactersState : ParameterlessState
  {
    private readonly GameplayMediator _mediator;
    private readonly RoleBase _roleBase;
    private readonly CharacterRegistryNetwork _characterRegistryNetwork;
    private readonly ICharacterPlacer _characterPlacer;

    public PlacingSecondPlayerCharactersState(GameplayMediator mediator, RoleBase roleBase,
      CharacterRegistryNetwork characterRegistryNetwork, ICharacterPlacer characterPlacer)
    {
      _mediator = mediator;
      _roleBase = roleBase;
      _characterRegistryNetwork = characterRegistryNetwork;
      _characterPlacer = characterPlacer;
    }

    public override void Enter()
    {
      switch (_roleBase.State.Value)
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
    }

    private void OnSecondPlayerCharactersPlaced(NetworkListEvent<CharacterNetworkData> changeevent)
    {
      Debug.Log("OnFirstPlayerCharactersPlaced");
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