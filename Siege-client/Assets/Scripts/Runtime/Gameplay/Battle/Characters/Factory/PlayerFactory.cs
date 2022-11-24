using Kulinaria.Siege.Runtime.Gameplay.Battle.Characters.Players;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Spawn;
using Kulinaria.Siege.Runtime.Infrastructure.Assets;
using Zenject;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Characters.Factory
{
	public class PlayerFactory : PrefabFactory<BasePlayer>
	{
		private const string PathForPlayerPrefab = "Prefabs/Battle/Characters/Players/Stanley_Player";
		
		private IAssetsProvider _assetsProvider;
		private DiContainer _container;

		public PlayerFactory(DiContainer container, IAssetsProvider assetsProvider)
		{
			_container = container;
			_assetsProvider = assetsProvider;
		}
		
		public BasePlayer Create(PlayerSlot slot) =>
			_container.InstantiatePrefabForComponent<BasePlayer>(
				_assetsProvider.LoadAsset<BasePlayer>(PathForPlayerPrefab),
				slot.transform);
	}
}