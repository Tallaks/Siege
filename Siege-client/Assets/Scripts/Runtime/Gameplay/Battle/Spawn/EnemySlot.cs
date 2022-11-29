using Kulinaria.Siege.Runtime.Gameplay.Battle.Level.Tiles;
using UnityEngine;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Spawn
{
	[RequireComponent(typeof(CustomTile))]
	public class EnemySlot : MonoBehaviour
	{
		public CustomTile Tile => GetComponent<CustomTile>();
	}
}