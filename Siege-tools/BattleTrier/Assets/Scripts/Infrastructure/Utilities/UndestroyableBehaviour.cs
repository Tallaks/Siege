using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Infrastructure.Utilities
{
  public class UndestroyableBehaviour : MonoBehaviour
  {
    private void Awake() => 
      DontDestroyOnLoad(gameObject);
  }
}