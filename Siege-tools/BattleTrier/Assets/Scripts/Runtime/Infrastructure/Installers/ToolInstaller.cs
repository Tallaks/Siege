using System;
using Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Services.Coroutines;
using Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Services.Inputs;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Authentication;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Connection;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Connection.States;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Data;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Lobbies;
using Unity.Netcode;
using UnityEngine.SceneManagement;
using Zenject;

namespace Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Installers
{
  public class ToolInstaller : MonoInstaller, IInitializable, IDisposable
  {
    private NetworkManager _networkManager;
    private IConnectionStateMachine _connectionStateMachine;

    public override void InstallBindings()
    {
      Container.BindInterfacesTo<ToolInstaller>().FromInstance(this).AsSingle();
      Container.Bind<IInputService>().To<OldInputService>().FromInstance(FindObjectOfType<OldInputService>()).
        AsSingle();
      Container.Bind<ICoroutineRunner>().To<CoroutineRunner>().FromInstance(FindObjectOfType<CoroutineRunner>()).
        AsSingle();

      Container.Bind<NetworkManager>().FromInstance(FindObjectOfType<NetworkManager>()).AsSingle();
      Container.Bind<AuthenticationServiceFacade>().FromNew().AsSingle();
      Container.Bind<LobbyServiceFacade>().FromNew().AsSingle();
      Container.Bind<UnityLobbyApi>().FromNew().AsSingle();
      Container.Bind<UserProfile>().FromNew().AsSingle();
      Container.Bind<LobbyInfo>().FromNew().AsSingle();
      Container.Bind<IConnectionStateMachine>().To<ConnectionStateMachine>().FromNew().AsSingle();
      Container.Bind<IConnectionService>().To<RelayConnectionService>().FromNew().AsSingle();
    }

    public void Initialize()
    {
      _connectionStateMachine = Container.Resolve<IConnectionStateMachine>();
      _networkManager = Container.Resolve<NetworkManager>();

      _connectionStateMachine.Initialize();
      _connectionStateMachine.Enter<OfflineState>();

      _networkManager.OnServerStarted += OnServerStarted;
      _networkManager.OnClientConnectedCallback += OnClientConnectedCallback;
      _networkManager.OnClientDisconnectCallback += OnClientDisconnectCallback;
      _networkManager.ConnectionApprovalCallback += ApprovalCheck;
      _networkManager.OnTransportFailure += OnTransportFailure;

      SceneManager.LoadSceneAsync("MainMenu");
    }

    public void Dispose()
    {
      _networkManager.OnServerStarted -= OnServerStarted;
      _networkManager.OnClientConnectedCallback -= OnClientConnectedCallback;
      _networkManager.OnClientDisconnectCallback -= OnClientDisconnectCallback;
      _networkManager.ConnectionApprovalCallback -= ApprovalCheck;
      _networkManager.OnTransportFailure -= OnTransportFailure;
    }

    private void OnTransportFailure()
    {
      _connectionStateMachine.Enter<OfflineState>();
    }

    private void ApprovalCheck(NetworkManager.ConnectionApprovalRequest request, NetworkManager.ConnectionApprovalResponse response) => 
      ((IApprovalCheck)_connectionStateMachine.CurrentState)?.ApprovalCheck(request, response);

    private void OnClientDisconnectCallback(ulong clientId) => 
      _connectionStateMachine.CurrentState.ReactToClientDisconnect(clientId);

    private void OnClientConnectedCallback(ulong clientId) =>
      _connectionStateMachine.Enter<ClientConnectedState, ulong, ConnectionState>(clientId, _connectionStateMachine.CurrentState);

    private void OnServerStarted() => _connectionStateMachine.Enter<HostingState>();
  }
}