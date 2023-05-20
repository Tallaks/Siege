using System.Collections.Generic;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Selection
{
  public interface ICharacterSelection
  {
    IDictionary<int, int> Characters { get; }
    void AddCharacter(int configId, int amount);
    bool PlayerHasCharactersOfConfig(int configId);
    void RemoveCharacter(int configId, int amount);
  }
}