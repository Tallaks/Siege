using System.Linq;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Data;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Factory;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Network;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Registry;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Selection.Placement;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Maps;
using Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Services.Data;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Roles;
using Unity.Netcode;
using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Placer
{
  public class CharacterPlacer : ICharacterPlacer
  {
    private readonly ICharacterFactory _characterFactory;
    private readonly IPlacementSelection _placementSelection;
    private readonly CharacterRegistryNetwork _characterRegistryNetwork;
    private readonly MapNetwork _mapNetwork;
    private readonly IEnemyFactory _enemyFactory;
    private readonly ICharacterRegistry _characterRegistry;
    private readonly IStaticDataProvider _staticDataProvider;
    private readonly NetworkManager _networkManager;
    
    private RoleState Role => _networkManager.LocalClient.PlayerObject.GetComponent<NetworkPlayerObject>().State.Value;

    public CharacterPlacer(
      ICharacterFactory characterFactory,
      ICharacterRegistry characterRegistry,
      IStaticDataProvider staticDataProvider,
      IEnemyFactory enemyFactory,
      IPlacementSelection placementSelection,
      CharacterRegistryNetwork characterRegistryNetwork,
      MapNetwork mapNetwork,
      NetworkManager networkManager)
    {
      _characterFactory = characterFactory;
      _characterRegistry = characterRegistry;
      _staticDataProvider = staticDataProvider;
      _enemyFactory = enemyFactory;
      _placementSelection = placementSelection;
      _characterRegistryNetwork = characterRegistryNetwork;
      _mapNetwork = mapNetwork;
      _networkManager = networkManager;
    }

    public void PlaceNewCharacterOnTile(Tile tileToPlace, CharacterConfig selectedPlayerConfig)
    {
      Debug.Log($"Placing new character with type id {selectedPlayerConfig.Id} on tile {tileToPlace.Coords}");
      Character character = _characterFactory.Create(selectedPlayerConfig.Id);
      tileToPlace.OccupyBy(character);
      character.MoveTo(tileToPlace.Coords);
      if (Role == RoleState.ChosenFirst)
      {
        for (var i = 0; i < _characterRegistryNetwork.FirstPlayerCharacters.Count; i++)
          if (_characterRegistryNetwork.FirstPlayerCharacters[i].TypeId == selectedPlayerConfig.Id &&
              _characterRegistryNetwork.FirstPlayerCharacters[i].TilePosition == new Vector2(-100, -100))
          {
            _characterRegistryNetwork.ChangeCharacterPositionServerRpc(
              tileToPlace.Coords,
              _characterRegistryNetwork.FirstPlayerCharacters[i].InstanceId,
              Role);
            character.Id = _characterRegistryNetwork.FirstPlayerCharacters[i].InstanceId;
            character.Name = _staticDataProvider.ConfigById(_characterRegistryNetwork.FirstPlayerCharacters[i].TypeId)
              .Name;
            _characterRegistry.AddCharacter(character);
            break;
          }
      }
      else if (Role == RoleState.ChosenSecond)
      {
        for (var i = 0; i < _characterRegistryNetwork.SecondPlayerCharacters.Count; i++)
          if (_characterRegistryNetwork.SecondPlayerCharacters[i].TypeId == selectedPlayerConfig.Id &&
              _characterRegistryNetwork.SecondPlayerCharacters[i].TilePosition == new Vector2(-100, -100))
          {
            _characterRegistryNetwork.ChangeCharacterPositionServerRpc(
              tileToPlace.Coords,
              _characterRegistryNetwork.SecondPlayerCharacters[i].InstanceId,
              Role);
            character.Id = _characterRegistryNetwork.SecondPlayerCharacters[i].InstanceId;
            character.Name = _staticDataProvider.ConfigById(_characterRegistryNetwork.SecondPlayerCharacters[i].TypeId)
              .Name;

            _characterRegistry.AddCharacter(character);
            break;
          }
      }

      _placementSelection.UnselectConfig();
    }

    public void PlaceEnemiesOnTheirPositions()
    {
      Debug.Log("Placing enemies on their positions");
      if (Role == RoleState.ChosenSecond)
        for (var i = 0; i < _characterRegistryNetwork.FirstPlayerCharacters.Count; i++)
        {
          Tile tile = _mapNetwork.Tiles
            .First(k => k.Coords == _characterRegistryNetwork.FirstPlayerCharacters[i].TilePosition);
          if (tile.IsOccupied)
            continue;
          Character enemy = _enemyFactory.Create(_characterRegistryNetwork.FirstPlayerCharacters[i].TypeId).Character;
          enemy.Id = _characterRegistryNetwork.FirstPlayerCharacters[i].InstanceId;
          enemy.Name = _staticDataProvider.ConfigById(_characterRegistryNetwork.FirstPlayerCharacters[i].TypeId).Name;
          tile.OccupyBy(enemy);
          enemy.MoveTo(tile.Coords);
          _characterRegistry.AddEnemy(enemy.GetComponent<Enemy>());
        }
      else
        for (var i = 0; i < _characterRegistryNetwork.SecondPlayerCharacters.Count; i++)
        {
          if (_characterRegistryNetwork.SecondPlayerCharacters[i].TilePosition == Vector2.one * -100)
            continue;
          Tile tile = _mapNetwork.Tiles
            .First(k => k.Coords == _characterRegistryNetwork.SecondPlayerCharacters[i].TilePosition);
          if (tile.IsOccupied)
            continue;
          Character enemy = _enemyFactory.Create(_characterRegistryNetwork.SecondPlayerCharacters[i].TypeId).Character;
          enemy.Id = _characterRegistryNetwork.SecondPlayerCharacters[i].InstanceId;
          enemy.Name = _staticDataProvider.ConfigById(_characterRegistryNetwork.SecondPlayerCharacters[i].TypeId).Name;
          tile.OccupyBy(enemy);
          enemy.MoveTo(tile.Coords);
          _characterRegistry.AddEnemy(enemy.GetComponent<Enemy>());
        }
    }

    public void PlaceExistingCharacterOnTile(Tile newTile, Character character)
    {
      newTile.OccupyBy(character);
      character.MoveTo(newTile.Coords);
      _characterRegistryNetwork.ChangeCharacterPositionServerRpc(newTile.Coords, character.Id, Role);
    }
  }
}