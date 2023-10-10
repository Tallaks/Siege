using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.UI;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Roles;
using Unity.Netcode;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.States
{
  public class BattleInitializationState : ParameterlessState
  {
    private readonly GameplayMediator _gameplayMediator;
    private readonly NetworkManager _networkManager;

    public BattleInitializationState(NetworkManager networkManager, GameplayMediator gameplayMediator)
    {
      _networkManager = networkManager;
      _gameplayMediator = gameplayMediator;
    }

    public override void Enter()
    {
      if (_networkManager.LocalClient.PlayerObject.GetComponent<NetworkPlayerObject>().State.Value == RoleState.ChosenSpectator)
        _gameplayMediator.ShowSpectatorBattleUi();
      else
        _gameplayMediator.ShowActivePlayerBattleUi();
    }

    public override void Exit()
    {
    }
  }
}