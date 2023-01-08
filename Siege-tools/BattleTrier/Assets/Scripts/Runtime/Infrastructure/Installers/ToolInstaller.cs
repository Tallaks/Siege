using Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Services.Coroutines;
using Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Services.Inputs;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Authentication;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Lobby;
using Unity.Netcode;
using UnityEngine.SceneManagement;
using Zenject;

namespace Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Installers
{
  public class ToolInstaller : MonoInstaller, IInitializable
  {
    public override void InstallBindings()
    {
      Container.BindInterfacesTo<ToolInstaller>().FromInstance(this).AsSingle();
      Container.Bind<IInputService>().To<OldInputService>().FromInstance(FindObjectOfType<OldInputService>()).AsSingle();
      Container.Bind<ICoroutineRunner>().To<CoroutineRunner>().FromInstance(FindObjectOfType<CoroutineRunner>()).AsSingle();

      Container.Bind<NetworkManager>().FromInstance(FindObjectOfType<NetworkManager>()).AsSingle();
      Container.Bind<AuthenticationServiceFacade>().FromNew().AsSingle();
      Container.Bind<LobbyServiceFacade>().FromNew().AsSingle();
    }

    public void Initialize() =>
      SceneManager.LoadSceneAsync("MainMenu");
  }
}