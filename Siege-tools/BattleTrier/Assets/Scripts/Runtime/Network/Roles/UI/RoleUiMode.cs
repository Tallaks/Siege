using System;

namespace Kulinaria.Tools.BattleTrier.Runtime.Network.Roles.UI
{
  [Flags]
  public enum RoleUiMode
  {
    None = 0,
    ChooseSeat = 1,
    SeatChosenFirst = 2,
    SeatChosenSecond = 4,
    SeatChosenSpectator = 8,
    LobbyEnding = 16,
    FatalError = 32
  }
}