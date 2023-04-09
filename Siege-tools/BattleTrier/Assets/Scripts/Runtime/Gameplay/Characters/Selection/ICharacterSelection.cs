using System.Collections.Generic;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Selection
{
  public interface ICharacterSelection
  {
    IDictionary<int, int> Characters { get; }
    bool PlayerHasCharactersOfConfig(int configId);
    void AddCharacter(int configId, int amount);
    void RemoveCharacter(int configId, int amount);
  }
}