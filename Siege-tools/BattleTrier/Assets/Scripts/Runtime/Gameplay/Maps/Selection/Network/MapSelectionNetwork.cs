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
    public NetworkVariable<bool> MapSelected = new();
    private BoardConfig _config;
    private MapNetwork _mapNetwork;
    private GameplayMediator _mediator;

    private StateMachine _stateMachine;

    [Inject]
    private void Construct(GameplayMediator mediator, StateMachine stateMachine, MapNetwork mapNetwork)
    {
      _mediator = mediator;
      _stateMachine = stateMachine;
      _mapNetwork = mapNetwork;
    }

    private void Awake() =>
      MapSelected.OnValueChanged += OnMapSelected;

    [ServerRpc(RequireOwnership = false)]
    private void SaveConfigServerRpc(string configName) =>
      _config = Resources.Load<BoardConfig>("Configs/Boards/" + configName);

    [ServerRpc(RequireOwnership = false)]
    public void SetSelectedServerRpc()
    {
      MapSelected.Value = true;
      _mapNetwork.SpawnTilesClientRpc(_config.name);
      _mapNetwork.InitMapBoardServerRpc();
    }

    public void Select(string configName)
    {
      SaveConfigServerRpc(configName);
      _mediator.EnableMapSubmitButton();
    }

    private void OnMapSelected(bool previousvalue, bool newvalue)
    {
      if (newvalue)
        _stateMachine.Enter<CharacterSelectionState>();
    }
  }
}