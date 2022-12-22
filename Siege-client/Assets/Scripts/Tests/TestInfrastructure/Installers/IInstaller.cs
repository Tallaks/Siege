using Zenject;

namespace Kulinaria.Siege.Tests.TestInfrastructure.Installers
{
	public interface IInstaller
	{
		void PreInstall(params object[] args);
		void Install(DiContainer container);
	}
}