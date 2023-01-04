using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Utilities
{
  public class UndestroyableBehaviour : MonoBehaviour
  {
    private void Awake() => 
      DontDestroyOnLoad(gameObject);
  }
}