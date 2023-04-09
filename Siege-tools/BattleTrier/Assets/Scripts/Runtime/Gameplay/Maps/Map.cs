using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Maps.Data;
using Unity.Netcode;
using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Maps
{
  public class Map : NetworkBehaviour
  {
    [SerializeField] private Tile _tilePrefab;

    public void SpawnTiles(TileType[,] mapTiles)
    {
      Debug.Log("SpawnTiles");
      for(var col = 0; col < mapTiles.GetLength(0); col++)
      for (var row = 0; row < mapTiles.GetLength(1); row++)
      {
        if (mapTiles[col, row] == TileType.Default)
        {
          Tile tile = Instantiate(_tilePrefab, new Vector3(-7.5f, -4, 0) + new Vector3(col, row) * 0.7f, Quaternion.identity);
          tile.NetworkObject.Spawn();
          tile.NetworkObject.TrySetParent(NetworkObject);
        }
      }
    }
  }
}