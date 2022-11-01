using System.Collections;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Movement;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Zenject;

namespace Kulinaria.Siege.Tests.Movement
{
	public class MovementServiceZenjectTests : ZenjectIntegrationTestFixture
	{
		[UnityTest]
		public IEnumerator WhenMovementServiceAndTileFactoryBound_ThenTheyCanBeResolved()
		{
			PreInstall();
			
			Container.BindFactory<CustomTile, TilemapFactory>().AsSingle();
			Container.Bind<IMovementService>().To<TileMovementService>().FromNew().AsSingle();

			PostInstall();

			Assert.NotNull(Container.Resolve<IMovementService>());
			Assert.NotNull(Container.Resolve<TilemapFactory>());
			yield break;
		}
		
		[UnityTest]
		public IEnumerator WhenTilemapInitializedWith2DArray_ThenItGeneratesGridWithFourTilesAsSquare2x2()
		{
			var grid0 = new int[,] { { 1, 1 }, { 1, 1 } };
		
			PreInstall();
			
			Container.BindFactory<CustomTile, TilemapFactory>().AsSingle();
			Container.Bind<IMovementService>().To<TileMovementService>().FromNew().AsSingle();

			PostInstall();

			Container.Resolve<IMovementService>().GenerateMap(grid0);
			
			Assert.NotZero(Object.FindObjectsOfType<CustomTile>().Length);
			Assert.AreEqual(4, Object.FindObjectsOfType<CustomTile>().Length);
			yield break;
		}
		
		[UnityTest]
		public IEnumerator WhenTilemapInitializedWith2DArray_ThenItGeneratesGridWithFourTilesAsDiagonal()
		{
			var grid1 = new int[,] { { 0, 1 }, { 1, 0 } };
			
			PreInstall();
			
			Container.BindFactory<CustomTile, TilemapFactory>().AsSingle();
			Container.Bind<IMovementService>().To<TileMovementService>().FromNew().AsSingle();

			PostInstall();

			Container.Resolve<IMovementService>().GenerateMap(grid1);
			
			Assert.NotZero(Object.FindObjectsOfType<CustomTile>().Length);
			Assert.AreEqual(2, Object.FindObjectsOfType<CustomTile>().Length);
			yield break;
		}
	}
}