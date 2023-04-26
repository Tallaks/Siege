using System;
using Unity.Netcode;
using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Runtime.Network.Utilities
{
  public class NetCodeHook : NetworkBehaviour
  {
    public event Action OnNetworkSpawnHook;

    public event Action OnNetworkDeSpawnHook;

    public override void OnNetworkSpawn()
    {
      base.OnNetworkSpawn();
      OnNetworkSpawnHook?.Invoke();
      Debug.Log("Hook spawned");
    }

    public override void OnNetworkDespawn()
    {
      base.OnNetworkDespawn();
      OnNetworkDeSpawnHook?.Invoke();
    }
  }
}