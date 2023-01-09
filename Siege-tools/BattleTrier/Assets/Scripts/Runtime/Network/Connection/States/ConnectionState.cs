namespace Kulinaria.Tools.BattleTrier.Runtime.Network.Connection.States
{
  public abstract class ConnectionState
  {
    public abstract void Exit();

    public virtual void ReactToClientDisconnect(ulong clientId)
    {
    }
  }
}