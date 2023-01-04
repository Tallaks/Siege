using Unity.Services.Authentication;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Kulinaria.Tools.BattleTrier.Network.Authentication
{
  public class SignInService
  {
    private string PlayerId { get; set; }

    public async void SignInAnonymously()
    {
      await AuthenticationService.Instance.SignInAnonymouslyAsync();
      PlayerId = AuthenticationService.Instance.PlayerId;
      Debug.Log($"Signed in. Player ID: {PlayerId}");

      SceneManager.LoadSceneAsync("Lobby");
    }
  }
}