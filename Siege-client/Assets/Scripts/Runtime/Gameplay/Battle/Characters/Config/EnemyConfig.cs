using System;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Characters.Enemies;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Characters.Config
{
	[CreateAssetMenu(menuName = "Siege/Enemy", fileName = "NewEnemy"), Serializable]
	public class EnemyConfig : ScriptableObject
	{
		public string Name;
		[AssetsOnly, Required] public BaseEnemy Prefab;
		public int ActionPoints;
		public int HealthPoints;
	}
}