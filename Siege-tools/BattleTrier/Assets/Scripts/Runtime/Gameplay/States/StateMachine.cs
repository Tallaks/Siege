using System;
using System.Collections.Generic;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Network;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Placer;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Registry;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Selection.Placement;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Maps;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.UI;
using Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Services.Coroutines;
using Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Services.Scenes;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Data;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Session;
using Unity.Netcode;
using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.States
{
  public class StateMachine
  {
    private readonly ICoroutineRunner _coroutineRunner;
    private readonly ISceneLoader _sceneLoader;
    private readonly ICharacterRegistry _characterRegistry;
    private readonly GameplayMediator _mediator;
    private readonly CharacterRegistryNetwork _characterRegistryNetwork;
    private readonly MapNetwork _mapNetwork;
    private readonly Session<SessionPlayerData> _session;
    private readonly ICharacterPlacer _characterPlacer;
    private readonly IPlacementSelection _placementSelection;

    public IExitState CurrentState { get; private set; }
    private readonly NetworkManager _networkManager;

    private Dictionary<Type, IExitState> _states;

    public StateMachine(
      ICoroutineRunner coroutineRunner,
      ISceneLoader sceneLoader,
      ICharacterRegistry characterRegistry,
      NetworkManager networkManager,
      GameplayMediator mediator,
      CharacterRegistryNetwork characterRegistryNetwork,
      MapNetwork mapNetwork,
      Session<SessionPlayerData> session,
      ICharacterPlacer characterPlacer,
      IPlacementSelection placementSelection)
    {
      _coroutineRunner = coroutineRunner;
      _sceneLoader = sceneLoader;
      _characterRegistry = characterRegistry;
      _networkManager = networkManager;
      _mediator = mediator;
      _characterRegistryNetwork = characterRegistryNetwork;
      _mapNetwork = mapNetwork;
      _session = session;
      _characterPlacer = characterPlacer;
      _placementSelection = placementSelection;
    }

    public void Initialize() =>
      _states = new Dictionary<Type, IExitState>
      {
        [typeof(RoleSelectionState)] = new RoleSelectionState(_sceneLoader),
        [typeof(MapSelectionState)] = new MapSelectionState(_coroutineRunner, _session, _networkManager, _mediator),
        [typeof(CharacterSelectionState)] = new CharacterSelectionState(
          _characterRegistry, _placementSelection, _networkManager, _characterRegistryNetwork, _mediator),
        [typeof(PlacingFirstPlayerCharactersState)] = new PlacingFirstPlayerCharactersState(
          _placementSelection, _mediator, _networkManager, _mapNetwork, _characterRegistryNetwork),
        [typeof(PlacingSecondPlayerCharactersState)] = new PlacingSecondPlayerCharactersState(
          _placementSelection, _mediator, _networkManager, _mapNetwork, _characterRegistryNetwork, _characterPlacer),
        [typeof(BattleInitializationState)] = new BattleInitializationState(_networkManager, _mediator)
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