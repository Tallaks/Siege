using Kulinaria.Tools.BattleTrier.Runtime.Network.Authentication;

namespace Kulinaria.Tools.BattleTrier.Runtime.Network.Lobby
{
  public class LobbyServiceFacade
  {
    private readonly AuthenticationServiceFacade _authentication;

    public LobbyServiceFacade(AuthenticationServiceFacade authentication)
    {
      _authentication = authentication;
    }
  }
}