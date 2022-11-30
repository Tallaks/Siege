using Kulinaria.Siege.Runtime.Gameplay.Battle.Characters;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Tiles;
using UnityEngine;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Selection
{
	[RequireComponent(typeof(BaseCharacter))]
	[RequireComponent(typeof(MeshCollider))]
	public class CharacterSelection : MonoBehaviour, ITileSelectable
	{
		public CustomTile Tile { get; private set; }
		public BaseCharacter Visitor => 
			GetComponent<BaseCharacter>();

		public void Assign(CustomTile customTile) => 
			Tile = customTile;
	}
}