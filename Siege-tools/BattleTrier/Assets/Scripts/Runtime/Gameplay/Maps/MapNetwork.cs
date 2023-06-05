using System.Collections.Generic;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Maps.Data;
using Unity.Netcode;
using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Maps
{
  public class MapNetwork : NetworkBehaviour
  {
    private readonly List<Tile> _tiles = new();
    [SerializeField] private Tile _tilePrefab;
    private BoardConfig _config;

    private int[,] _mapBoard;

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

    public void Refresh()
    {
      foreach (Tile tile in _tiles)
        tile.ChangeColor(Color.white);
    }

    [ClientRpc]
    public void SpawnTilesClientRpc(string configName)
    {
      _config = Resources.Load<BoardConfig>("Configs/Boards/" + configName);
      TileType[,] mapTiles = _config.MapTiles;
      Debug.Log("Spawn Tiles");
      InstantiateTiles(mapTiles);
      AssignTilesWithNeighbours();
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