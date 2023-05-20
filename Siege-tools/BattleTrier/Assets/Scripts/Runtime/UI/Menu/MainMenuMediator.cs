using UnityEngine;
using UnityEngine.SceneManagement;

namespace Kulinaria.Tools.BattleTrier.Runtime.UI.Menu
{
  public class MainMenuMediator : MonoBehaviour
  {
    [SerializeField] private MainMenuUi _mainMenuUi;

    public void Initialize() => _mainMenuUi.Initialize();
    public void GoToLobby() => SceneManager.LoadSceneAsync("Lobby");
    public void HideUntilAuth() => _mainMenuUi.HideMainMenuButtons();
  }
}