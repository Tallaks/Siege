using Kulinaria.Tools.BattleTrier.Runtime.Network.Roles;
using Unity.Netcode;
using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.UI
{
  public class GameplayMediator : MonoBehaviour
  {
    [SerializeField] private MapSelectionUi _mapSelectionUi;
    public void InitializeMapSelectionUi(RoleState stateValue, NetworkVariable<bool> mapSelectedState) => 
      _mapSelectionUi.Initialize(stateValue, mapSelectedState);

    public void HideMapSelectionUi() => 
      _mapSelectionUi.HideMapSelectionUi();
  }
}