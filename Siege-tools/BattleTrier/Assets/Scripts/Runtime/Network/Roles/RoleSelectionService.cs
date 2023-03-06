using System;
using Unity.Netcode;
using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Runtime.Network.Roles
{
  public class RoleSelectionService : NetworkBehaviour
  {
    public event Action<ulong, int> OnClientChoseRole;
    public NetworkVariable<bool> LobbyIsClosed { get; } = new();
    public NetworkList<PlayerRoleState> PlayerRoles;

    private void Awake() => 
      PlayerRoles = new NetworkList<PlayerRoleState>();

    public int IndexOfClient(ulong clientId)
    {
      for (var i = 0; i < PlayerRoles.Count; i++)
      {
        if (PlayerRoles[i].ClientId == clientId)
          return i;
      }

      return -1;
    }

    [ServerRpc(RequireOwnership = false)]
    public void ChangeSeatServerRpc(ulong clientId, int seatIdx)
    {
      Debug.Log("ChangeSeatServerRpc");
      OnClientChoseRole?.Invoke(clientId, seatIdx);
    }
  }
}