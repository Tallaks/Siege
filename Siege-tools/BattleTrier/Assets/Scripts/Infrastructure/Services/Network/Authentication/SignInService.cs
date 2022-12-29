using Unity.Services.Authentication;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Kulinaria.Tools.BattleTrier.Infrastructure.Services.Network.Authentication
{
  public class SignInService
  {
    public string PlayerId { get; private set; }

    public async void SignInAnonymously()
    {
      await AuthenticationService.Instance.SignInAnonymouslyAsync();
      PlayerId = AuthenticationService.Instance.PlayerId;
      Debug.Log($"Signed in. Player ID: {PlayerId}");

      SceneManager.LoadSceneAsync("Lobby");
    }
  }
}