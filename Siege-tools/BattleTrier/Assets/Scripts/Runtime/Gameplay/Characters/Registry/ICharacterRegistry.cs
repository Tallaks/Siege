using System.Collections.Generic;
using Kulinaria.Tools.BattleTrier.Runtime.Data;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Registry
{
  public interface ICharacterRegistry
  {
    IDictionary<CharacterConfig, int> Characters { get; }
    bool PlayerHasCharactersOfConfig(CharacterConfig config);
    void Select(CharacterConfig config, int amount);
  }
}