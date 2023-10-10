using Kulinaria.Siege.Runtime.Gameplay.Battle;
using Kulinaria.Siege.Runtime.Gameplay.Battle.UI;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Kulinaria.Siege.Runtime.Infrastructure.ZenjectInstallers
{
	public class GameplayInstaller : MonoInstaller
	{
		[SerializeField, Required] private CameraMover _cameraMover;
		[SerializeField, Required] private BattleMediator _battleMediator;

		public override void InstallBindings()
		{
			Container
				.Bind<CameraMover>()
				.FromInstance(_cameraMover)
				.AsSingle();
		}
	}
}