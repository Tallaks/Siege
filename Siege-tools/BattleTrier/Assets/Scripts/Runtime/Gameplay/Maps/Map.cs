using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Maps.Data;
using Unity.Netcode;
using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Maps
{
  public class Map : NetworkBehaviour
  {
    [SerializeField] private Tile _tilePrefab;

    [ClientRpc]
    public void SpawnTilesClientRpc(string configName)
    {
      var config = Resources.Load<BoardConfig>("Configs/Boards/" + configName);
      TileType[,] mapTiles = config.MapTiles;
      Debug.Log("Spawn Tiles");
      for (var col = 0; col < mapTiles.GetLength(0); col++)
      for (var row = 0; row < mapTiles.GetLength(1); row++)
        if (mapTiles[col, row] == TileType.Default)
        {
          Tile tile = Instantiate(_tilePrefab, new Vector3(-4f, -1.5f, 0) + new Vector3(col, row) * 0.7f,
            Quaternion.identity);
        }
    }
  }
}