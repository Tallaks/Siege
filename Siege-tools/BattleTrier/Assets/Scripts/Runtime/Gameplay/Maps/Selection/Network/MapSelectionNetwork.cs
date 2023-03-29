using Kulinaria.Tools.BattleTrier.Runtime.Data;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.StateMachine;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.UI;
using Unity.Netcode;
using UnityEngine;
using Zenject;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Maps.Selection.Network
{
  public class MapSelectionNetwork : NetworkBehaviour
  {
    [SerializeField] private Map _mapPrefab;

    private StateMachine.StateMachine _stateMachine;
    private GameplayMediator _mediator;

    public NetworkVariable<bool> MapSelected = new();
    private BoardConfig _config;

    [Inject]
    private void Construct(GameplayMediator mediator, StateMachine.StateMachine stateMachine)
    {
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

    public void Select(string configName)
    {
      SaveConfigServerRpc(configName);
      _mediator.EnableSubmitButton();
    }

    [ServerRpc(RequireOwnership = false)]
    private void SaveConfigServerRpc(string configName) => 
      _config = Resources.Load<BoardConfig>("Configs/Boards/" + configName);

    private void OnMapSelected(bool previousvalue, bool newvalue)
    {
      if (newvalue)
        _stateMachine.Enter<CharacterSelectionState>();
    }
  }
}