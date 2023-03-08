using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.StateMachine;
using Unity.Netcode;
using Zenject;

namespace Kulinaria.Tools.BattleTrier.Runtime.Network.Gameplay
{
  public class MapSelectionBehaviour : NetworkBehaviour
  {
    private StateMachine _stateMachine;

    public NetworkVariable<bool> MapSelected = new();

    [Inject]
    private void Construct(StateMachine stateMachine) => 
      _stateMachine = stateMachine;

    private void Awake() => 
      MapSelected.OnValueChanged += OnMapSelected;

    private void OnMapSelected(bool previousvalue, bool newvalue)
    {
      if(newvalue == true)
        _stateMachine.Enter<CharacterSelectionState>();
    }
  }
}