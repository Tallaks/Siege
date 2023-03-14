using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Maps.Selection.UI;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Gameplay;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Roles;
using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.UI
{
  public class GameplayMediator : MonoBehaviour
  {
    [SerializeField] private MapSelectionUi _mapSelectionUi;
    public void InitializeMapSelectionUi(RoleState stateValue, MapSelectionBehaviour mapSelectionBehaviour) => 
      _mapSelectionUi.Initialize(stateValue, mapSelectionBehaviour);

    public void HideMapSelectionUi() => 
      _mapSelectionUi.HideMapSelectionUi();
  }
}