using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.States;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.UI;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Roles;
using Unity.Netcode;
using Zenject;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Network
{
  public class CharacterSelectionNetwork : NetworkBehaviour
  {
    private readonly NetworkVariable<bool> _firstPlayerReady = new();
    private readonly NetworkVariable<bool> _secondPlayerReady = new();

    private StateMachine _gameStateMachine;
    private GameplayMediator _mediator;

    [Inject]
    private void Construct(StateMachine gameStateMachine, GameplayMediator mediator)
    {
      _gameStateMachine = gameStateMachine;
      _mediator = mediator;
    }

    private void Awake()
    {
      _firstPlayerReady.OnValueChanged += OnFirstPlayerReady;
      _secondPlayerReady.OnValueChanged += OnSecondPlayerReady;
    }

    [ServerRpc(RequireOwnership = false)]
    public void SubmitSelectionServerRpc(int roleId)
    {
      if (roleId == (int)RoleState.ChosenFirst)
        _firstPlayerReady.Value = true;
      else
        _secondPlayerReady.Value = true;
    }

    private void OnFirstPlayerReady(bool previousValue, bool newValue)
    {
      if (newValue && _secondPlayerReady.Value)
        _gameStateMachine.Enter<PlacingCharactersState>();
      else
        _mediator.ChangeCharacterSelectionUiOnFirstPlayerReady();
    }

    private void OnSecondPlayerReady(bool previousValue, bool newValue)
    {
      if (newValue && _firstPlayerReady.Value)
        _gameStateMachine.Enter<PlacingCharactersState>();
      else
        _mediator.ChangeCharacterSelectionUiOnSecondPlayerReady();
    }
  }
}