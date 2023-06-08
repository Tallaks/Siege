using Kulinaria.Tools.BattleTrier.Runtime.Network.Data;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Roles;
using Unity.Netcode;
using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Runtime.Network.Authentication
{
  public class PlayerService : NetworkBehaviour
  {
    [SerializeField] private FirstPlayerBehaviour _firstPlayerPrefab;
    [SerializeField] private SecondPlayerBehaviour _secondPlayerPrefab;

    [ServerRpc(RequireOwnership = false)]
    public void RegisterPlayerServerRpc(Role role)
    {
      switch (role)
      {
        case Role.FirstPlayer:
          FirstPlayerBehaviour firstPlayer = Instantiate(_firstPlayerPrefab);
          firstPlayer.NetworkObject.Spawn();
          break;
        case Role.SecondPlayer:
          SecondPlayerBehaviour secondPlayer = Instantiate(_secondPlayerPrefab);
          secondPlayer.NetworkObject.Spawn();
          break;
      }
    }

    /// <summary>
    /// Gets called when the <see cref="NetworkObject"/> gets spawned, message handlers are ready to be registered and the network is setup.
    /// </summary>
    public override void OnNetworkSpawn()
    {
      base.OnNetworkSpawn();
      Debug.Log("Player Service Spawn");
    }
  }
}