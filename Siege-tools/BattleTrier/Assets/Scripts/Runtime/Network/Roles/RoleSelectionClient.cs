using Kulinaria.Tools.BattleTrier.Runtime.Network.Roles.UI;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Utilities;
using Unity.Netcode;
using UnityEngine;
using Zenject;

namespace Kulinaria.Tools.BattleTrier.Runtime.Network.Roles
{
  public class RoleSelectionClient : MonoBehaviour
  {
    [SerializeField] private NetCodeHook _hook;
    private RoleMediator _mediator;

    private NetworkManager _networkManager;
    private RoleSelectionService _roleSelectionService;

    [Inject]
    private void Construct(
      NetworkManager networkManager,
      RoleSelectionService roleSelectionService,
      RoleMediator mediator)
    {
      _networkManager = networkManager;
      _roleSelectionService = roleSelectionService;
      _mediator = mediator;
    }

    private void Awake()
    {
      if (_networkManager.IsClient)
      {
        Debug.Log("Role Selection Client Awake", _mediator);
        _hook.OnNetworkSpawnHook += OnNetworkSpawn;
        _hook.OnNetworkDeSpawnHook += OnNetworkDeSpawn;
      }

      _mediator.UpdateLobbyUi();
    }

    private void OnDestroy()
    {
      _hook.OnNetworkSpawnHook -= OnNetworkSpawn;
      _hook.OnNetworkDeSpawnHook -= OnNetworkDeSpawn;
    }

    private void OnNetworkSpawn()
    {
      Debug.Log("Network spawn: client");
      _roleSelectionService.PlayerRoles.OnListChanged += OnLobbyPlayerStateChanged;
    }

    private void OnNetworkDeSpawn()
    {
      Debug.Log("Network despawn: client");
      _roleSelectionService.PlayerRoles.OnListChanged -= OnLobbyPlayerStateChanged;
    }

    private void OnLobbyPlayerStateChanged(NetworkListEvent<PlayerRoleState> changeEvent)
    {
      Debug.Log("Player states list changed");
      UpdatePlayerCount();
      _mediator.UpdateLobbyUi();
    }

    private void UpdatePlayerCount()
    {
      int count = _roleSelectionService.PlayerRoles.Count;
      _mediator.UpdatePlayerCount(count);
    }
  }
}