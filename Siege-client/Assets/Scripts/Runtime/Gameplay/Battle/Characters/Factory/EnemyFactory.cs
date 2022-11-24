using Kulinaria.Siege.Runtime.Gameplay.Battle.Characters.Enemies;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Spawn;
using Kulinaria.Siege.Runtime.Infrastructure.Assets;
using Zenject;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Characters.Factory
{
	public class EnemyFactory : PrefabFactory<BaseEnemy>
	{
		private const string PathForEnemyPrefab = "Prefabs/Battle/Characters/Enemies/Stanley_Enemy";

		private readonly DiContainer _container;
		private readonly IAssetsProvider _assetsProvider;

		public EnemyFactory(DiContainer container, IAssetsProvider assetsProvider)
		{
			_container = container;
			_assetsProvider = assetsProvider;
		}

		public BaseEnemy Create(EnemySlot slot) =>
			_container.InstantiatePrefabForComponent<BaseEnemy>(
				_assetsProvider.LoadAsset<BaseEnemy>(PathForEnemyPrefab),
				slot.transform);
	}
}