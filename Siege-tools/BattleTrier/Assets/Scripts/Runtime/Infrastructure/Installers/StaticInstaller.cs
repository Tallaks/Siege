using Zenject;

namespace Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Installers
{
  public class StaticInstaller : MonoInstaller
  {
    public T GetResolve<T>() =>
      Container.Resolve<T>();

    public bool HasResolve<T>() =>
      Container.HasBinding<T>();
  }
}