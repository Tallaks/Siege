using System.Collections.Generic;
using System.Linq;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Data;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Factory;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Network;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Placer;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Selection.Placement;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Maps.Data;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.UI;
using Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Services;
using Unity.Netcode;
using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Maps
{
  public class MapNetwork : NetworkBehaviour
  {
    private readonly List<Tile> _tiles = new();
    [SerializeField] private Tile _tilePrefab;

    private GameplayMediator Mediator =>
      _mediator == null ? _mediator = FindObjectOfType<GameplayMediator>() : _mediator;

    private ICharacterFactory _characterFactory;

    private BoardConfig _config;
    private int[,] _mapBoard;

    private GameplayMediator _mediator;
    private CharacterRegistryNetwork _networkRegistry;
    private IPlacementSelection _placementSelection;
    private ICharacterPlacer _placer;

    [ServerRpc(RequireOwnership = false)]
    public void InitMapBoardServerRpc()
    {
      TileType[,] mapTiles = _config.MapTiles;
      _mapBoard = new int[mapTiles.GetLength(0), mapTiles.GetLength(1)];

      for (var col = 0; col < mapTiles.GetLength(0); col++)
      for (var row = 0; row < mapTiles.GetLength(1); row++)
        _mapBoard[col, row] = mapTiles[col, row] switch
        {
          TileType.None => -1,
          TileType.Default => 0,
          TileType.Obstacle => 1,
          TileType.WeakCover => 2,
          TileType.StrongCover => 3,
          _ => _mapBoard[col, row]
        };
    }

    [ClientRpc]
    public void SpawnTilesClientRpc(string configName)
    {
      _characterFactory = ServiceProvider.ResolveFromOfflineInstaller<ICharacterFactory>();
      _placementSelection = ServiceProvider.ResolveFromOfflineInstaller<IPlacementSelection>();
      _networkRegistry = ServiceProvider.ResolveFromOnlineInstaller<CharacterRegistryNetwork>();
      _placer = ServiceProvider.ResolveFromOfflineInstaller<ICharacterPlacer>();
      _config = Resources.Load<BoardConfig>("Configs/Boards/" + configName);
      TileType[,] mapTiles = _config.MapTiles;
      Debug.Log("Spawn Tiles");
      InstantiateTiles(mapTiles);
      AssignTilesWithNeighbours();
    }

    public void OnTileSelected(Tile selectedTile)
    {
      if (Mediator.CharacterPlacementUiIsActive)
      {
        Refresh();
        selectedTile.ChangeToSelectedColor();
        Debug.Log($"Tile with coords {selectedTile.Coords.x}; {selectedTile.Coords.y} was clicked");
      }
    }

    public void PlacePlayerOn(Tile tileToPlace, CharacterConfig selectedPlayerConfig) =>
      _placer.PlaceNewCharacterOnTile(tileToPlace, selectedPlayerConfig);

    private void Refresh()
    {
      foreach (Tile tile in _tiles.Where(k => !k.Occupied))
        tile.ChangeToUnselectedColor();
    }

    private void AssignTilesWithNeighbours()
    {
      foreach (Tile tile in _tiles)
      foreach (Tile otherTile in _tiles)
      {
        if (otherTile == tile)
          continue;
        if (otherTile.Coords.x <= tile.Coords.x + 1 &&
            otherTile.Coords.x >= tile.Coords.x - 1 &&
            otherTile.Coords.y <= tile.Coords.y + 1 &&
            otherTile.Coords.y >= tile.Coords.y - 1)
          tile.AddNeighbour(otherTile);
      }
    }

    private void InstantiateTiles(TileType[,] mapTiles)
    {
      for (var col = 0; col < mapTiles.GetLength(0); col++)
      for (var row = 0; row < mapTiles.GetLength(1); row++)
        if (mapTiles[col, row] == TileType.Default)
        {
          Tile tile = Instantiate(_tilePrefab, new Vector3(-4f, -1.5f, 0) + new Vector3(col, row) * 0.7f,
            Quaternion.identity);
          tile.Initialize(col, row, this);
          _tiles.Add(tile);
        }
    }
  }
}