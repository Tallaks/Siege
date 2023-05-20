using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Maps
{
  public class Tile : MonoBehaviour
  {
    private Map _map;
    private Vector2Int _coords;

    public void Initialize(int col, int row, Map map)
    {
      _map = map;
      _coords = new Vector2Int(col, row);
    }
  }
}