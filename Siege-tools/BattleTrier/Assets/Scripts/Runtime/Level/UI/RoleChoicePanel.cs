using UnityEngine;
using UnityEngine.UI;

namespace Kulinaria.Tools.BattleTrier.Runtime.Level.UI
{
  public class RoleChoicePanel : MonoBehaviour
  {
    [SerializeField] private Button _spectatorButton;
    [SerializeField] private Button _secondPlayerButton;
    
    public void Show() => 
      gameObject.SetActive(true);
  }
}