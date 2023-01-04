using Unity.Netcode;
using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Level.Board
{
  public class Tile : NetworkBehaviour
  {
    /// <summary>
    /// Gets called when the <see cref="NetworkObject"/> gets spawned, message handlers are ready to be registered and the network is setup.
    /// </summary>
    public override void OnNetworkSpawn()
    {
      base.OnNetworkSpawn();
      Debug.Log(name + " spawned");
    }

    private void OnMouseDown()
    {
      Debug.Log(name + " clicked");
    }
  }
}