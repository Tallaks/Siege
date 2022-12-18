using Kulinaria.Siege.Runtime.Gameplay.Battle.Characters.Registry;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.UI
{
	public class BattleMediator : MonoBehaviour, IInitializable
	{
		[SerializeField, SceneObjectsOnly, Required] private ActionPointsPanel _actionPointsPanel;
		
		[Inject] private ICharacterRegistry _characterRegistry;
		
		public void Initialize() => _actionPointsPanel.OnActionPointsChanged?.Invoke(10);
		public void ChangeActionPoints(int value) => _characterRegistry.ChangeActionPointsForAll(value);
	}
}