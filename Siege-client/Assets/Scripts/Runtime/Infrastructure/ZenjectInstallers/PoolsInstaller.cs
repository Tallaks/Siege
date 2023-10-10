using Kulinaria.Siege.Runtime.Gameplay.Battle.Utilities;
using UnityEngine;
using Zenject;

namespace Kulinaria.Siege.Runtime.Infrastructure.ZenjectInstallers
{
	public class PoolsInstaller : MonoInstaller, IInitializable
	{
		[SerializeField] private LineRenderer _pathRendererPrefab;

		public void Initialize()
		{
			Container.Resolve<Pool<LineRenderer>>().Initialize();
		}

		public override void InstallBindings()
		{
			Container
				.BindInterfacesTo<PoolsInstaller>()
				.FromInstance(this)
				.AsSingle();

			Container
				.Bind<Pool<LineRenderer>>()
				.FromMethod(_ => new Pool<LineRenderer>(Container, _pathRendererPrefab.gameObject))
				.AsSingle();
		}
	}
}