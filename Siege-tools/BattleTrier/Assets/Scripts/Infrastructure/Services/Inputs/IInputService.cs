using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Infrastructure.Services.Inputs
{
  public interface IInputService
  {
    public Vector2 MoveDirection { get; }
  }
}