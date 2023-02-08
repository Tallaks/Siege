using System;
using Unity.Netcode;

namespace Kulinaria.Tools.BattleTrier.Runtime.Roles
{
  public struct PlayerRole : INetworkSerializable, IEquatable<PlayerRole>
  {
    public ulong ClientId;
    public SeatState SeatState;
    public int SeatIdx;

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
      serializer.SerializeValue(ref ClientId);
      serializer.SerializeValue(ref SeatState);
      serializer.SerializeValue(ref SeatIdx);
    }

    public bool Equals(PlayerRole other) => 
      ClientId == other.ClientId &&
      SeatState == other.SeatState &&
      SeatIdx == other.SeatIdx;
  }
}