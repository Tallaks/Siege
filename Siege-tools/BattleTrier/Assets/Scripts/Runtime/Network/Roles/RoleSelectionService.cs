using System;
using Unity.Netcode;

namespace Kulinaria.Tools.BattleTrier.Runtime.Network.Roles
{
  public class RoleSelectionService : NetworkBehaviour
  {
    private NetworkList<PlayerRoleState> _playerRoles;

    public event Action<ulong, int> OnClientChoseRole;
    public NetworkVariable<bool> LobbyIsClosed { get; } = new();
    public NetworkList<PlayerRoleState> PlayerRoles => _playerRoles;

    private void Awake() => 
      _playerRoles = new NetworkList<PlayerRoleState>();

    public int IndexOfClient(ulong clientId)
    {
      for (var i = 0; i < _playerRoles.Count; i++)
      {
        if(_playerRoles[i].ClientId == clientId)
          return i;
      }

      return -1;
    }
    
    [ServerRpc(RequireOwnership = false)]
    public void ChangeSeatServerRpc(ulong clientId, int seatIdx) => 
      OnClientChoseRole?.Invoke(clientId, seatIdx);
  }
}