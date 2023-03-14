using System;
using System.Collections.Generic;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.UI;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Gameplay;
using Unity.Netcode;
using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.StateMachine
{
  public class StateMachine
  {
    private readonly NetworkManager _networkManager;
    private readonly GameplayMediator _mediator;
    private readonly MapSelectionNetwork _mapSelectionNetwork;

    private Dictionary<Type, IExitState> _states;

    public IExitState CurrentState { get; private set; }

    public StateMachine(NetworkManager networkManager, GameplayMediator mediator, MapSelectionNetwork mapSelectionNetwork)
    {
      _networkManager = networkManager;
      _mediator = mediator;
      _mapSelectionNetwork = mapSelectionNetwork;
    }

    public void Initialize()
    {
      _states = new Dictionary<Type, IExitState>()
      {
        [typeof(MapSelectionState)] = new MapSelectionState(this, _networkManager, _mediator, _mapSelectionNetwork),
        [typeof(CharacterSelectionState)] = new CharacterSelectionState()
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