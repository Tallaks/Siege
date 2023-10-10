using Kulinaria.Siege.Runtime.Debugging.Logging;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Characters.Players;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Spawn;
using UnityEngine;
using Zenject;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Characters.Factory
{
	public class PlayerFactory : PrefabFactory<BasePlayer>
	{
		private readonly ILoggerService _loggerService;
		private readonly DiContainer _container;

		public PlayerFactory(DiContainer container, ILoggerService loggerService)
		{
			_container = container;
			_loggerService = loggerService;
		}

		public BasePlayer Create(PlayerSlot slot)
		{
			var player = _container.InstantiatePrefabForComponent<BasePlayer>(
				slot.Player.Prefab,
				slot.Spawn.transform.position,
				Quaternion.identity,
				null);

			player.MaxHP = slot.Player.HealthPoints;
			player.MaxAP = slot.Player.ActionPoints;
			player.Name = slot.Player.Name;

			slot.Spawn.Tile.RegisterVisitor(player);

			_loggerService.Log($"Created: {player}", LoggerLevel.Characters);
			return player;
		}
	}
}