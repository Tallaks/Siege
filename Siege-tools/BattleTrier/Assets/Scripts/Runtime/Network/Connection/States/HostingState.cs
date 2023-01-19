using Kulinaria.Tools.BattleTrier.Runtime.Network.Data;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Lobbies;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Session;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Kulinaria.Tools.BattleTrier.Runtime.Network.Connection.States
{
  public class HostingState : NonParameterConnectionState, IApprovalCheck, IOnlineState
  {
    private readonly IConnectionStateMachine _connectionStateMachine;
    private readonly NetworkManager _networkManager;
    private readonly LobbyServiceFacade _lobbyService;

    public HostingState(IConnectionStateMachine connectionStateMachine, NetworkManager networkManager, LobbyServiceFacade lobbyService)
    {
      _connectionStateMachine = connectionStateMachine;
      _networkManager = networkManager;
      _lobbyService = lobbyService;
    }
    
    public override void Enter()
    {
      SceneManager.LoadSceneAsync("RoleSelection");
      if (_lobbyService.CurrentLobby != null)
        _lobbyService.StartTracking();
    }

    public void ApprovalCheck(NetworkManager.ConnectionApprovalRequest request, NetworkManager.ConnectionApprovalResponse response)
    {
      var connectionData = request.Payload;
      var clientId = request.ClientNetworkId;
      if (connectionData.Length > 8)
      {
        // If connectionData too high, deny immediately to avoid wasting time on the server. This is intended as
        // a bit of light protection against DOS attacks that rely on sending silly big buffers of garbage.
        response.Approved = false;
        return;
      }

      var payload = System.Text.Encoding.UTF8.GetString(connectionData);
      var connectionPayload = JsonUtility.FromJson<ConnectionPayload>(payload); // https://docs.unity3d.com/2020.2/Documentation/Manual/JSONSerialization.html
      var gameReturnStatus = GetConnectStatus(connectionPayload);

      if (gameReturnStatus == ConnectStatus.Success)
      {
        SessionService<SessionPlayerData>.Instance.SetupConnectingPlayerSessionData(clientId, connectionPayload.PlayerId,
          new SessionPlayerData(clientId, connectionPayload.PlayerName, new NetworkGuid(), 0, true));

        // connection approval will create a player object for you
        response.Approved = true;
        response.CreatePlayerObject = true;
        response.Position = Vector3.zero;
        response.Rotation = Quaternion.identity;
        return;
      }

      response.Approved = false;
      response.Reason = JsonUtility.ToJson(gameReturnStatus);
      if (_lobbyService.CurrentLobby != null)
        _lobbyService.RemovePlayerFromLobbyAsync(connectionPayload.PlayerId, _lobbyService.CurrentLobby.Id);

    }

    public void OnTransportFailure() => 
      _connectionStateMachine.Enter<OfflineState>();

    public override void Exit()
    {
      
    }

    private ConnectStatus GetConnectStatus(ConnectionPayload connectionPayload)
    {
      if (_networkManager.ConnectedClientsIds.Count >= 8)
      {
        return ConnectStatus.ServerFull;
      }

      if (connectionPayload.IsDebug != Debug.isDebugBuild)
      {
        return ConnectStatus.IncompatibleBuildType;
      }

      return SessionService<SessionPlayerData>.Instance.IsDuplicateConnection(connectionPayload.PlayerId) ?
        ConnectStatus.LoggedInAgain : ConnectStatus.Success;
    }
  }
}