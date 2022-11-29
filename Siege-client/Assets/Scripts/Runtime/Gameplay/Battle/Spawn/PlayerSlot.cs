using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Tiles;
using UnityEngine;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Spawn
{
	[RequireComponent(typeof(CustomTile))]
	public class PlayerSlot : MonoBehaviour
	{
		public CustomTile Tile => GetComponent<CustomTile>();
	}
}