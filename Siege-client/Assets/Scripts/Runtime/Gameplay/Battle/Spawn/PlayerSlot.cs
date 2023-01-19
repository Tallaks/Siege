using System;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Characters.Config;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Spawn
{
	[Serializable]
	public struct PlayerSlot
	{
		public PlayerSpawnTile Spawn;
		public PlayerConfig Player;
	}
}