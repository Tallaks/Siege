using System.Collections.Generic;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Characters.Enemies;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Characters.Players;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Characters.Registry
{
	public interface ICharacterRegistry
	{
		IEnumerable<BaseEnemy> Enemies { get; }
		IEnumerable<BasePlayer> Players { get; }
		void RegisterPlayer(BasePlayer player);
		void RegisterEnemy(BaseEnemy enemy);
		void ChangeActionPointsForAll(int newValue);
	}
}