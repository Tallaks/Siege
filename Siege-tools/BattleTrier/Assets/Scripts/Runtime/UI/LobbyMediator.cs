using Kulinaria.Tools.BattleTrier.Runtime.Network.Authentication;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Kulinaria.Tools.BattleTrier.Runtime.UI
{
  public class LobbyMediator : MonoBehaviour
  {
    [SerializeField] private Button _createButton;
    [SerializeField] private Button _joinByCodeButton;
    [SerializeField] private Button _joinButton;

    [SerializeField] private GameObject _joinPanel;
    [SerializeField] private TMP_InputField _codeInputField;


    private LobbyService _lobbyService;

    [Inject]
    private void Construct(LobbyService lobbyService)
    {
      _lobbyService = lobbyService;
    }

    public void Initialize()
    {
      _joinPanel.SetActive(false);

      _createButton.onClick.AddListener(() => _lobbyService.CreateGame());
      _joinByCodeButton.onClick.AddListener(() => _joinPanel.SetActive(true));
      _joinButton.onClick.AddListener(() => _lobbyService.JoinGame(_codeInputField.text));
    }
  }
}