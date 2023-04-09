using System.Collections.Generic;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Data;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Factory;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Selection;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.States
{
  public class PlacingCharactersState : ParameterlessState
  {
    private readonly ICharacterSelection _characterSelection;
    private readonly ICharacterFactory _characterFactory;

    public PlacingCharactersState(ICharacterSelection characterSelection, ICharacterFactory characterFactory)
    {
      _characterSelection = characterSelection;
      _characterFactory = characterFactory;
    }

    public override void Enter()
    {
      foreach (KeyValuePair<CharacterConfig, int> characterGroup in _characterSelection.Characters)
      {
        for (var i = 0; i < characterGroup.Value; i++)
          _characterFactory.Create(characterGroup.Key);
      }
    }

    public override void Exit()
    {
      
    }
  }
}