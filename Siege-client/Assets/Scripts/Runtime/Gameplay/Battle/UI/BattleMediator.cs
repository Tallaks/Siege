using Sirenix.OdinInspector;
using UnityEngine;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.UI
{
	public class BattleMediator : MonoBehaviour
	{
		[BoxGroup(GroupName = "Player Info Panel"), Title("Player Info Panel", TitleAlignment = TitleAlignments.Centered),
		 SerializeField, Required]
		private PlayerInfoUi _playerInfo;

		[BoxGroup(GroupName = "Player Info Panel"), Button, DisableInEditorMode]
		public void ShowPlayerInfo()
		{
			_playerInfo.ShowPanel();
		}

		[BoxGroup(GroupName = "Player Info Panel"), Button, DisableInEditorMode]
		public void HidePlayerInfo()
		{
			_playerInfo.HidePanel();
		}
	}
}