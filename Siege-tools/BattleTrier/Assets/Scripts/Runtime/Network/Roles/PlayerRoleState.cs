using System;
using Unity.Netcode;

namespace Kulinaria.Tools.BattleTrier.Runtime.Network.Roles
{
  public struct PlayerRoleState : INetworkSerializable, IEquatable<PlayerRoleState>
  {
    public ulong ClientId;
    public float LastChangeTime;
    public RoleState State;

    public PlayerRoleState(ulong clientId, RoleState state, float lastChangeTime = 0)
    {
      LastChangeTime = lastChangeTime;
      ClientId = clientId;
      State = state;
    }

    public bool Equals(PlayerRoleState other) =>
      other.State == State &&
      other.ClientId == ClientId &&
      LastChangeTime.Equals(other.LastChangeTime);

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
      serializer.SerializeValue(ref ClientId);
      serializer.SerializeValue(ref State);
      serializer.SerializeValue(ref LastChangeTime);
    }
  }
}