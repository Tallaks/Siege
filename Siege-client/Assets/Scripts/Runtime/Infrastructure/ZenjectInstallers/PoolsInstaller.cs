using Kulinaria.Siege.Runtime.Gameplay.Battle.Utilities;
using UnityEngine;
using Zenject;

namespace Kulinaria.Siege.Runtime.Infrastructure.ZenjectInstallers
{
	public class PoolsInstaller : MonoInstaller
	{
		[SerializeField] private LineRenderer _pathRendererPrefab;
		
		public override void InstallBindings()
		{
			Container
				.Bind<Pool<LineRenderer>>()
				.FromMethod(_ => new Pool<LineRenderer>(Container, _pathRendererPrefab.gameObject, 5))
				.AsSingle();
		}
	}
}