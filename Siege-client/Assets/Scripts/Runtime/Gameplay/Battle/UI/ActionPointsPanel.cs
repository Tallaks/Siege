using System;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.UI
{
	public class ActionPointsPanel : MonoBehaviour
	{
		[SerializeField, Required] private BattleMediator _battleMediator;
		[SerializeField, Required] private Slider _actionPointsSlider;
		[SerializeField, Required] private TMP_InputField _actionPointsInputField;

		public Action<int> OnActionPointsChanged { get; set; }

		private void Awake()
		{
			OnActionPointsChanged += value =>
			{
				_actionPointsSlider.value = value;
				_actionPointsInputField.text = value.ToString();
				_battleMediator.ChangeActionPoints(value);
			};
			
			_actionPointsSlider.onValueChanged.AddListener(input => 
				OnActionPointsChanged?.Invoke((int)input));
			
			_actionPointsInputField.onValueChanged.AddListener(input => 
				OnActionPointsChanged?.Invoke(int.Parse(input)));			
		}
	}
}