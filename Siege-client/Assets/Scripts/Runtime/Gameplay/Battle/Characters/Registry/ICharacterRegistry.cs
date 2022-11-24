using Kulinaria.Siege.Runtime.Gameplay.Battle.Characters.Enemies;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Characters.Players;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Characters.Registry
{
	public interface ICharacterRegistry
	{
		void RegisterPlayer(BasePlayer player);
		void RegisterEnemy(BaseEnemy enemy);
	}
}