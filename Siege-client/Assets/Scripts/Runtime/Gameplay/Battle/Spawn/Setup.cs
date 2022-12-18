using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Spawn
{
	public class Setup : MonoBehaviour
	{
		[SerializeField] [ValidateInput(nameof(ValidatePlayerSlots), "Spawn or Config is null somewhere")] 
		[ValidateInput(nameof(ValidateEqualSpawnPlayerTiles), "Some players are on the same tile")] 
		private PlayerSlot[] _playerSlots;
		
		[SerializeField] [ValidateInput(nameof(ValidateEnemySlots), "Spawn or Config is null somewhere")] 
		[ValidateInput(nameof(ValidateEqualSpawnEnemyTiles), "Some enemies are on the same tile")]
		private EnemySlot[] _enemySlots;

#if UNITY_INCLUDE_TESTS
		public void InitPlayers(IEnumerable<PlayerSpawnTile> slots) => 
			PlayerSpawnersForTest = slots;

		public IEnumerable<PlayerSpawnTile> PlayerSpawnersForTest;
#endif
		
		public IEnumerable<PlayerSlot> PlayerSlots => _playerSlots;
		public IEnumerable<EnemySlot> EnemySlots => _enemySlots;

		private bool ValidateEqualSpawnPlayerTiles(PlayerSlot[] slots) =>
			slots.Select(k => k.Spawn).Distinct().Count() == slots.Select(k => k.Spawn).Count();
		
		private bool ValidateEqualSpawnEnemyTiles(EnemySlot[] slots) =>
			slots.Select(k => k.Spawn).Distinct().Count() == slots.Select(k => k.Spawn).Count();
		
		private bool ValidateEnemySlots(EnemySlot[] slots) => 
			slots.All(slot => slot.Enemy != null && slot.Spawn != null);

		private bool ValidatePlayerSlots(PlayerSlot[] slots) => 
			slots.All(slot => slot.Player != null && slot.Spawn != null);
	}
}