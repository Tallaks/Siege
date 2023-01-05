using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Runtime.Data
{
  [CreateAssetMenu(menuName = "Kulinaria/Board")]
  public class BoardData : ScriptableObject
  {
    public string Name;
    public GameObject Prefab;
    public Sprite Icon;
  }
}