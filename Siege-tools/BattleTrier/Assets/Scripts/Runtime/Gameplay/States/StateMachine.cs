using System;
using System.Collections.Generic;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Factory;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Network;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Selection;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.UI;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Roles;
using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.States
{
  public class StateMachine
  {
    private readonly RoleBase _role;
    private readonly ICharacterSelection _characterSelection;
    private readonly GameplayMediator _mediator;
    private readonly CharacterRegistryNetwork _characterRegistryNetwork;

    private Dictionary<Type, IExitState> _states;

    public IExitState CurrentState { get; private set; }

    public StateMachine(
      RoleBase role,
      ICharacterSelection characterSelection,
      GameplayMediator mediator,
      CharacterRegistryNetwork characterRegistryNetwork)
    {
      _role = role;
      _characterSelection = characterSelection;
      _mediator = mediator;
      _characterRegistryNetwork = characterRegistryNetwork;
    }

    public void Initialize()
    {
      _states = new Dictionary<Type, IExitState>
      {
        [typeof(MapSelectionState)] = new MapSelectionState(_role, _mediator),
        [typeof(CharacterSelectionState)] = new CharacterSelectionState(_role, _mediator),
        [typeof(PlacingCharactersState)] = new PlacingCharactersState(_characterSelection, _characterRegistryNetwork, _role)
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