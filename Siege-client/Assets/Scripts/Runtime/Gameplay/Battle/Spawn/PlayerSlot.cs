using Kulinaria.Siege.Runtime.Extensions;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Level.Tiles;
using UnityEngine;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Spawn
{
	[RequireComponent(typeof(CustomTile))]
	public class PlayerSlot : MonoBehaviour
	{
		public CustomTile Tile => GetComponent<CustomTile>();

		private void OnDrawGizmos() => 
			CustomGizmos.DrawBounds(GetComponent<Collider>().bounds, Color.green);
	}
}