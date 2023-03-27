using System.Collections.Generic;
using Kulinaria.Tools.BattleTrier.Runtime.Data;
using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Registry
{
  public class LocalCharacterRegistry : ICharacterRegistry
  {
    public IDictionary<CharacterConfig, int> Characters => _characterGroup;

    private readonly HashSet<CharacterConfig> _selectedConfigs = new();
    private readonly Dictionary<CharacterConfig, int> _characterGroup = new();

    public bool PlayerHasCharactersOfConfig(CharacterConfig config) =>
      _selectedConfigs.Contains(config);

    public void Select(CharacterConfig config, int amount)
    {
      Debug.Log($"{config.Name} added by {amount.ToString()}");
      if (!_selectedConfigs.Contains(config))
        _selectedConfigs.Add(config);

      if (!_characterGroup.ContainsKey(config))
        _characterGroup.Add(config, amount);
      else
        _characterGroup[config] += amount;
    }
  }
}