using Kulinaria.Siege.Runtime.Infrastructure.Inputs;
using Zenject;

namespace Kulinaria.Siege.Runtime.Infrastructure.ZenjectInstallers
{
	public class GameInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container
				.Bind<IInputService>()
				.To<InputService>()
				.FromNewComponentOnRoot()
				.AsSingle();
		}
	}
}