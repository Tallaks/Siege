using System.Collections.Generic;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Registry
{
  public interface ICharacterRegistry
  {
    IReadOnlyDictionary<int, int> CharactersGroupsByConfigId { get; }
    IReadOnlyDictionary<int, Character> CharactersById { get; }
    IReadOnlyDictionary<int, Enemy> EnemiesById { get; }
    void AddCharacterGroup(int configId, int amount);
    void RemoveCharacterGroup(int configId, int amount);
    void AddEnemy(Enemy enemy);
    void RemoveEnemy(int id);
    void AddCharacter(Character character);
    void RemoveCharacter(int id);
    bool PlayerHasCharactersOfConfig(int configId);
  }
}