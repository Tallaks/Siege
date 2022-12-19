using System;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Characters.Players;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Characters.Config
{
	[CreateAssetMenu(menuName = "Siege/Player", fileName = "NewPlayer")]
	[Serializable]
	public class PlayerConfig : ScriptableObject
	{
		public string Name;
		[AssetsOnly, Required] public BasePlayer Prefab;
		public int ActionPoints;
		public int HealthPoints;
	}
}