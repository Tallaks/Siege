namespace Kulinaria.Tools.BattleTrier.Runtime.Network.Connection.States
{
  public interface IClientDisconnect
  {
    void ReactToClientDisconnect(ulong clientId);
  }
}