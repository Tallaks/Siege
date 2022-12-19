using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Selection;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Characters
{
	[RequireComponent(typeof(CharacterSelection))]
	public abstract class BaseCharacter : MonoBehaviour
	{
		public CharacterSelection Selection => GetComponent<CharacterSelection>();
		[ShowInInspector]
		public int MaxAP { get; set; }
		[ShowInInspector]
		public int MaxHP { get; set; }
		[ShowInInspector]
		public string Name { get; set; }
	}
}