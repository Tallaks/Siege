using Unity.Netcode;
using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Runtime.Data
{
  [CreateAssetMenu(menuName = "Kulinaria/Board")]
  public class BoardData : ScriptableObject
  {
    public string Name;
    public NetworkObject Prefab;
    public Sprite Icon;
  }
}