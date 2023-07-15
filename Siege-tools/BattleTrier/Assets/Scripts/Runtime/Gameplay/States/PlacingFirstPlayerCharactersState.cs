using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Network;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.UI;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Roles;
using Unity.Netcode;
using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.States
{
  public class PlacingFirstPlayerCharactersState : ParameterlessState
  {
    private readonly GameplayMediator _mediator;
    private readonly RoleBase _roleBase;
    private readonly CharacterRegistryNetwork _characterRegistryNetwork;

    public PlacingFirstPlayerCharactersState(GameplayMediator mediator, RoleBase roleBase,
      CharacterRegistryNetwork characterRegistryNetwork)
    {
      _mediator = mediator;
      _roleBase = roleBase;
      _characterRegistryNetwork = characterRegistryNetwork;
    }

    public override void Enter()
    {
      switch (_roleBase.State.Value)
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

    public override void Exit() =>
      _characterRegistryNetwork.FirstPlayerCharacters.OnListChanged -= OnFirstPlayerCharactersPlaced;

    private void OnFirstPlayerCharactersPlaced(NetworkListEvent<CharacterNetworkData> changeEvent)
    {
      Debug.Log("OnFirstPlayerCharactersPlaced");
      for (var i = 0; i < _characterRegistryNetwork.FirstPlayerCharacters.Count; i++)
        Debug.Log(_characterRegistryNetwork.FirstPlayerCharacters[i].TilePosition + " " +
                  _characterRegistryNetwork.FirstPlayerCharacters[i].InstanceId);

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