using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Spawn
{
	public class Setup : MonoBehaviour
	{
		[SerializeField] private PlayerSlot[] _playerSlots;
		[SerializeField] private EnemySlot[] _enemySlots;

#if UNITY_INCLUDE_TESTS
		public void InitPlayers(IEnumerable<PlayerSlot> slots) => 
			_playerSlots = slots.ToArray();
#endif
		
		public IEnumerable<PlayerSlot> PlayerSlots => _playerSlots;
		public IEnumerable<EnemySlot> EnemySlots => _enemySlots;
	}
}