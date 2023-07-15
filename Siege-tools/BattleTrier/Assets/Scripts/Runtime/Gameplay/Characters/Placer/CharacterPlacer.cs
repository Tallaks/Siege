using System.Linq;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Data;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Factory;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Network;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Selection.Placement;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Maps;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Roles;
using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Placer
{
  public class CharacterPlacer : ICharacterPlacer
  {
    private readonly ICharacterFactory _characterFactory;
    private readonly IPlacementSelection _placementSelection;
    private readonly RoleBase _roleBase;
    private readonly CharacterRegistryNetwork _characterRegistryNetwork;
    private readonly MapNetwork _mapNetwork;
    private readonly IEnemyFactory _enemyFactory;

    public CharacterPlacer(ICharacterFactory characterFactory,
      IEnemyFactory enemyFactory,
      IPlacementSelection placementSelection,
      RoleBase roleBase,
      CharacterRegistryNetwork characterRegistryNetwork,
      MapNetwork mapNetwork)
    {
      _characterFactory = characterFactory;
      _enemyFactory = enemyFactory;
      _placementSelection = placementSelection;
      _roleBase = roleBase;
      _characterRegistryNetwork = characterRegistryNetwork;
      _mapNetwork = mapNetwork;
    }

    public void PlaceNewCharacterOnTile(Tile tileToPlace, CharacterConfig selectedPlayerConfig)
    {
      Debug.Log($"Placing new character with type id {selectedPlayerConfig.Id} on tile {tileToPlace.Coords}");
      Character character = _characterFactory.Create(selectedPlayerConfig.Id);
      tileToPlace.OccupyBy(character);
      character.MoveTo(tileToPlace.Coords);
      if (_roleBase.State.Value == RoleState.ChosenFirst)
      {
        for (var i = 0; i < _characterRegistryNetwork.FirstPlayerCharacters.Count; i++)
          if (_characterRegistryNetwork.FirstPlayerCharacters[i].TypeId == selectedPlayerConfig.Id &&
              _characterRegistryNetwork.FirstPlayerCharacters[i].TilePosition == new Vector2(-100, -100))
          {
            _characterRegistryNetwork.ChangeCharacterPositionServerRpc(
              tileToPlace.Coords,
              _characterRegistryNetwork.FirstPlayerCharacters[i].InstanceId,
              _roleBase.State.Value);
            character.Id = _characterRegistryNetwork.FirstPlayerCharacters[i].InstanceId;
            break;
          }
      }
      else if (_roleBase.State.Value == RoleState.ChosenSecond)
      {
        for (var i = 0; i < _characterRegistryNetwork.SecondPlayerCharacters.Count; i++)
          if (_characterRegistryNetwork.SecondPlayerCharacters[i].TypeId == selectedPlayerConfig.Id &&
              _characterRegistryNetwork.SecondPlayerCharacters[i].TilePosition == new Vector2(-100, -100))
          {
            _characterRegistryNetwork.ChangeCharacterPositionServerRpc(
              tileToPlace.Coords,
              _characterRegistryNetwork.SecondPlayerCharacters[i].InstanceId,
              _roleBase.State.Value);
            character.Id = _characterRegistryNetwork.SecondPlayerCharacters[i].InstanceId;
            break;
          }
      }

      _placementSelection.Unselect();
    }

    public void PlaceEnemiesOnTheirPositions()
    {
      Debug.Log("Placing enemies on their positions");
      if (_roleBase.State.Value == RoleState.ChosenSecond)
        for (var i = 0; i < _characterRegistryNetwork.FirstPlayerCharacters.Count; i++)
        {
          Tile tile = _mapNetwork.Tiles
            .First(k => k.Coords == _characterRegistryNetwork.FirstPlayerCharacters[i].TilePosition);
          if (tile.IsOccupied)
            continue;
          Character enemy = _enemyFactory.Create(_characterRegistryNetwork.FirstPlayerCharacters[i].TypeId).Character;
          tile.OccupyBy(enemy);
          enemy.MoveTo(tile.Coords);
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
          tile.OccupyBy(enemy);
          enemy.MoveTo(tile.Coords);
        }
    }

    public void PlaceExistingCharacterOnTile(Tile newTile, Character character)
    {
      newTile.OccupyBy(character);
      character.MoveTo(newTile.Coords);
      _characterRegistryNetwork.ChangeCharacterPositionServerRpc(newTile.Coords, character.Id, _roleBase.State.Value);
    }
  }
}