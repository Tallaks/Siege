using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Kulinaria.Tools.BattleTrier.Runtime.UI.LobbyScene
{
  public class LobbyUi : MonoBehaviour
  {
    [SerializeField] private Button _createLobbyButton;
    [SerializeField] private GameObject _lobbyCreationPanel;
    [SerializeField] private Button _openLobbyCreationPanelButton;
    [SerializeField] private TMP_InputField _lobbyNameInput;
    [SerializeField] private TMP_Text _errorSign;
    [SerializeField] private GameObject _blockScreen;
    
    private LobbyMediator _mediator;

    [Inject]
    private void Construct(LobbyMediator mediator)
    {
      _mediator = mediator;
    }

    public void Initialize()
    {
      _blockScreen.SetActive(false);
      _errorSign.gameObject.SetActive(false);
      _lobbyCreationPanel.SetActive(false);
      _openLobbyCreationPanelButton.onClick.AddListener(() => _lobbyCreationPanel.SetActive(true));
      _createLobbyButton.onClick.AddListener(() =>
      {
        if (string.IsNullOrWhiteSpace(_lobbyNameInput.text))
        {
          StartCoroutine(ShowErrorSign());
          return;
        }

        _mediator.TryCreateLobby(_lobbyNameInput.text);
      });
    }
    
    private IEnumerator ShowErrorSign()
    {
      _errorSign.gameObject.SetActive(true);
      yield return new WaitForSeconds(3);
      _errorSign.gameObject.SetActive(false);
    }

    public void Block() => 
      _blockScreen.SetActive(true);

    public void Unblock() => 
      _blockScreen.SetActive(false);
  }
}