using Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Installers;
using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Services
{
  public static class ServiceProvider
  {
    public static T GetResolve<T>()
    {
      foreach (StaticInstaller installer in Object.FindObjectsOfType<StaticInstaller>())
        if (installer.HasResolve<T>())
          return installer.GetResolve<T>();

      return default;
    }
  }
}