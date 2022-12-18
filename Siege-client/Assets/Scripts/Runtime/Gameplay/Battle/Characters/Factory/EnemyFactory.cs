using Kulinaria.Siege.Runtime.Gameplay.Battle.Characters.Enemies;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Spawn;
using Kulinaria.Siege.Runtime.Infrastructure.Assets;
using UnityEngine;
using Zenject;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Characters.Factory
{
	public class EnemyFactory : PrefabFactory<BaseEnemy>
	{
		private readonly DiContainer _container;
		private readonly IAssetsProvider _assetsProvider;

		public EnemyFactory(DiContainer container) => 
			_container = container;

		public BaseEnemy Create(EnemySlot spawnTile)
		{
			var enemy = _container.InstantiatePrefabForComponent<BaseEnemy>(
				spawnTile.Enemy.Prefab,
				spawnTile.Spawn.transform.position,
				Quaternion.Euler(0, 180f,0), 
				null);
			
			spawnTile.Spawn.Tile.RegisterVisitor(enemy);
			return enemy;
		}
	}
}