using System;
using System.Collections.Generic;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.UI;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Roles;
using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.States
{
  public class StateMachine
  {
    private readonly RoleBase _role;
    private readonly GameplayMediator _mediator;

    private Dictionary<Type, IExitState> _states;

    public IExitState CurrentState { get; private set; }

    public StateMachine(
      RoleBase role,
      GameplayMediator mediator)
    {
      _role = role;
      _mediator = mediator;
    }

    public void Initialize()
    {
      _states = new Dictionary<Type, IExitState>()
      {
        [typeof(MapSelectionState)] = new MapSelectionState(_role, _mediator),
        [typeof(CharacterSelectionState)] = new CharacterSelectionState(_role, _mediator),
        [typeof(PlacingCharactersState)] = new PlacingCharactersState()
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

      Debug.Log($"Changed state from {CurrentState?.GetType().Name} to {state.GetType().Name}.");
      CurrentState = state;

      return state;
    }

    private TState GetState<TState>() where TState : class, IExitState =>
      _states[typeof(TState)] as TState;
  }
}