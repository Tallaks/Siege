using Unity.Netcode;
using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Maps
{
  public class Tile : MonoBehaviour
  {
    private MapNetwork _mapNetwork;
    private Vector2Int _coords;

    private void OnMouseDown()
    {
      _mapNetwork.OnTileClickedServerRpc(NetworkManager.Singleton.LocalClient.ClientId, _coords.x, _coords.y);
      Debug.Log($"Tile with coords {_coords.x}; {_coords.y} was clicked");
    }

    public void Initialize(int col, int row, MapNetwork mapNetwork)
    {
      _mapNetwork = mapNetwork;
      _coords = new Vector2Int(col, row);
    }
  }
}