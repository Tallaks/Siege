using Kulinaria.Siege.Runtime.Gameplay.Battle;
using UnityEditor;
using Zenject;

namespace Kulinaria.Siege.Tests.TestInfrastructure.Installers
{
	public class GameplayInstaller : IInstaller
	{
		private CameraMover _cameraMover;

		public void PreInstall(params object[] args) => 
			_cameraMover = AssetDatabase.LoadAssetAtPath<CameraMover>("Assets/Prefabs/Battle/CameraMover.prefab");

		public void Install(DiContainer container) => 
			container.Bind<CameraMover>().FromComponentInNewPrefab(_cameraMover).AsSingle();
	}
}