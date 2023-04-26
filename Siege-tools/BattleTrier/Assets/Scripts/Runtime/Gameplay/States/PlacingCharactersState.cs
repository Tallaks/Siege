using System.Collections;
using System.Collections.Generic;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Factory;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Network;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Selection;
using Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Services.Coroutines;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Roles;
using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.States
{
  public class PlacingCharactersState : ParameterlessState
  {
    private readonly ICoroutineRunner _coroutineRunner;
    private readonly ICharacterSelection _characterSelection;
    private readonly ICharacterFactory _characterFactory;
    private readonly CharacterRegistryNetwork _characterRegistryNetwork;
    private readonly RoleBase _roleBase;

    public PlacingCharactersState(
      ICoroutineRunner coroutineRunner,
      ICharacterSelection characterSelection,
      ICharacterFactory characterFactory,
      CharacterRegistryNetwork characterRegistryNetwork,
      RoleBase roleBase)
    {
      _coroutineRunner = coroutineRunner;
      _characterSelection = characterSelection;
      _characterFactory = characterFactory;
      _characterRegistryNetwork = characterRegistryNetwork;
      _roleBase = roleBase;
    }

    public override void Enter()
    {
      foreach (KeyValuePair<int, int> characterGroup in _characterSelection.Characters)
        for (var i = 0; i < characterGroup.Value; i++)
          _characterRegistryNetwork.RegisterByIdServerRpc(characterGroup.Key, _roleBase.State.Value);

      _coroutineRunner.StartCoroutine(WaitForCharacterRegistration());
    }

    public override void Exit()
    {
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