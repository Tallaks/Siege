using System.Collections;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Movement;
using NUnit.Framework;
using UnityEngine.TestTools;
using Zenject;

namespace Kulinaria.Siege.Tests.Movement
{
	public class MovementServiceTests : ZenjectIntegrationTestFixture
	{
		[UnityTest]
		public IEnumerator WhenMovementServiceBound_ThenMovementServiceIsInstalled()
		{
			PreInstall();

			Container.Bind<IMovementService>().To<TileMovementService>().FromNew().AsSingle();

			PostInstall();

			Assert.NotNull(Container.Resolve<IMovementService>());
			yield break;
		}

		[UnityTest]
		public IEnumerator WhenTileFactoryBound_ThenTileFactoryInstalled()
		{
			PreInstall();

			Container.BindFactory<CustomTile, TilemapFactory>().AsSingle();

			PostInstall();

			Assert.NotNull(Container.Resolve<TilemapFactory>());
			yield break;
		}
	}
}