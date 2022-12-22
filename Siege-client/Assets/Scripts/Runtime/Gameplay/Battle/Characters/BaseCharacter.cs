using Sirenix.OdinInspector;
using UnityEngine;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Characters
{
	public abstract class BaseCharacter : MonoBehaviour
	{
		[SerializeField, Required, ShowIn(PrefabKind.PrefabAsset)] 
		private CharacterInteraction _interaction;
		
		public CharacterInteraction Interaction => _interaction;
		[ShowInInspector] public int MaxAP { get; set; }
		[ShowInInspector] public int MaxHP { get; set; }
		[ShowInInspector] public string Name { get; set; }
	}
}