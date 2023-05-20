using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Maps.Data;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.States;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.UI;
using Unity.Netcode;
using UnityEngine;
using Zenject;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Maps.Selection.Network
{
  public class MapSelectionNetwork : NetworkBehaviour
  {
    [SerializeField] private Map _mapPrefab;

    public NetworkVariable<bool> MapSelected = new();
    private BoardConfig _config;
    private GameplayMediator _mediator;

    private StateMachine _stateMachine;

    [Inject]
    private void Construct(GameplayMediator mediator, StateMachine stateMachine)
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
      map.SpawnTilesClientRpc(_config.name);
      map.InitMapBoardServerRpc();
    }

    public void Select(string configName)
    {
      SaveConfigServerRpc(configName);
      _mediator.EnableMapSubmitButton();
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