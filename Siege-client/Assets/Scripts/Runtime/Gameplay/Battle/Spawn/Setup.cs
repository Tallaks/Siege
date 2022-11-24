using System.Collections.Generic;
using UnityEngine;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Spawn
{
	public class Setup : MonoBehaviour
	{
		[SerializeField] private PlayerSlot[] _playerSlots;
		[SerializeField] private EnemySlot[] _enemySlots;

		public IEnumerable<PlayerSlot> PlayerSlots => _playerSlots;
		public IEnumerable<EnemySlot> EnemySlots => _enemySlots;
	}
}