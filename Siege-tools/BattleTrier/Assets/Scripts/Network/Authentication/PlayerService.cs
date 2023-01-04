using Kulinaria.Tools.BattleTrier.Network.Roles;
using Unity.Services.Authentication;
using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Network.Authentication
{
  public class PlayerService : MonoBehaviour
  {
    [SerializeField] private FirstPlayerBehaviour _firstPlayerPrefab;
    
    public void RegisterFirstPlayer()
    {
      FirstPlayerBehaviour firstPlayer = Instantiate(_firstPlayerPrefab);
      firstPlayer.NetworkObject.Spawn();
      firstPlayer.TakeRole(AuthenticationService.Instance.PlayerId);
    }

    public void RegisterSecondPlayer()
    {
      
    }

    public void RegisterSpectatorPlayer()
    {
      
    }
  }
}