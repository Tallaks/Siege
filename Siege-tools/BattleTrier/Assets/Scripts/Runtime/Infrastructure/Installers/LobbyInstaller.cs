using Kulinaria.Tools.BattleTrier.Runtime.Network.Authentication;
using Kulinaria.Tools.BattleTrier.Runtime.UI;
using UnityEngine;
using Zenject;

namespace Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Installers
{
  public class LobbyInstaller : MonoInstaller, IInitializable
  {
    public static string JoinCode;

    [SerializeField] private LobbyMediator _lobbyMediator;

    private AuthenticationServiceFacade _authentication;
    
    public void Initialize() => 
      TrySignIn();

    private async void TrySignIn()
    {
      _authentication = Container.Resolve<AuthenticationServiceFacade>();
      await _authentication.SignInAnonymously();
      
      Debug.Log($"Signed in. Unity Player ID {_authentication.PlayerId}");
    }

    public override void InstallBindings()
    {
      Container.BindInterfacesTo<LobbyInstaller>().FromInstance(this).AsSingle();
      Container.Bind<AuthenticationServiceFacade>().FromNew().AsSingle();
    }
  }
}