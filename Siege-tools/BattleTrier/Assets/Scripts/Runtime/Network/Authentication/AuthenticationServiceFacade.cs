using System;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Runtime.Network.Authentication
{
  public class AuthenticationServiceFacade
  {
    public string PlayerId => AuthenticationService.Instance.PlayerId;

    public async Task SignInAnonymously()
    {
      try
      {
        await UnityServices.InitializeAsync();

        if (!AuthenticationService.Instance.IsSignedIn)
        {
          await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }
      }
      catch (Exception e)
      {
        var reason = $"{e.Message} ({e.InnerException?.Message})";
        Debug.Log($"Authentication Error : {reason}");
        throw;
      }
    }
  }
}