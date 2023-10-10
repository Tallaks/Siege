using System.Collections.Generic;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Characters.Enemies;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Characters.Players;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Characters.Registry
{
	public class CharacterRegistry : ICharacterRegistry
	{
		private readonly List<BasePlayer> _players = new();
		private readonly List<BaseEnemy> _enemies = new();

		public IEnumerable<BasePlayer> Players => _players;
		public IEnumerable<BaseEnemy> Enemies => _enemies;

		public void ChangeActionPointsForAll(int newValue)
		{
			foreach (BasePlayer player in _players)
				player.MaxAP = newValue;

			foreach (BaseEnemy enemy in _enemies)
				enemy.MaxAP = newValue;
		}

		public void RegisterPlayer(BasePlayer player)
		{
			_players.Add(player);
		}

		public void RegisterEnemy(BaseEnemy enemy)
		{
			_enemies.Add(enemy);
		}
	}
}