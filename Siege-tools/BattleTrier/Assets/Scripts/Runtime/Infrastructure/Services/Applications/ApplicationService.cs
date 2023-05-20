using System;
using System.Collections;
using Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Services.Coroutines;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Data;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Lobbies;
using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Services.Applications
{
  public class ApplicationService : IApplicationService
  {
    private readonly LobbyInfo _lobbyInfo;
    private readonly LobbyServiceFacade _lobbyServiceFacade;
    private readonly ICoroutineRunner _runner;

    public ApplicationService(ICoroutineRunner runner, LobbyInfo lobbyInfo, LobbyServiceFacade lobbyServiceFacade)
    {
      _runner = runner;
      _lobbyInfo = lobbyInfo;
      _lobbyServiceFacade = lobbyServiceFacade;
    }

    public void Initialize() =>
      Application.wantsToQuit += OnWantToQuit;

    public void QuitApplication()
    {
#if UNITY_EDITOR
      UnityEditor.EditorApplication.isPlaying = false;
#else
      Application.Quit();
#endif
    }

    private bool OnWantToQuit()
    {
      bool canQuit = string.IsNullOrEmpty(_lobbyInfo?.Id);
      if (!canQuit)
        _runner.StartCoroutine(LeaveBeforeQuit());
      return canQuit;
    }

    private IEnumerator LeaveBeforeQuit()
    {
      try
      {
        _lobbyServiceFacade.EndTracking();
      }
      catch (Exception e)
      {
        Debug.LogError(e.Message);
      }

      yield return null;
      QuitApplication();
    }
  }
}