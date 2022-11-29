using Kulinaria.Siege.Runtime.Gameplay.Battle.Characters;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Tiles;
using UnityEngine;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Selection
{
	[RequireComponent(typeof(CustomTile))]
	[RequireComponent(typeof(Collider))]
	public class TileSelection : MonoBehaviour, ITileSelectable
	{
		public CustomTile Tile => GetComponent<CustomTile>();
		public BaseCharacter Visitor => Tile.Visitor;
	}
}