using Unity.Netcode;

namespace Kulinaria.Tools.BattleTrier.Network.Roles
{
  public abstract class RoleBehaviour : NetworkBehaviour
  {
    public string PlayerId { get; private set; }

    public virtual void TakeRole(string playerId) => 
      PlayerId = playerId;
  }
}