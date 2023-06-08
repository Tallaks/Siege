using Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Installers;
using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Services
{
  public static class ServiceProvider
  {
    public static T ResolveFromOfflineInstaller<T>() =>
      Object.FindObjectOfType<OfflineGameplayInstaller>().GetResolve<T>();

    public static T ResolveFromOnlineInstaller<T>() =>
      Object.FindObjectOfType<OnlineGameplayInstaller>().GetResolve<T>();
  }
}