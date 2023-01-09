using System.Threading.Tasks;

namespace Kulinaria.Tools.BattleTrier.Runtime.Network.Connection
{
  public interface IConnectionService
  {
    Task SetupHostConnectionAsync(string userName);
    Task ConnectClientAsync(string userName);
  }
}