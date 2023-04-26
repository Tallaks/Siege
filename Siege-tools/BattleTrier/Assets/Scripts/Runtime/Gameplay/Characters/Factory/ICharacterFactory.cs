using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Network;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Factory
{
  public interface ICharacterFactory
  {
    Character Create(CharacterNetworkData networkData);
  }
}