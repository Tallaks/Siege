using Kulinaria.Siege.Runtime.Gameplay.Battle.Characters.Players;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Spawn;
using UnityEngine;
using Zenject;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Characters.Factory
{
	public class PlayerFactory : PrefabFactory<BasePlayer>
	{
		private DiContainer _container;

		public PlayerFactory(DiContainer container) => 
			_container = container;

		public BasePlayer Create(PlayerSlot slot)
		{
			var player = _container.InstantiatePrefabForComponent<BasePlayer>(
				slot.Player.Prefab,
				slot.Spawn.transform.position, 
				Quaternion.identity,
				null);
			
			slot.Spawn.Tile.RegisterVisitor(player);
			return player;
		}
	}
}