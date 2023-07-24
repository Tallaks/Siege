using Kulinaria.Tools.BattleTrier.Runtime.Network.Roles;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Session;

namespace Kulinaria.Tools.BattleTrier.Runtime.Network.Data
{
  public struct SessionPlayerData : ISessionPlayerData
  {
    public bool IsConnected { get; set; }
    public ulong ClientID { get; set; }

    public RoleState RoleState { get; set; }

    /// Instead of using a NetworkGuid (two ulongs) we could just use an int or even a byte-sized index into an array of possible avatars defined in our game data source
    public NetworkGuid AvatarNetworkGuid;

    public bool HasCharacterSpawned;
    public string PlayerName;
    public int PlayerNumber;

    public SessionPlayerData(ulong clientID, string name, NetworkGuid avatarNetworkGuid, bool isConnected = false,
      bool hasCharacterSpawned = false)
    {
      ClientID = clientID;
      PlayerName = name;
      PlayerNumber = -1;
      AvatarNetworkGuid = avatarNetworkGuid;
      IsConnected = isConnected;
      HasCharacterSpawned = hasCharacterSpawned;
      RoleState = RoleState.None;
    }

    public void Reinitialize() =>
      HasCharacterSpawned = false;
  }
}