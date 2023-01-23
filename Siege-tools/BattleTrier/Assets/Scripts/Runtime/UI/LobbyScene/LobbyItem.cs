using Kulinaria.Tools.BattleTrier.Runtime.Network.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Kulinaria.Tools.BattleTrier.Runtime.UI.LobbyScene
{
  public class LobbyItem : MonoBehaviour
  {
    [SerializeField] private TMP_Text _name;
    [SerializeField] private Button _joinButton;
    [SerializeField] private LobbyInfo _lobby;

    private LobbyMediator _mediator;

    [Inject]
    private void Construct(LobbyMediator mediator) => 
      _mediator = mediator;

    public void Initialize(LobbyInfo lobby)
    {
      _lobby = lobby;
      _name.text = lobby.Name;
      _joinButton.onClick.AddListener(() => _mediator.JoinLobbyRequest(_lobby));
    }

    private void OnDestroy() => 
      _joinButton.onClick.RemoveAllListeners();
  }
}