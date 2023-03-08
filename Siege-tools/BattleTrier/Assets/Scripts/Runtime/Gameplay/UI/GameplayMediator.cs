using Kulinaria.Tools.BattleTrier.Runtime.Network.Roles;
using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.UI
{
  public class GameplayMediator : MonoBehaviour
  {
    [SerializeField] private MapSelectionUi _mapSelectionUi;
    public void InitializeMapSelectionUi(RoleState stateValue) => 
      _mapSelectionUi.Initialize(stateValue);
  }
}