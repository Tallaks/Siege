using Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Services;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Data;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Session;
using Unity.Netcode;

namespace Kulinaria.Tools.BattleTrier.Runtime.Network.Roles
{
  public class NetworkPlayerObject : NetworkBehaviour
  {
    public NetworkVariable<RoleState> State { get; set; } = new() { Value = RoleState.None };

    public override void OnNetworkSpawn()
    {
      var session = ServiceProvider.GetResolve<Session<SessionPlayerData>>();
      if (session.GetPlayerData(OwnerClientId).HasValue)
      {
        SessionPlayerData sessionPlayerData = session.GetPlayerData(OwnerClientId).Value;
        gameObject.name = sessionPlayerData.PlayerName;
        if (sessionPlayerData.RoleState != RoleState.None)
          State.Value = sessionPlayerData.RoleState;
      }
    }
  }
}