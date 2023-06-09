using System.Collections.Generic;
using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Registry
{
  public class CharacterRegistryLocal : ICharacterRegistry
  {
    private readonly Dictionary<int, int> _characterGroupByConfigId = new();
    private readonly Dictionary<int, Character> _charactersById = new();
    private readonly HashSet<int> _selectedConfigs = new();

    public IReadOnlyDictionary<int, int> CharactersGroupsByConfigId => _characterGroupByConfigId;
    public IReadOnlyDictionary<int, Character> CharactersById => _charactersById;

    public bool PlayerHasCharactersOfConfig(int configId) =>
      _selectedConfigs.Contains(configId);

    public void AddCharacterGroup(int configId, int amount)
    {
      Debug.Log($"Id {configId} added by {amount.ToString()}");
      _selectedConfigs.Add(configId);

      if (!_characterGroupByConfigId.ContainsKey(configId))
        _characterGroupByConfigId.Add(configId, amount);
      else
        _characterGroupByConfigId[configId] += amount;
    }

    public void RemoveCharacterGroup(int configId, int amount)
    {
      Debug.Log($"Id {configId} removed by {amount.ToString()}");
      _characterGroupByConfigId[configId] -= amount;
      if (_characterGroupByConfigId[configId] <= 0)
      {
        _characterGroupByConfigId.Remove(configId);
        _selectedConfigs.Remove(configId);
      }
    }

    public void Register(Character character) =>
      _charactersById.Add(character.Id, character);
  }
}