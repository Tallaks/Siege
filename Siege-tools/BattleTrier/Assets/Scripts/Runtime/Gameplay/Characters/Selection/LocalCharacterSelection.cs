using System.Collections.Generic;
using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Selection
{
  public class LocalCharacterSelection : ICharacterSelection
  {
    private readonly Dictionary<int, int> _characterGroup = new();

    private readonly HashSet<int> _selectedConfigs = new();
    public IDictionary<int, int> Characters => _characterGroup;

    public bool PlayerHasCharactersOfConfig(int configId) =>
      _selectedConfigs.Contains(configId);

    public void AddCharacter(int configId, int amount)
    {
      Debug.Log($"Id {configId} added by {amount.ToString()}");
      _selectedConfigs.Add(configId);

      if (!_characterGroup.ContainsKey(configId))
        _characterGroup.Add(configId, amount);
      else
        _characterGroup[configId] += amount;
    }

    public void RemoveCharacter(int configId, int amount)
    {
      Debug.Log($"Id {configId} removed by {amount.ToString()}");
      _characterGroup[configId] -= amount;
      if (_characterGroup[configId] <= 0)
      {
        _characterGroup.Remove(configId);
        _selectedConfigs.Remove(configId);
      }
    }
  }
}