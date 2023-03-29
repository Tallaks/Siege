using System;
using System.Collections.Generic;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Network;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Maps.Selection.Network;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.UI;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Roles;
using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.StateMachine
{
  public class StateMachine
  {
    private readonly RoleBase _role;
    private readonly GameplayMediator _mediator;
    private readonly MapSelectionNetwork _mapSelectionNetwork;
    private readonly CharacterSelectionNetwork _characterSelectionNetwork;

    private Dictionary<Type, IExitState> _states;

    public IExitState CurrentState { get; private set; }

    public StateMachine(
      RoleBase role,
      GameplayMediator mediator,
      MapSelectionNetwork mapSelectionNetwork,
      CharacterSelectionNetwork characterSelectionNetwork)
    {
      _role = role;
      _mediator = mediator;
      _mapSelectionNetwork = mapSelectionNetwork;
      _characterSelectionNetwork = characterSelectionNetwork;
    }

    public void Initialize()
    {
      _states = new Dictionary<Type, IExitState>()
      {
        [typeof(MapSelectionState)] = new MapSelectionState(this, _role, _mediator, _mapSelectionNetwork),
        [typeof(CharacterSelectionState)] = new CharacterSelectionState(this, _role, _mediator, _characterSelectionNetwork)
      };
    }

    public void Enter<TState>() where TState : ParameterlessState
    {
      var state = ChangeState<TState>();
      state.Enter();
    }

    private TState ChangeState<TState>() where TState : class, IExitState
    {
      CurrentState?.Exit();
      var state = GetState<TState>();

      Debug.Log($"Changed connection state from {CurrentState?.GetType().Name} to {state.GetType().Name}.");
      CurrentState = state;

      return state;
    }

    private TState GetState<TState>() where TState : class, IExitState =>
      _states[typeof(TState)] as TState;
  }
}