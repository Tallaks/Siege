using System.Collections.Generic;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Data;

namespace Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Services.Data
{
  public interface IStaticDataProvider
  {
    CharacterConfig ConfigById(int id);
    IEnumerable<int> GetAllCharacterConfigIds();
  }
}