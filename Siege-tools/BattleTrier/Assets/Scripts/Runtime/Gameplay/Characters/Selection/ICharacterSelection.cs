using System.Collections.Generic;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Data;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Selection
{
  public interface ICharacterSelection
  {
    IDictionary<CharacterConfig, int> Characters { get; }
    bool PlayerHasCharactersOfConfig(CharacterConfig config);
    void AddCharacter(CharacterConfig config, int amount);
    void RemoveCharacter(CharacterConfig config, int amount);
  }
}