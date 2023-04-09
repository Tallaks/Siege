using System.Collections.Generic;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Data;
using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Services.Data
{
  public class StaticDataProvider : IStaticDataProvider
  {
    private const string ConfigsCharactersPath = "Configs/Characters/";
    public IEnumerable<CharacterConfig> GetAllCharacterConfigs() =>
      Resources.LoadAll<CharacterConfig>(ConfigsCharactersPath);
  }
}