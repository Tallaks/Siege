using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters
{
  public class Character : MonoBehaviour
  {
    public int Id;
    public SpriteRenderer Renderer;
    public Vector2Int Position;

    public void MoveTo(Vector2Int newTileCoords)
    {
      Position = newTileCoords;
      transform.position = new Vector3(-4f, -1.5f, 0) + new Vector3(Position.x, Position.y) * 0.7f;
    }
  }
}