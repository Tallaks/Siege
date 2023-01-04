using Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Services.Inputs;
using Unity.Netcode;
using UnityEngine;
using Zenject;

namespace Kulinaria.Tools.BattleTrier.Runtime.Level
{
  public class PlayerMover : NetworkBehaviour
  {
    [Inject] private IInputService _inputService;

    private IInputService InputService => _inputService ?? FindObjectOfType<OldInputService>();

    private void Update()
    {
      if (IsOwner)
        transform.position += (Vector3)InputService.MoveDirection;
    }
  }
}