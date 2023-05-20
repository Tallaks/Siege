using System.Collections;
using System.Collections.Generic;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Factory;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Network;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Selection;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.UI;
using Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Services.Coroutines;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Roles;
using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.States
{
  public class CharacterSelectionState : ParameterlessState
  {
    private readonly ICharacterFactory _characterFactory;
    private readonly CharacterRegistryNetwork _characterRegistryNetwork;
    private readonly ICharacterSelection _characterSelection;
    private readonly ICoroutineRunner _coroutineRunner;
    private readonly GameplayMediator _mediator;
    private readonly RoleBase _role;

    public CharacterSelectionState(
      ICoroutineRunner coroutineRunner,
      ICharacterSelection characterSelection,
      ICharacterFactory characterFactory,
      CharacterRegistryNetwork characterRegistryNetwork,
      RoleBase role,
      GameplayMediator mediator)
    {
      _coroutineRunner = coroutineRunner;
      _characterSelection = characterSelection;
      _characterFactory = characterFactory;
      _characterRegistryNetwork = characterRegistryNetwork;
      _role = role;
      _mediator = mediator;
    }

    public override void Enter()
    {
      Debug.Log("Entering character selection state");
      _mediator.InitializeCharacterSelectionUi(_role.State.Value);
    }

    public override void Exit()
    {
      Debug.Log("Exiting character selection state");
      _mediator.HideCharacterSelectionUi();

      foreach (KeyValuePair<int, int> characterGroup in _characterSelection.Characters)
        for (var i = 0; i < characterGroup.Value; i++)
          _characterRegistryNetwork.RegisterByIdServerRpc(characterGroup.Key, _role.State.Value);

      _coroutineRunner.StartCoroutine(WaitForCharacterRegistration());
    }

    private IEnumerator WaitForCharacterRegistration()
    {
      yield return new WaitUntil(() => _characterRegistryNetwork.FirstPlayerCharacters.Count > 0 &&
                                       _characterRegistryNetwork.SecondPlayerCharacters.Count > 0);

      foreach (CharacterNetworkData firstPlayerCharacter in _characterRegistryNetwork.FirstPlayerCharacters)
        _characterFactory.Create(firstPlayerCharacter);

      foreach (CharacterNetworkData secondPlayerCharacter in _characterRegistryNetwork.SecondPlayerCharacters)
        _characterFactory.Create(secondPlayerCharacter);
    }
  }
}