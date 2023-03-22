using System;
using System.Collections.Generic;
using Unity.Services.Lobbies.Models;

namespace Kulinaria.Tools.BattleTrier.Runtime.Network.Data
{
  [Serializable]
  public class UserProfile
  {
    public bool IsHost
    {
      get => _isHost;
      set
      {
        _isHost = value;
        OnChanged?.Invoke(this);
      }
    }

    public string Name
    {
      get => _name;
      set
      {
        _name = value;
        OnChanged?.Invoke(this);
      }
    }

    public string Id
    {
      get => _id;
      set
      {
        _id = value;
        OnChanged?.Invoke(this);
      }
    }

    private string _id;
    private bool _isHost;
    private string _name;

    public event Action<UserProfile> OnChanged;

    [Flags]
    public enum ChangedFields
    {
      IsHost = 1,
      DisplayName = 2,
      ID = 4,
    }

    public void CopyDataFrom(UserProfile lobbyUser)
    {
      int lastChanged = // Set flags just for the members that will be changed.
        (IsHost == lobbyUser.IsHost ? 0 : (int)ChangedFields.IsHost) |
        (Name == lobbyUser.Name ? 0 : (int)ChangedFields.DisplayName) |
        (Id == lobbyUser.Id ? 0 : (int)ChangedFields.ID);

      if (lastChanged == 0) // Ensure something actually changed.
        return;

      Id = lobbyUser.Id;
      Name = lobbyUser.Name;
      IsHost = lobbyUser.IsHost;

      OnChanged?.Invoke(this);
    }

    public void ResetState() =>
      IsHost = false;

    public Dictionary<string, PlayerDataObject> GetDataForUnityServices() =>
      new()
      {
        { "DisplayName", new PlayerDataObject(PlayerDataObject.VisibilityOptions.Member, Name) },
      };
  }
}