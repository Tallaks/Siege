using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters
{
  public class Enemy : MonoBehaviour
  {
    public Character Character => GetComponent<Character>();
  }
}