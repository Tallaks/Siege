using System;
using System.Collections.Generic;
using Unity.Services.Lobbies.Models;
using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Runtime.Network.Data
{
  [Serializable]
  public sealed class LobbyInfo
  {
    public struct LobbyData
    {
      public string LobbyID { get; set; }
      public string LobbyCode { get; set; }
      public string RelayJoinCode { get; set; }
      public string LobbyName { get; set; }

      public LobbyData(LobbyData existing)
      {
        LobbyID = existing.LobbyID;
        LobbyCode = existing.LobbyCode;
        RelayJoinCode = existing.RelayJoinCode;
        LobbyName = existing.LobbyName;
      }

      public LobbyData(string lobbyCode)
      {
        LobbyID = null;
        LobbyCode = lobbyCode;
        RelayJoinCode = null;
        LobbyName = null;
      }
    }

    public string Id
    {
      get => _data.LobbyID;
      set
      {
        _data.LobbyID = value;
        OnLobbyChanged?.Invoke(this);
      }
    }

    public string Code
    {
      get => _data.LobbyCode;
      set
      {
        _data.LobbyCode = value;
        OnLobbyChanged?.Invoke(this);
      }
    }

    public string RelayJoinCode
    {
      get => _data.RelayJoinCode;
      set
      {
        _data.RelayJoinCode = value;
        OnLobbyChanged?.Invoke(this);
      }
    }

    public string Name
    {
      get => _data.LobbyName;
      set
      {
        _data.LobbyName = value;
        OnLobbyChanged?.Invoke(this);
      }
    }

    public IDictionary<string, UserProfile> LobbyUsers => _lobbyUsers;

    private LobbyData _data;
    private Dictionary<string, UserProfile> _lobbyUsers = new();

    public LobbyInfo()
    {
    }

    public LobbyInfo(Lobby lobby) =>
      ApplyRemoteData(lobby);

    public void AddUser(UserProfile localUser)
    {
      if (!_lobbyUsers.ContainsKey(localUser.Id))
      {
        DoAddUser(localUser);
        OnLobbyChanged?.Invoke(this);
      }
    }

    public void ApplyRemoteData(Lobby lobby)
    {
      var info = new LobbyData();
      info.LobbyID = lobby.Id;
      info.LobbyCode = lobby.LobbyCode;
      info.LobbyName = lobby.Name;
      if (lobby.Data != null)
        info.RelayJoinCode = lobby.Data.TryGetValue("RelayJoinCode", out DataObject value) ? value.Value : null;
      else
        info.RelayJoinCode = null;

      var lobbyUsers = new Dictionary<string, UserProfile>();
      foreach (Player player in lobby.Players)
      {
        if (player.Data == null)
          if (LobbyUsers.TryGetValue(player.Id, out UserProfile user))
          {
            lobbyUsers.Add(player.Id, user);
            continue;
          }

        var incomingData = new UserProfile
        {
          IsHost = lobby.HostId.Equals(player.Id),
          Id = player.Id,
          Name = player.Data != null && player.Data.TryGetValue("DisplayName", out PlayerDataObject value)
            ? value.Value
            : default
        };

        lobbyUsers.Add(incomingData.Id, incomingData);
      }

      CopyDataFrom(info, lobbyUsers);
    }

    public Dictionary<string, DataObject> GetDataForUnityServices() =>
      new()
      {
        { "RelayJoinCode", new DataObject(DataObject.VisibilityOptions.Public, RelayJoinCode) }
      };

    public void Reset(UserProfile localUser)
    {
      CopyDataFrom(new LobbyData(), new Dictionary<string, UserProfile>());
      AddUser(localUser);
    }

    private void CopyDataFrom(LobbyData data, Dictionary<string, UserProfile> lobbyUsers)
    {
      _data = data;

      if (lobbyUsers == null)
      {
        _lobbyUsers = new Dictionary<string, UserProfile>();
      }
      else
      {
        var toRemove = new List<UserProfile>();
        foreach (KeyValuePair<string, UserProfile> oldUser in _lobbyUsers)
          if (lobbyUsers.TryGetValue(oldUser.Key, out UserProfile user))
            oldUser.Value.CopyDataFrom(user);
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

    public event Action<LobbyInfo> OnLobbyChanged;
  }
}