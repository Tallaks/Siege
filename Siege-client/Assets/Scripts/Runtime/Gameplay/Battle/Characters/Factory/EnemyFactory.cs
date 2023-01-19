using Kulinaria.Siege.Runtime.Debugging.Logging;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Characters.Enemies;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Spawn;
using UnityEngine;
using Zenject;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Characters.Factory
{
	public class EnemyFactory : PrefabFactory<BaseEnemy>
	{
		private readonly DiContainer _container;
		private readonly ILoggerService _loggerService;

		public EnemyFactory(DiContainer container, ILoggerService loggerService)
		{
			_container = container;
			_loggerService = loggerService;
		}

		public BaseEnemy Create(EnemySlot spawnTile)
		{
			var enemy = _container.InstantiatePrefabForComponent<BaseEnemy>(
				spawnTile.Enemy.Prefab,
				spawnTile.Spawn.transform.position,
				Quaternion.LookRotation(spawnTile.LookDirection, Vector3.up),
				null);

			enemy.MaxAP = spawnTile.Enemy.ActionPoints;
			enemy.MaxHP = spawnTile.Enemy.HealthPoints;
			enemy.Name = spawnTile.Enemy.Name;

			_loggerService.Log($"Created: {enemy}", LoggerLevel.Characters);

			spawnTile.Spawn.Tile.RegisterVisitor(enemy);
			return enemy;
		}
	}
}