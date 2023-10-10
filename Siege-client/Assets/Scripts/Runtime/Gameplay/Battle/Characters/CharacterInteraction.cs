using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Selection;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Tiles;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Characters
{
	[RequireComponent(typeof(Collider))]
	public abstract class CharacterInteraction : MonoBehaviour, IInteractable
	{
		[SerializeField, Required, ShowIn(PrefabKind.PrefabAsset)] private BaseCharacter _owner;

		public CustomTile Tile { get; private set; }

		public BaseCharacter Visitor =>
			_owner;

		public abstract void Interact();

		public void Assign(CustomTile customTile)
		{
			Tile = customTile;
		}
	}
}