using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.UI;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Roles;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.States
{
  public class BattleInitializationState : ParameterlessState
  {
    private readonly GameplayMediator _gameplayMediator;
    private readonly RoleState _role;

    public BattleInitializationState(GameplayMediator gameplayMediator) =>
      _gameplayMediator = gameplayMediator;

    public override void Enter()
    {
      if (_role == RoleState.ChosenSpectator)
        _gameplayMediator.ShowSpectatorBattleUi();
      else
        _gameplayMediator.ShowActivePlayerBattleUi();
    }

    public override void Exit()
    {
    }
  }
}