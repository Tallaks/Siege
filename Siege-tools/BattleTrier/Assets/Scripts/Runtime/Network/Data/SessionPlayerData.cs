using Kulinaria.Tools.BattleTrier.Runtime.Network.Session;
using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Runtime.Network.Data
{
  public struct SessionPlayerData : ISessionPlayerData
  {
    public bool IsConnected { get; set; }
    public ulong ClientID { get; set; }

    /// Instead of using a NetworkGuid (two ulongs) we could just use an int or even a byte-sized index into an array of possible avatars defined in our game data source
    public NetworkGuid AvatarNetworkGuid;

    public int CurrentHitPoints;
    public bool HasCharacterSpawned;
    public string PlayerName;
    public int PlayerNumber;
    public Vector3 PlayerPosition;
    public Quaternion PlayerRotation;

    public SessionPlayerData(ulong clientID, string name, NetworkGuid avatarNetworkGuid, int currentHitPoints = 0,
      bool isConnected = false, bool hasCharacterSpawned = false)
    {
      ClientID = clientID;
      PlayerName = name;
      PlayerNumber = -1;
      PlayerPosition = Vector3.zero;
      PlayerRotation = Quaternion.identity;
      AvatarNetworkGuid = avatarNetworkGuid;
      CurrentHitPoints = currentHitPoints;
      IsConnected = isConnected;
      HasCharacterSpawned = hasCharacterSpawned;
    }

    public void Reinitialize() =>
      HasCharacterSpawned = false;
  }
}