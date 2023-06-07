using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Factory
{
  public interface ICharacterFactory
  {
    Character Create(int id, Vector2Int position);
  }
}