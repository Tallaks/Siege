using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.States;
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

    [Inject]
    private void Construct(StateMachine gameStateMachine) =>
      _gameStateMachine = gameStateMachine;

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
      if (newValue == true && _secondPlayerReady.Value == true)
        _gameStateMachine.Enter<PlacingCharactersState>();
    }

    private void OnSecondPlayerReady(bool previousValue, bool newValue)
    {
      if (newValue == true && _firstPlayerReady.Value == true)
        _gameStateMachine.Enter<PlacingCharactersState>();
    }
  }
}