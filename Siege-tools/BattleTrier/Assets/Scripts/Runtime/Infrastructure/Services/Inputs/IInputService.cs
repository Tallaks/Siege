using System;
using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Services.Inputs
{
  public interface IInputService
  {
    public Action OnConsoleCalled { get; set; }
    public Vector2 MoveDirection { get; }
  }
}