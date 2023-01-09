using Unity.Netcode;

namespace Kulinaria.Tools.BattleTrier.Runtime.Network.Data
{
  public class NetworkGuid : INetworkSerializable
  {
    public ulong FirstHalf;
    public ulong SecondHalf;

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
      serializer.SerializeValue(ref FirstHalf);
      serializer.SerializeValue(ref SecondHalf);
    }
  }
}