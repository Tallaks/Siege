using System.Threading.Tasks;

namespace Kulinaria.Tools.BattleTrier.Runtime.Network.Connection
{
  public interface IConnectionService
  {
    Task ConnectClientAsync(string userName);
    Task SetupHostConnectionAsync(string userName);
  }
}