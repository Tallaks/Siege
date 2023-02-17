using System;
using System.Collections.Generic;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Roles.UI;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Utilities;
using Unity.Netcode;
using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Runtime.Network.Roles
{
  public enum UiMode
  {
    None = 0,
    ChooseSeat = 1,
    SeatChosen = 2,
    LobbyEnding = 3,
    FatalError = 4
  }

  public class RoleSelectionClient : IDisposable
  {
    private readonly NetCodeHook _hook;
    private readonly NetworkManager _networkManager;
    private readonly RoleSelectionService _roleSelectionService;
    private readonly RoleMediator _mediator;

    private List<string> _roleVariant = new();

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
    }

    public void Dispose()
    {
      _hook.OnNetworkSpawnHook -= OnNetworkSpawn;
      _hook.OnNetworkDeSpawnHook -= OnNetworkDeSpawn;
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

    private void OnLobbyPlayerStateChanged(NetworkListEvent<PlayerRoleState> changeevent)
    {
      UpdateSeats();
      UpdatePlayerCount();

      int localPlayerIdx = -1;
      for (int i = 0; i < _roleSelectionService.PlayerRoles.Count; ++i)
      {
        if (_roleSelectionService.PlayerRoles[i].ClientId == _networkManager.LocalClientId)
        {
          localPlayerIdx = i;
          break;
        }
      }

      if (localPlayerIdx == -1)
        UpdateCharacterSelection(RoleState.Inactive);
      else if (_roleSelectionService.PlayerRoles[localPlayerIdx].State == RoleState.Inactive)
        UpdateCharacterSelection(RoleState.Inactive);
      else
        UpdateCharacterSelection(_roleSelectionService.PlayerRoles[localPlayerIdx].State,
          _roleSelectionService.PlayerRoles[localPlayerIdx].RoleId);
    }

    private void OnLobbyClosedChanged(bool wasLobbyClosed, bool isLobbyClosed)
    {
      if (isLobbyClosed)
        _mediator.ConfigureUIForLobbyMode(UiMode.LobbyEnding);
      else
        _mediator.ConfigureUIForLobbyMode(_lastRoleSelected == -1 ? UiMode.ChooseSeat : UiMode.SeatChosen);
    }

    private void UpdateSeats()
    {
      var curSeats = new PlayerRoleState[_roleVariant.Count];
      foreach (PlayerRoleState playerState in _roleSelectionService.PlayerRoles)
      {
        if (playerState.RoleId == -1 || playerState.State == RoleState.Inactive)
          continue;
        if (curSeats[playerState.RoleId].State == RoleState.Inactive
            || (curSeats[playerState.RoleId].State == RoleState.Active &&
                curSeats[playerState.RoleId].LastChangeTime < playerState.LastChangeTime))
        {
          curSeats[playerState.RoleId] = playerState;
        }
      }

      // now actually update the seats in the UI
      for (var i = 0; i < _roleVariant.Count; ++i)
      {
      }
    }

    void UpdateCharacterSelection(RoleState state, int roleId = -1)
    {
      bool isNewSeat = _lastRoleSelected != roleId;

      _lastRoleSelected = roleId;
      if (state == RoleState.Inactive)
      {
      }
      else
      {
        if (roleId != -1)
        {
          // change character preview when selecting a new seat
          if (isNewSeat)
          {
          }
        }

        if (state == RoleState.Chosen && !_hasLocalPlayerLockedIn)
        {
          _mediator.ConfigureUIForLobbyMode(_roleSelectionService.LobbyIsClosed.Value
            ? UiMode.LobbyEnding
            : UiMode.SeatChosen);
          _hasLocalPlayerLockedIn = true;
        }
        else if (_hasLocalPlayerLockedIn && state == RoleState.Active)
        {
          // reset character seats if locked in choice was unselected
          if (_hasLocalPlayerLockedIn)
          {
            _mediator.ConfigureUIForLobbyMode(UiMode.ChooseSeat);
            _hasLocalPlayerLockedIn = false;
          }
        }
        else if (state == RoleState.Active && isNewSeat)
        {
        }
      }
    }

    private void UpdatePlayerCount()
    {
      int count = _roleSelectionService.PlayerRoles.Count;
      _mediator.UpdatePlayerCount(count);
    }
  }
}