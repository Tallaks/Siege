using System;
using Unity.Netcode;

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
    }

    public override void OnNetworkDespawn()
    {
      base.OnNetworkDespawn();
      OnNetworkDeSpawnHook?.Invoke();
    }

    public override void OnDestroy()
    {
      base.OnDestroy();
      OnNetworkSpawnHook = null;
      OnNetworkDeSpawnHook = null;
    }
  }
}