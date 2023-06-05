using System;
using System.Collections.Generic;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Factory;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Network;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Registry;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.UI;
using Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Services.Coroutines;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Roles;
using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.States
{
  public class StateMachine
  {
    private readonly ICharacterFactory _characterFactory;
    private readonly CharacterRegistryNetwork _characterRegistryNetwork;
    private readonly ICharacterRegistry _characterRegistry;
    private readonly ICoroutineRunner _coroutineRunner;
    private readonly GameplayMediator _mediator;
    private readonly RoleBase _role;

    public IExitState CurrentState { get; private set; }

    private Dictionary<Type, IExitState> _states;

    public StateMachine(
      ICoroutineRunner coroutineRunner,
      RoleBase role,
      ICharacterRegistry characterRegistry,
      ICharacterFactory characterFactory,
      GameplayMediator mediator,
      CharacterRegistryNetwork characterRegistryNetwork)
    {
      _coroutineRunner = coroutineRunner;
      _role = role;
      _characterRegistry = characterRegistry;
      _characterFactory = characterFactory;
      _mediator = mediator;
      _characterRegistryNetwork = characterRegistryNetwork;
    }

    public void Initialize() =>
      _states = new Dictionary<Type, IExitState>
      {
        [typeof(MapSelectionState)] = new MapSelectionState(_role, _mediator),
        [typeof(CharacterSelectionState)] = new CharacterSelectionState(_coroutineRunner, _characterRegistry,
          _characterFactory, _characterRegistryNetwork, _role, _mediator),
        [typeof(PlacingCharactersState)] = new PlacingCharactersState(_mediator, _role)
      };

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