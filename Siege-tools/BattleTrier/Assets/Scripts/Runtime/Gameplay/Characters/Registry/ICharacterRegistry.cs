using System.Collections.Generic;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Registry
{
  public interface ICharacterRegistry
  {
    IReadOnlyDictionary<int, int> CharactersGroupsByConfigId { get; }
    IReadOnlyDictionary<int, Character> CharactersById { get; }
    void AddCharacterGroup(int configId, int amount);
    bool PlayerHasCharactersOfConfig(int configId);
    void RemoveCharacterGroup(int configId, int amount);
    void Register(Character character);
  }
}