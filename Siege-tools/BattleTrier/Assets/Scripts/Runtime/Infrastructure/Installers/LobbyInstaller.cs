using Kulinaria.Tools.BattleTrier.Runtime.Network.Authentication;
using Kulinaria.Tools.BattleTrier.Runtime.UI;
using UnityEngine;
using Zenject;

namespace Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Installers
{
  public class LobbyInstaller : MonoInstaller, IInitializable
  {
    public static string JoinCode;

    [SerializeField] private MainMenuMediator _mainMenuMediator;

    private AuthenticationServiceFacade _authenticationService;

    [Inject]
    private void Construct(AuthenticationServiceFacade authenticationService) =>
      _authenticationService = authenticationService;

    public void Initialize() =>
      TrySignIn();

    public override void InstallBindings()
    {
      Container.Bind<IInitializable>().To<LobbyInstaller>().FromInstance(this).AsSingle();
      Container.Bind<MainMenuMediator>().FromInstance(_mainMenuMediator).AsSingle();
    }

    private async void TrySignIn()
    {
      _mainMenuMediator.HideUntilAuth();
      _authenticationService = Container.Resolve<AuthenticationServiceFacade>();
      await _authenticationService.SignInAnonymously();

      Debug.Log($"Signed in. Unity Player ID {_authenticationService.PlayerId}");
      _mainMenuMediator.Initialize();
    }
  }
}