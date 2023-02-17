using System;
using Unity.Netcode;

namespace Kulinaria.Tools.BattleTrier.Runtime.Network.Roles
{
  public class RoleSelectionService : NetworkBehaviour
  {
    private NetworkList<PlayerRoleState> _playerRoles;

    public event Action<ulong, int, bool> OnClientChangedRole;
    public NetworkVariable<bool> LobbyIsClosed { get; } = new();
    public NetworkList<PlayerRoleState> PlayerRoles => _playerRoles;

    private void Awake() => 
      _playerRoles = new NetworkList<PlayerRoleState>();
    
    [ServerRpc(RequireOwnership = false)]
    public void ChangeSeatServerRpc(ulong clientId, int seatIdx, bool lockedIn) => 
      OnClientChangedRole?.Invoke(clientId, seatIdx, lockedIn);
  }
}