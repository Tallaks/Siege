using System.Collections.Generic;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Network;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Selection;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Roles;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.States
{
  public class PlacingCharactersState : ParameterlessState
  {
    private readonly ICharacterSelection _characterSelection;
    private readonly CharacterRegistryNetwork _characterRegistryNetwork;
    private readonly RoleBase _roleBase;

    public PlacingCharactersState(
      ICharacterSelection characterSelection,
      CharacterRegistryNetwork characterRegistryNetwork, 
      RoleBase roleBase)
    {
      _characterSelection = characterSelection;
      _characterRegistryNetwork = characterRegistryNetwork;
      _roleBase = roleBase;
    }

    public override void Enter()
    {
      foreach (KeyValuePair<int, int> characterGroup in _characterSelection.Characters)
      {
        for (var i = 0; i < characterGroup.Value; i++)
        {
          _characterRegistryNetwork.RegisterByIdServerRpc(characterGroup.Key, _roleBase.State.Value);
        }
      }
    }

    public override void Exit()
    {
    }
  }
}