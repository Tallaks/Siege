using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Data;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Factory;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Network;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Selection.Placement;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Maps;
using Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Services.Data;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Roles;
using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Placer
{
  public class CharacterPlacer : ICharacterPlacer
  {
    private readonly IStaticDataProvider _staticDataProvider;
    private readonly ICharacterFactory _characterFactory;
    private readonly IPlacementSelection _placementSelection;
    private readonly RoleBase _roleBase;
    private readonly CharacterRegistryNetwork _characterRegistryNetwork;

    public CharacterPlacer(
      IStaticDataProvider staticDataProvider,
      ICharacterFactory characterFactory,
      IPlacementSelection placementSelection,
      RoleBase roleBase,
      CharacterRegistryNetwork characterRegistryNetwork)
    {
      _staticDataProvider = staticDataProvider;
      _characterFactory = characterFactory;
      _placementSelection = placementSelection;
      _roleBase = roleBase;
      _characterRegistryNetwork = characterRegistryNetwork;
    }

    public void PlaceNewCharacterOnTile(Tile tileToPlace, CharacterConfig selectedPlayerConfig)
    {
      tileToPlace.OccupyBy(_characterFactory.Create(selectedPlayerConfig.Id, tileToPlace.Coords));
      if (_roleBase.State.Value == RoleState.ChosenFirst)
      {
        for (var i = 0; i < _characterRegistryNetwork.FirstPlayerCharacters.Count; i++)
          if (_characterRegistryNetwork.FirstPlayerCharacters[i].TypeId == selectedPlayerConfig.Id &&
              _characterRegistryNetwork.FirstPlayerCharacters[i].TilePosition == new Vector2(-100, -100))
          {
            _characterRegistryNetwork.FirstPlayerCharacters[i] = new CharacterNetworkData(
              selectedPlayerConfig.Id,
              _characterRegistryNetwork.FirstPlayerCharacters[i].InstanceId,
              RoleState.ChosenFirst,
              tileToPlace.Coords,
              _staticDataProvider);
            break;
          }
      }
      else if (_roleBase.State.Value == RoleState.ChosenSecond)
      {
        for (var i = 0; i < _characterRegistryNetwork.SecondPlayerCharacters.Count; i++)
          if (_characterRegistryNetwork.SecondPlayerCharacters[i].TypeId == selectedPlayerConfig.Id &&
              _characterRegistryNetwork.SecondPlayerCharacters[i].TilePosition == Vector2.positiveInfinity)
          {
            _characterRegistryNetwork.SecondPlayerCharacters[i] = new CharacterNetworkData(
              selectedPlayerConfig.Id,
              _characterRegistryNetwork.SecondPlayerCharacters[i].InstanceId,
              RoleState.ChosenSecond,
              tileToPlace.Coords,
              _staticDataProvider);
            break;
          }
      }

      _placementSelection.Unselect();
    }
  }
}