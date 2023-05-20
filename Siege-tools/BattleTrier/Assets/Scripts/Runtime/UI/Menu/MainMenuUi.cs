using System.Collections;
using Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Services.Applications;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Kulinaria.Tools.BattleTrier.Runtime.UI.Menu
{
  public class MainMenuUi : MonoBehaviour
  {
    [SerializeField] private Button _createButton;
    [SerializeField] private TMP_Text _errorSignText;
    [SerializeField] private Transform _parent;

    [SerializeField] private TMP_InputField _playerProfileName;
    [SerializeField] private Button _quitButton;
    private IApplicationService _applicationService;
    private UserProfile _localUser;
    private MainMenuMediator _menuMediator;

    [Inject]
    private void Construct(IApplicationService applicationService, MainMenuMediator menuMediator, UserProfile localUser)
    {
      _applicationService = applicationService;
      _localUser = localUser;
      _menuMediator = menuMediator;
    }

    public void Initialize()
    {
      _parent.gameObject.SetActive(true);
      _errorSignText.gameObject.SetActive(false);
      _errorSignText.gameObject.SetActive(false);

      _playerProfileName.onValueChanged.AddListener(text => _localUser.Name = text);

      _quitButton.onClick.AddListener(_applicationService.QuitApplication);

      _createButton.onClick.AddListener(() =>
      {
        if (string.IsNullOrWhiteSpace(_playerProfileName.text))
        {
          StartCoroutine(ShowErrorSign());
          return;
        }

        _menuMediator.GoToLobby();
        HideMainMenuButtons();
      });
    }

    private IEnumerator ShowErrorSign()
    {
      _errorSignText.gameObject.SetActive(true);
      yield return new WaitForSeconds(3);
      _errorSignText.gameObject.SetActive(false);
    }

    public void HideMainMenuButtons() =>
      _parent.gameObject.SetActive(false);
  }
}