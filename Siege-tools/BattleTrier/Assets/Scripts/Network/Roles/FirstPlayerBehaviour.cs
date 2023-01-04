using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Network.Roles
{
  public class FirstPlayerBehaviour : RoleBehaviour
  {
    /// <inheritdoc />
    /// <summary>
    /// Gets called when the <see cref="T:Unity.Netcode.NetworkObject" /> gets spawned, message handlers are ready to be registered and the network is setup.
    /// </summary>
    public override void OnNetworkSpawn()
    {
      base.OnNetworkSpawn();
      Debug.Log("First player spawned");
    }
  }
}