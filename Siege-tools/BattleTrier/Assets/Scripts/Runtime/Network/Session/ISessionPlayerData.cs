namespace Kulinaria.Tools.BattleTrier.Runtime.Network.Session
{
  public interface ISessionPlayerData
  {
    bool IsConnected { get; set; }
    ulong ClientID { get; set; }
    void Reinitialize();
  }
}