using Kulinaria.Siege.Runtime.Infrastructure.UI;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.UI
{
	public class PlayerInfoUi : MonoBehaviour, IPanel
	{
		[SerializeField, Required] private BattleMediator _battleMediator;

		[SerializeField, Required, HideIf(nameof(CanvasIsValid))] private Canvas _canvas;
		[SerializeField, Required] private Slider _healthSlider;
		[SerializeField, Required] private Slider _actionSlider;
		
		public void ShowPanel() => 
			_canvas.gameObject.SetActive(true);

		public void HidePanel() => 
			_canvas.gameObject.SetActive(false);

		private bool CanvasIsValid() =>
			_canvas != null;
	}
}