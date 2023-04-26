using System.Collections.Generic;
using System.Linq;
using Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Services.Coroutines;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Data;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Kulinaria.Tools.BattleTrier.Runtime.UI.LobbyScene
{
  public class LobbyList : MonoBehaviour
  {
    [SerializeField] private LobbyItem _lobbyItemPrefab;
    [SerializeField] private Transform _content;
    [SerializeField] private Button _refreshButton;

    private IUpdateRunner _updateRunner;
    private List<LobbyItem> _lobbyItems = new();
    private DiContainer _container;
    private LobbyMediator _mediator;

    [Inject]
    private void Construct(DiContainer container, IUpdateRunner updateRunner, LobbyMediator mediator)
    {
      _mediator = mediator;
      _container = container;
      _updateRunner = updateRunner;
    }

    public void Initialize()
    {
      _refreshButton.onClick.AddListener(() => _mediator.QueryLobbiesRequest(true));
      _updateRunner.Subscribe(Refresh, 10);
    }

    private void OnDestroy()
    {
      _refreshButton.onClick.RemoveAllListeners();
      _updateRunner.Unsubscribe(Refresh);
    }

    public void UpdateList(IEnumerable<Lobby> lobbies)
    {
      ClearList();

      foreach (Lobby lobby in lobbies.Distinct())
      {
        var item = _container.InstantiatePrefabForComponent<LobbyItem>(_lobbyItemPrefab, _content);
        var lobbyInfo = new LobbyInfo(lobby);
        item.Initialize(lobbyInfo);
        _lobbyItems.Add(item);
      }
    }

    private void ClearList()
    {
      foreach (LobbyItem item in _lobbyItems)
        Destroy(item.gameObject);

      _lobbyItems = new List<LobbyItem>();
    }

    private void Refresh() =>
      _mediator.QueryLobbiesRequest(false);
  }
}