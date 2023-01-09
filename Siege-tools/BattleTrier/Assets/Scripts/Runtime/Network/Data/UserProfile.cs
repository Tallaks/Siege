using System;

namespace Kulinaria.Tools.BattleTrier.Runtime.Network.Data
{
  [Serializable]
  public class UserProfile
  {
    public event Action<UserProfile> OnChanged;

    public bool IsHost { get; set; }
    public string Name { get; set; }
    public string Id { get; set; }

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
  }
}