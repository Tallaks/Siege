using System.Collections.Generic;
using UnityEngine;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Spawn
{
	public class Setup : MonoBehaviour
	{
		[SerializeField] private PlayerSlot[] _playerSlots;

		public IEnumerable<PlayerSlot> PlayerSlots => _playerSlots;
	}
}