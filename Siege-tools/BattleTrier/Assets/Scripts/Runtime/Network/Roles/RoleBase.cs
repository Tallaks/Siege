using Unity.Netcode;

namespace Kulinaria.Tools.BattleTrier.Runtime.Network.Roles
{
  public class RoleBase : NetworkBehaviour
  {
    public NetworkVariable<RoleState> State { get; set; } = new() { Value = RoleState.None };
  }
}