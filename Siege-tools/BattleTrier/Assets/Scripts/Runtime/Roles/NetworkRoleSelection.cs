using System.Collections.Generic;
using Unity.Netcode;

namespace Kulinaria.Tools.BattleTrier.Runtime.Roles
{
  public class NetworkRoleSelection
  {
    public List<PlayerRole> PlayerRoles { get; set; }
    public NetworkVariable<bool> IsLobbyClosed { get; } = new();
  }
}