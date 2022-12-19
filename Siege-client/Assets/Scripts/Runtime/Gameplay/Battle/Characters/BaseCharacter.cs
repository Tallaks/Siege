using Sirenix.OdinInspector;
using UnityEngine;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Characters
{
	[RequireComponent(typeof(CharacterInteraction))]
	public abstract class BaseCharacter : MonoBehaviour
	{
		public CharacterInteraction Interaction => GetComponent<CharacterInteraction>();

		[ShowInInspector] public int MaxAP { get; set; }
		[ShowInInspector] public int MaxHP { get; set; }
		[ShowInInspector] public string Name { get; set; }
	}
}