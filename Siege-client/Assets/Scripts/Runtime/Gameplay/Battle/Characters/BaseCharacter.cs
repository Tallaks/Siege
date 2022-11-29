using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Selection;
using UnityEngine;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Characters
{
	[RequireComponent(typeof(CharacterSelection))]
	public abstract class BaseCharacter : MonoBehaviour
	{
		public CharacterSelection Selection => GetComponent<CharacterSelection>();
		public int ActionPoints { get; protected set; } = 10;
	}
}