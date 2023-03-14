using Kulinaria.Tools.BattleTrier.Runtime.Data;
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
    }
  }
}