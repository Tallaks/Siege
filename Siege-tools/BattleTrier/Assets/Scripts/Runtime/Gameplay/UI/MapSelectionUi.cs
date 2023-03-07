using Unity.Netcode;
using UnityEngine;
using Zenject;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.UI
{
  public class MapSelectionUi : MonoBehaviour
  {
    [SerializeField] private GameObject _firstRoleUi;
    [SerializeField] private GameObject _otherRoleUi;

    private NetworkManager _networkManager;

    [Inject]
    private void Construct(NetworkManager networkManager) =>
      _networkManager = networkManager;

    public void Initialize() => 
      _otherRoleUi.SetActive(true);
  }
}