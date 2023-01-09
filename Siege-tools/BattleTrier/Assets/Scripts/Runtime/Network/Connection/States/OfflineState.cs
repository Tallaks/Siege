using Unity.Netcode;

namespace Kulinaria.Tools.BattleTrier.Runtime.Network.Connection.States
{
  public class OfflineState : NonParameterConnectionState
  {
    private NetworkManager _networkManager;

    public OfflineState(NetworkManager networkManager) => 
      _networkManager = networkManager;

    public override void Enter() => 
      _networkManager.Shutdown();

    public override void Exit()
    {
    }
  }
}