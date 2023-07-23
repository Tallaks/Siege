using System;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Authentication;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Connection;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Connection.States;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Data;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Lobbies;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Session;
using Unity.Netcode;
using UnityEngine;
using Zenject;

namespace Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Installers
{
  public class NetworkInstaller : StaticInstaller, IInitializable, IDisposable
  {
    private IConnectionStateMachine _connectionStateMachine;
    private NetworkManager _networkManager;

    public void Initialize()
    {
      _connectionStateMachine = Container.Resolve<IConnectionStateMachine>();
      _networkManager = Container.Resolve<NetworkManager>();

      _connectionStateMachine.Initialize();
      _connectionStateMachine.Enter<OfflineState>();

      _networkManager.OnServerStarted += OnServerStarted;
      _networkManager.OnClientConnectedCallback += OnClientConnectedCallback;
      _networkManager.OnClientDisconnectCallback += OnClientDisconnectCallback;
      _networkManager.NetworkConfig.ConnectionApproval = true;
      _networkManager.NetworkConfig.EnableSceneManagement = true;
      _networkManager.NetworkConfig.EnableNetworkLogs = true;
      _networkManager.ConnectionApprovalCallback += ApprovalCheck;
      _networkManager.OnTransportFailure += OnTransportFailure;
    }

    public void Dispose()
    {
      _networkManager.OnServerStarted -= OnServerStarted;
      _networkManager.OnClientConnectedCallback -= OnClientConnectedCallback;
      _networkManager.OnClientDisconnectCallback -= OnClientDisconnectCallback;
      _networkManager.ConnectionApprovalCallback -= ApprovalCheck;
      _networkManager.OnTransportFailure -= OnTransportFailure;
    }

    public override void InstallBindings()
    {
      Container.BindInterfacesTo<NetworkInstaller>().FromInstance(this).AsSingle();
      Container.Bind<NetworkManager>().FromInstance(FindObjectOfType<NetworkManager>()).AsSingle();
      Container.Bind<AuthenticationServiceFacade>().FromNew().AsSingle();
      Container.Bind<LobbyServiceFacade>().FromNew().AsSingle();
      Container.Bind<LobbyHeartbeat>().FromNew().AsSingle();
      Container.Bind<UnityLobbyApi>().FromNew().AsSingle();
      Container.Bind<UserProfile>().FromNew().AsSingle();
      Container.Bind<LobbyInfo>().FromNew().AsSingle();
      Container.Bind<IConnectionStateMachine>().To<ConnectionStateMachine>().FromNew().AsSingle();
      Container.Bind<IConnectionService>().To<RelayConnectionService>().FromNew().AsSingle();
      Container.Bind<Session<SessionPlayerData>>().FromNew().AsSingle();
    }

    private void OnTransportFailure() =>
      _connectionStateMachine.Enter<OfflineState>();

    private void ApprovalCheck(NetworkManager.ConnectionApprovalRequest request,
      NetworkManager.ConnectionApprovalResponse response)
    {
      Debug.Log("Attempt to approve check");
      (_connectionStateMachine.CurrentState as IApprovalCheck)?.ApprovalCheck(request, response);
    }

    private void OnClientConnectedCallback(ulong clientId) =>
      (_connectionStateMachine.CurrentState as IClientConnect)?.OnClientConnect(clientId);

    private void OnClientDisconnectCallback(ulong clientId) =>
      (_connectionStateMachine.CurrentState as IClientDisconnect)?.ReactToClientDisconnect(clientId);

    private void OnServerStarted() => _connectionStateMachine.Enter<HostingState, bool>(true);
  }
}