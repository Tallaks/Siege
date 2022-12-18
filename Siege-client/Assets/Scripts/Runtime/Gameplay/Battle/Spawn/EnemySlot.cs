using System;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Characters.Config;
using Sirenix.OdinInspector;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Spawn
{
	[Serializable]
	public struct EnemySlot
	{
		[SceneObjectsOnly] public EnemySpawnTile Spawn;
		public EnemyConfig Enemy;
	}
}