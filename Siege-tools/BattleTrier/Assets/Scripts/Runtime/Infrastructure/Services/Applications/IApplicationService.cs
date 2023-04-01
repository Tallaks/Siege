using Zenject;

namespace Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Services.Applications
{
  public interface IApplicationService : IInitializable
  {
    void QuitApplication();
  }
}