using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Runtime.Network.Data
{
  [SerializeField]
  public class ConnectionPayload
  {
    public string PlayerId;
    public string PlayerName;
    public bool IsDebug;
  }
}