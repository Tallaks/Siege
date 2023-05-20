using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Maps.Data;
using Unity.Netcode;
using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Maps
{
  public class MapNetwork : NetworkBehaviour
  {
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
        if (mapTiles[col, row] == TileType.Default)
          _mapBoard[col, row] = 0;
    }

    [ServerRpc(RequireOwnership = false)]
    public void OnTileClickedServerRpc(ulong clientId, int coordsX, int coordsY) =>
      Debug.Log($"Client with {clientId} clicked on coords {coordsX}; {coordsY}");

    [ClientRpc]
    public void SpawnTilesClientRpc(string configName)
    {
      _config = Resources.Load<BoardConfig>("Configs/Boards/" + configName);
      TileType[,] mapTiles = _config.MapTiles;
      Debug.Log("Spawn Tiles");
      for (var col = 0; col < mapTiles.GetLength(0); col++)
      for (var row = 0; row < mapTiles.GetLength(1); row++)
        if (mapTiles[col, row] == TileType.Default)
        {
          Tile tile = Instantiate(_tilePrefab, new Vector3(-4f, -1.5f, 0) + new Vector3(col, row) * 0.7f,
            Quaternion.identity);
          tile.Initialize(col, row, this);
        }
    }
  }
}