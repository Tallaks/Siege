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
    private Session<SessionPlayerData> _session;

    public HostingState(IConnectionStateMachine connectionStateMachine,
      NetworkManager networkManager,
      Session<SessionPlayerData> session,
      LobbyServiceFacade lobbyService)
    {
      _connectionStateMachine = connectionStateMachine;
      _networkManager = networkManager;
      _session = session;
      _lobbyService = lobbyService;
    }
    
    public override void Enter()
    {
      Debug.Log("Server started, entering hosting state");
      SceneManager.LoadSceneAsync("RoleSelection");
      if (_lobbyService.CurrentLobby != null)
        _lobbyService.StartTracking();
    }

    public void ApprovalCheck(NetworkManager.ConnectionApprovalRequest request, NetworkManager.ConnectionApprovalResponse response)
    {
      Debug.Log("Hosting state approval check");
      byte[] connectionData = request.Payload;
      ulong clientId = request.ClientNetworkId;
      Debug.Log("Connection data length" + connectionData.Length);
      if (connectionData.Length > 1024)
      {
        response.Approved = false;
        return;
      }

      string payload = System.Text.Encoding.UTF8.GetString(connectionData);
      var connectionPayload = JsonUtility.FromJson<ConnectionPayload>(payload); // https://docs.unity3d.com/2020.2/Documentation/Manual/JSONSerialization.html
      ConnectStatus gameReturnStatus = GetConnectStatus(connectionPayload);

      if (gameReturnStatus == ConnectStatus.Success)
      {
        _session.SetupConnectingPlayerSessionData(clientId, connectionPayload.PlayerId,
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

      return _session.IsDuplicateConnection(connectionPayload.PlayerId) ?
        ConnectStatus.LoggedInAgain : ConnectStatus.Success;
    }
  }
}