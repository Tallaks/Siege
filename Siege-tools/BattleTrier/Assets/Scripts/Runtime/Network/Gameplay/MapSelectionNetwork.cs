using Kulinaria.Tools.BattleTrier.Runtime.Data;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Maps;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.StateMachine;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.UI;
using Unity.Netcode;
using UnityEngine;
using Zenject;

namespace Kulinaria.Tools.BattleTrier.Runtime.Network.Gameplay
{
  public class MapSelectionNetwork : NetworkBehaviour
  {
    [SerializeField] private Map _mapPrefab;
    
    private DiContainer _container;
    private StateMachine _stateMachine;
    private GameplayMediator _mediator;

    public NetworkVariable<bool> MapSelected = new();
    private BoardConfig _config;

    [Inject]
    private void Construct(DiContainer container, GameplayMediator mediator, StateMachine stateMachine)
    {
      _container = container;
      _mediator = mediator;
      _stateMachine = stateMachine;
    }

    private void Awake() =>
      MapSelected.OnValueChanged += OnMapSelected;

    [ServerRpc(RequireOwnership = false)]
    public void SetSelectedServerRpc()
    {
      MapSelected.Value = true;
      Map map = Instantiate(_mapPrefab);
      map.NetworkObject.Spawn();
      map.SpawnTiles(_config.MapTiles);
    }

    public void Select(BoardConfig config)
    {
      _config = config;
      _mediator.EnableSubmitButton();
    }

    private void OnMapSelected(bool previousvalue, bool newvalue)
    {
      if (newvalue)
        _stateMachine.Enter<CharacterSelectionState>();
    }
  }
}