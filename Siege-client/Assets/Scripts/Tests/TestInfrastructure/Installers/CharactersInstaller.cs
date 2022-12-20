using Kulinaria.Siege.Runtime.Gameplay.Battle.Characters.Factory;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Characters.Registry;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Spawn;
using UnityEditor;
using Zenject;

namespace Kulinaria.Siege.Tests.TestInfrastructure.Installers
{
	public class CharactersInstaller : IInstaller
	{
		private Setup _spawnSetup;

		public void PreInstall(params object[] args)
		{
			_spawnSetup = AssetDatabase.LoadAssetAtPath<Setup>("Assets/Prefabs/Battle/Map/SpawnSetup.prefab");
		}

		public void Install(DiContainer container)
		{
			container.Bind<Setup>().FromInstance(_spawnSetup).AsSingle();
			container.Bind<EnemyFactory>().FromNew().AsSingle();
			container.Bind<PlayerFactory>().FromNew().AsSingle();
			container.Bind<ICharacterRegistry>().To<CharacterRegistry>().FromNew().AsSingle();
		}
	}
}