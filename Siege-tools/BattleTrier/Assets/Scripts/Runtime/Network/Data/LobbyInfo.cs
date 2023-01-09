using System;
using System.Collections.Generic;
using Unity.Services.Lobbies.Models;
using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Runtime.Network.Data
{
  [Serializable]
  public class LobbyInfo
  {
    public event Action<LobbyInfo> OnLobbyChanged;
    public string Id { get; set; }
    public string Code { get; set; }
    public string RelayJoinCode { get; set; }
    public string Name { get; set; }
    public IDictionary<string, UserProfile> LobbyUsers => _lobbyUsers;

    private Dictionary<string, UserProfile> _lobbyUsers = new();

    public LobbyInfo()
    {
    }

    public LobbyInfo(Lobby lobby) =>
      ApplyRemoteData(lobby);

    public void ApplyRemoteData(Lobby lobby)
    {
      var data = new LobbyInfo();
      data.Id = lobby.Id;
      data.Name = lobby.Name;
      data.Code = lobby.LobbyCode;
      if (lobby.Data != null)
        data.RelayJoinCode = lobby.Data.ContainsKey("RelayJoinCode") ? lobby.Data["RelayJoinCode"].Value : null;
      else
        data.RelayJoinCode = null;

      var lobbyUsers = new Dictionary<string, UserProfile>();
      foreach (var player in lobby.Players)
      {
        if (player.Data == null)
          continue;

        if (LobbyUsers.ContainsKey(player.Id))
          lobbyUsers.Add(player.Id, LobbyUsers[player.Id]);
      }

      CopyDataFrom(lobbyUsers);
    }

    private void CopyDataFrom(Dictionary<string, UserProfile> lobbyUsers)
    {
      var toRemove = new List<UserProfile>();
      foreach (KeyValuePair<string, UserProfile> oldUser in _lobbyUsers)
      {
        if (lobbyUsers.ContainsKey(oldUser.Key))
          oldUser.Value.CopyDataFrom(lobbyUsers[oldUser.Key]);
        else
          toRemove.Add(oldUser.Value);
      }

      foreach (UserProfile remove in toRemove)
        DoRemoveUser(remove);

      foreach (KeyValuePair<string, UserProfile> currUser in lobbyUsers)
      {
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

    public Dictionary<string, DataObject> GetDataForUnityServices() =>
      new()
      {
        { "RelayJoinCode", new DataObject(DataObject.VisibilityOptions.Public, RelayJoinCode) }
      };
  }
}