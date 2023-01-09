using UnityEngine;
using UnityEngine.SceneManagement;

namespace Kulinaria.Tools.BattleTrier.Runtime.UI.Menu
{
  public class MainMenuMediator : MonoBehaviour
  {
    [SerializeField] private MainMenuUi _mainMenuUi;

    public void Initialize() => _mainMenuUi.Initialize();
    public void HideUntilAuth() => _mainMenuUi.HideMainMenuButtons();
    public void GoToLobby() => SceneManager.LoadSceneAsync("Lobby");
  }
}