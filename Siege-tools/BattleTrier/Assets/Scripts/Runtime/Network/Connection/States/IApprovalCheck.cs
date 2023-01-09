using Unity.Netcode;

namespace Kulinaria.Tools.BattleTrier.Runtime.Network.Connection.States
{
  public interface IApprovalCheck
  {
    void ApprovalCheck(NetworkManager.ConnectionApprovalRequest request,
      NetworkManager.ConnectionApprovalResponse response);
  }
}