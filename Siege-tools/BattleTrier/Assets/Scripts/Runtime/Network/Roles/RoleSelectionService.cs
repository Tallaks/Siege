using System;
using Unity.Netcode;
using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Runtime.Network.Roles
{
  public class RoleSelectionService : NetworkBehaviour
  {
    public NetworkVariable<bool> LobbyIsClosed { get; } = new();
    public NetworkList<PlayerRoleState> PlayerRoles;

    private void Awake() =>
      PlayerRoles = new NetworkList<PlayerRoleState>();

    [ServerRpc(RequireOwnership = false)]
    public void ChangeSeatServerRpc(ulong clientId, int seatIdx)
    {
      Debug.Log("ChangeSeatServerRpc");
      OnClientChoseRole?.Invoke(clientId, seatIdx);
    }

    public event Action<ulong, int> OnClientChoseRole;
  }
}