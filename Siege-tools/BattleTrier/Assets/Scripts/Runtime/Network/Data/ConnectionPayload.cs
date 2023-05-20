using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Runtime.Network.Data
{
  [SerializeField]
  public class ConnectionPayload
  {
    public bool IsDebug;
    public string PlayerId;
    public string PlayerName;
  }
}