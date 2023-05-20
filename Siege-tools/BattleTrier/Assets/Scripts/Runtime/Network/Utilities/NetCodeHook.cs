using System;
using Unity.Netcode;
using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Runtime.Network.Utilities
{
  public class NetCodeHook : NetworkBehaviour
  {
    public override void OnNetworkDespawn()
    {
      base.OnNetworkDespawn();
      OnNetworkDeSpawnHook?.Invoke();
    }

    public override void OnNetworkSpawn()
    {
      base.OnNetworkSpawn();
      OnNetworkSpawnHook?.Invoke();
      Debug.Log("Hook spawned");
    }

    public event Action OnNetworkSpawnHook;

    public event Action OnNetworkDeSpawnHook;
  }
}