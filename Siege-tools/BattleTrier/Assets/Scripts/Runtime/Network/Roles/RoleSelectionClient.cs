using System;
using System.Collections.Generic;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Roles.UI;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Utilities;
using Unity.Netcode;
using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Runtime.Network.Roles
{
  public class RoleSelectionClient : IDisposable
  {
    private readonly NetCodeHook _hook;
    private readonly NetworkManager _networkManager;
    private readonly RoleSelectionService _roleSelectionService;
    private readonly RoleMediator _mediator;

    private List<RoleType> _roleVariant = new();

    private int _lastRoleSelected = -1;

    private bool _hasLocalPlayerLockedIn = false;

    public RoleSelectionClient(RoleSelectionService roleSelectionService, NetworkManager networkManager,
      RoleMediator mediator, NetCodeHook hook)
    {
      _networkManager = networkManager;
      _mediator = mediator;
      _roleSelectionService = roleSelectionService;
      _hook = hook;
    }

    public void Initialize()
    {
      if (_networkManager.IsClient)
      {
        Debug.Log("Role Selection Client Initialization", _mediator);
        _hook.OnNetworkSpawnHook += OnNetworkSpawn;
        _hook.OnNetworkDeSpawnHook += OnNetworkDeSpawn;
      }

      _mediator.ConfigureUIForLobbyMode(RoleUiMode.ChooseSeat);
    }

    public void Dispose()
    {
      _hook.OnNetworkSpawnHook -= OnNetworkSpawn;
      _hook.OnNetworkDeSpawnHook -= OnNetworkDeSpawn;
    }

    public void OnPlayerChosenRole(RoleType buttonIndex)
    {
      if(_roleSelectionService.IsSpawned)
        _roleSelectionService.ChangeSeatServerRpc(_networkManager.LocalClientId, (int)buttonIndex);
    }

    private void OnNetworkSpawn()
    {
      Debug.Log("Network spawn: client");
      _roleSelectionService.LobbyIsClosed.OnValueChanged += OnLobbyClosedChanged;
      _roleSelectionService.PlayerRoles.OnListChanged += OnLobbyPlayerStateChanged;
    }

    private void OnNetworkDeSpawn()
    {
      Debug.Log("Network despawn: client");
      _roleSelectionService.LobbyIsClosed.OnValueChanged -= OnLobbyClosedChanged;
      _roleSelectionService.PlayerRoles.OnListChanged -= OnLobbyPlayerStateChanged;
    }

    private void OnLobbyPlayerStateChanged(NetworkListEvent<PlayerRoleState> changeEvent)
    {
      UpdateSeats();
      UpdatePlayerCount();

      int localPlayerIdx = _roleSelectionService.IndexOfClient(_networkManager.LocalClientId);
    }

    private void OnLobbyClosedChanged(bool wasLobbyClosed, bool isLobbyClosed)
    {
      if (isLobbyClosed)
        _mediator.ConfigureUIForLobbyMode(RoleUiMode.LobbyEnding);
      else
        _mediator.ConfigureUIForLobbyMode(_lastRoleSelected == -1 ? RoleUiMode.ChooseSeat : RoleUiMode.SeatChosen);
    }

    private void UpdateSeats()
    {
      var firstRoleChosen = false;
      var firstRoleState = new PlayerRoleState();
      var secondRoleChosen = false;
      var secondRoleState = new PlayerRoleState();
      foreach (PlayerRoleState playerRole in _roleSelectionService.PlayerRoles)
      {
        if (playerRole.RoleId == 1)
        {
          firstRoleChosen = true;
          firstRoleState = playerRole;
        }

        if (playerRole.RoleId == 2)
        {
          secondRoleChosen = true;
          secondRoleState = playerRole;
        }
      }

      if(firstRoleChosen)
        _mediator.SerFirstRoleState(firstRoleState);
      if (secondRoleChosen)
        _mediator.SetSecondRoleState(secondRoleState);
    }

    private void UpdatePlayerCount()
    {
      int count = _roleSelectionService.PlayerRoles.Count;
      _mediator.UpdatePlayerCount(count);
    }
  }
}