using Kulinaria.Tools.BattleTrier.Infrastructure.Services.Network.Authentication;
using Kulinaria.Tools.BattleTrier.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Kulinaria.Tools.BattleTrier.Infrastructure.Installers
{
  public class LobbyInstaller : MonoInstaller, IInitializable
  {
    public static string JoinCode;

    [SerializeField] private LobbyMediator _lobbyMediator;
    
    public async void Initialize()
    {
      _lobbyMediator.Initialize();
      
      Container.Resolve<LobbyService>().OnGameCreated += code =>
      {
        JoinCode = code;
        SceneManager.LoadSceneAsync("Level1");
      };
    }

    public override void InstallBindings()
    {
      Container.BindInterfacesTo<LobbyInstaller>().FromInstance(this).AsSingle();
      Container.Bind<LobbyService>().AsSingle();
    }
  }
}