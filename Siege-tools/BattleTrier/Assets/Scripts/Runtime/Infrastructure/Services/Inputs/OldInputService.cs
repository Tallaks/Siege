using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Services.Inputs
{
  public class OldInputService : MonoBehaviour, IInputService
  {
    private void Update()
    {
      MoveDirection = Vector2.zero;
      if (Input.GetKeyDown(KeyCode.W)) MoveDirection = new Vector2(0, 1);
      if (Input.GetKeyDown(KeyCode.S)) MoveDirection = new Vector2(0, -1);
      if (Input.GetKeyDown(KeyCode.A)) MoveDirection = new Vector2(-1, 0);
      if (Input.GetKeyDown(KeyCode.D)) MoveDirection = new Vector2(1, 0);
    }

    public Vector2 MoveDirection { get; private set; }
  }
}