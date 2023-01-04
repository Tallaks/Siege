using System.Collections;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Authentication;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Installers
{
  public class AuthenticationInstaller : MonoInstaller, IInitializable
  {
    [SerializeField] private Button _signInButton;

    public void Initialize()
    {
      _signInButton.interactable = false;
      StartCoroutine(WaitForUnityServicesInitialized());
      _signInButton.onClick.AddListener(Container.Resolve<SignInService>().SignInAnonymously);
    }

    public override void InstallBindings()
    {
      Container.BindInterfacesTo<AuthenticationInstaller>().FromInstance(this).AsSingle();
      Container.Bind<SignInService>().FromNew().AsSingle();
    }

    private IEnumerator WaitForUnityServicesInitialized()
    {
      while (true)
      {
        yield return null;
        if (UnityServices.State == ServicesInitializationState.Initialized)
        {
          _signInButton.interactable = true;
          yield break;
        }
      }
    }
  }
}