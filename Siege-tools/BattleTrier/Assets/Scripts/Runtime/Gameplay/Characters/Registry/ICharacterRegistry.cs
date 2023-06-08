using System.Collections.Generic;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Registry
{
  public interface ICharacterRegistry
  {
    IReadOnlyDictionary<int, int> Characters { get; }
    IEnumerable<int> CharacterConfigsIds { get; }
    void AddCharacter(int configId, int amount);
    bool PlayerHasCharactersOfConfig(int configId);
    void RemoveCharacter(int configId, int amount);
  }
}