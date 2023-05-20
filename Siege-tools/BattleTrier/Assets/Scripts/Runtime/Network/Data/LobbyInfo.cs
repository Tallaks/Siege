using System;
using System.Collections.Generic;
using Unity.Services.Lobbies.Models;
using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Runtime.Network.Data
{
  [Serializable]
  public sealed class LobbyInfo
  {
    private Dictionary<string, UserProfile> _lobbyUsers = new();

    public string Id { get; set; }
    public string Code { get; set; }
    public string RelayJoinCode { get; set; }
    public string Name { get; set; }
    public IDictionary<string, UserProfile> LobbyUsers => _lobbyUsers;

    public LobbyInfo()
    {
    }

    public LobbyInfo(Lobby lobby) =>
      ApplyRemoteData(lobby);

    public event Action<LobbyInfo> OnLobbyChanged;

    public void ApplyRemoteData(Lobby lobby)
    {
      Id = lobby.Id;
      Name = lobby.Name;
      Code = lobby.LobbyCode;
      if (lobby.Data != null)
        RelayJoinCode = lobby.Data.ContainsKey("RelayJoinCode") ? lobby.Data["RelayJoinCode"].Value : null;
      else
        RelayJoinCode = null;

      var lobbyUsers = new Dictionary<string, UserProfile>();
      foreach (Player player in lobby.Players)
      {
        if (player.Data == null)
          if (LobbyUsers.ContainsKey(player.Id))
          {
            lobbyUsers.Add(player.Id, LobbyUsers[player.Id]);
            continue;
          }

        var incomingData = new UserProfile
        {
          IsHost = lobby.HostId.Equals(player.Id),
          Id = player.Id,
          Name = player.Data?.ContainsKey("DisplayName") == true ? player.Data["DisplayName"].Value : default
        };

        lobbyUsers.Add(incomingData.Id, incomingData);
      }

      CopyDataFrom(lobbyUsers);
    }

    public void AddUser(UserProfile localUser)
    {
      if (!_lobbyUsers.ContainsKey(localUser.Id))
      {
        DoAddUser(localUser);
        OnLobbyChanged?.Invoke(this);
      }
    }

    public void Reset(UserProfile localUser)
    {
      CopyDataFrom(new Dictionary<string, UserProfile>());
      AddUser(localUser);
    }

    public Dictionary<string, DataObject> GetDataForUnityServices() =>
      new()
      {
        { "RelayJoinCode", new DataObject(DataObject.VisibilityOptions.Public, RelayJoinCode) }
      };

    private void CopyDataFrom(Dictionary<string, UserProfile> lobbyUsers)
    {
      if (lobbyUsers == null)
      {
        _lobbyUsers = new Dictionary<string, UserProfile>();
      }
      else
      {
        var toRemove = new List<UserProfile>();
        foreach (KeyValuePair<string, UserProfile> oldUser in _lobbyUsers)
          if (lobbyUsers.ContainsKey(oldUser.Key))
            oldUser.Value.CopyDataFrom(lobbyUsers[oldUser.Key]);
          else
            toRemove.Add(oldUser.Value);

        foreach (UserProfile remove in toRemove)
          DoRemoveUser(remove);

        foreach (KeyValuePair<string, UserProfile> currUser in lobbyUsers)
          if (!_lobbyUsers.ContainsKey(currUser.Key))
            DoAddUser(currUser.Value);
      }

      OnLobbyChanged?.Invoke(this);
    }

    private void DoAddUser(UserProfile user)
    {
      _lobbyUsers.Add(user.Id, user);
      user.OnChanged += OnChangedUser;
    }

    private void DoRemoveUser(UserProfile user)
    {
      if (!_lobbyUsers.ContainsKey(user.Id))
      {
        Debug.LogWarning($"Player {user.Name}({user.Id}) does not exist in lobby: {Id}");
        return;
      }

      _lobbyUsers.Remove(user.Id);
      user.OnChanged -= OnChangedUser;
    }

    private void OnChangedUser(UserProfile user) =>
      OnLobbyChanged?.Invoke(this);
  }
}