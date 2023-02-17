using System;
using Unity.Netcode;

namespace Kulinaria.Tools.BattleTrier.Runtime.Network.Roles
{
  public struct PlayerRoleState : INetworkSerializable, IEquatable<PlayerRoleState>
  {
    public ulong ClientId;
    public int RoleId;
    public RoleState State;
    public float LastChangeTime;

    public PlayerRoleState(ulong clientId, RoleState state, int roleId = -1, float lastChangeTime = 0)
    {
      LastChangeTime = lastChangeTime;
      ClientId = clientId;
      RoleId = roleId;
      State = state;
    }

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
      serializer.SerializeValue(ref ClientId);
      serializer.SerializeValue(ref RoleId);
      serializer.SerializeValue(ref State);
      serializer.SerializeValue(ref LastChangeTime);
    }

    public bool Equals(PlayerRoleState other) =>
      other.State == State &&
      other.ClientId == ClientId &&
      other.RoleId == RoleId &&
      LastChangeTime.Equals(other.LastChangeTime);
  }
}