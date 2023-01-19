using System;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Characters.Config;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Spawn
{
	[Serializable]
	public struct EnemySlot
	{
		[SceneObjectsOnly] public EnemySpawnTile Spawn;
		public EnemyConfig Enemy;

		[ValidateInput(nameof(VectorIsNotZero), "Look direction is zero")]
		public Vector3 LookDirection;

		private bool VectorIsNotZero(Vector3 lookDirection) =>
			lookDirection != Vector3.zero;
	}
}