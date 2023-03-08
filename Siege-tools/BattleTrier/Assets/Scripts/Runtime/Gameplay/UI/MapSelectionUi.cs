using Kulinaria.Tools.BattleTrier.Runtime.Network.Roles;
using Unity.Netcode;
using UnityEngine;
using Zenject;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.UI
{
  public class MapSelectionUi : MonoBehaviour
  {
    [SerializeField] private GameObject _firstRoleUi;
    [SerializeField] private GameObject _otherRoleUi;

    public void Initialize(RoleState stateValue)
    {
      if(stateValue == RoleState.ChosenFirst)
        _firstRoleUi.SetActive(true);
      else
        _otherRoleUi.SetActive(true);
    }
  }
}