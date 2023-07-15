using System;
using System.Collections.Generic;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Network;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Placer;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Registry;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.UI;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Roles;
using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.States
{
  public class StateMachine
  {
    private readonly CharacterRegistryNetwork _characterRegistryNetwork;
    private readonly ICharacterRegistry _characterRegistry;
    private readonly GameplayMediator _mediator;
    private readonly RoleBase _role;
    private readonly ICharacterPlacer _characterPlacer;

    public IExitState CurrentState { get; private set; }

    private Dictionary<Type, IExitState> _states;

    public StateMachine(RoleBase role,
      ICharacterRegistry characterRegistry,
      GameplayMediator mediator,
      CharacterRegistryNetwork characterRegistryNetwork,
      ICharacterPlacer characterPlacer)
    {
      _role = role;
      _characterRegistry = characterRegistry;
      _mediator = mediator;
      _characterRegistryNetwork = characterRegistryNetwork;
      _characterPlacer = characterPlacer;
    }

    public void Initialize() =>
      _states = new Dictionary<Type, IExitState>
      {
        [typeof(MapSelectionState)] = new MapSelectionState(_role, _mediator),
        [typeof(CharacterSelectionState)] =
          new CharacterSelectionState(_characterRegistry, _characterRegistryNetwork, _role, _mediator),
        [typeof(PlacingFirstPlayerCharactersState)] =
          new PlacingFirstPlayerCharactersState(_mediator, _role, _characterRegistryNetwork),
        [typeof(PlacingSecondPlayerCharactersState)] =
          new PlacingSecondPlayerCharactersState(_mediator, _role, _characterRegistryNetwork, _characterPlacer),
        [typeof(BattleInitializationState)] = new BattleInitializationState()
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