using Kulinaria.Tools.BattleTrier.Runtime.Network.Roles;

namespace Kulinaria.Tools.BattleTrier.Runtime.Network.Session
{
  public interface ISessionPlayerData
  {
    bool IsConnected { get; set; }
    ulong ClientID { get; set; }
    RoleState RoleState { get; set; }
    void Reinitialize();
  }
}