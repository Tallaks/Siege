using Kulinaria.Siege.Runtime.Gameplay.Battle.Spawn;
using UnityEngine;

namespace Kulinaria.Siege.Runtime.Debugging.Rendering
{
	public class TileGizmos : MonoBehaviour
	{
#if UNITY_EDITOR
		private void OnDrawGizmos()
		{
			var bounds = new Bounds(GetComponent<Collider>().bounds.center, GetComponent<Collider>().bounds.size * 0.95f);
			CustomGizmos.DrawTile(bounds, ColorByComponent());
		}
#endif

		private Color ColorByComponent()
		{
			if(GetComponent<PlayerSpawnTile>())
				return Color.green;

			if(GetComponent<EnemySpawnTile>())
				return Color.red;
			
			return new Color(0.1f, 0.1f, 0.1f);
		}
	}
}