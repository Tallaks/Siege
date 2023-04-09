using System.Collections.Generic;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Data;
using UnityEngine;
using Zenject;

namespace Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Services.Data
{
  public class StaticDataProvider : IStaticDataProvider, IInitializable
  {
    private const string ConfigsCharactersPath = "Configs/Characters/";

    private Dictionary<int, CharacterConfig> _configsById = new();

    public void Initialize()
    {
      _configsById = new Dictionary<int, CharacterConfig>();
      CharacterConfig[] configs = Resources.LoadAll<CharacterConfig>(ConfigsCharactersPath);
      foreach (CharacterConfig config in configs)
        _configsById[config.Id] = config;
    }

    public IEnumerable<int> GetAllCharacterConfigIds() =>
      _configsById.Keys;

    public CharacterConfig ConfigById(int id) =>
      _configsById[id];
  }
}