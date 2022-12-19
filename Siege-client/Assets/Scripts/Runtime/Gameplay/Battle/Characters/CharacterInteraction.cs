using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Selection;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Tiles;
using UnityEngine;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Characters
{
	[RequireComponent(typeof(BaseCharacter))]
	[RequireComponent(typeof(MeshCollider))]
	public abstract class CharacterInteraction : MonoBehaviour, IInteractable
	{
		public CustomTile Tile { get; private set; }

		public BaseCharacter Visitor =>
			GetComponent<BaseCharacter>();

		public void Assign(CustomTile customTile) =>
			Tile = customTile;

		public abstract void Interact();
	}
}