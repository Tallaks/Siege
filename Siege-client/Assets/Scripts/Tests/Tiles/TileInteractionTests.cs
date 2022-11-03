using System.Collections;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Movement;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Prototype;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Zenject;

namespace Kulinaria.Siege.Tests.Tiles
{
	public class TileInteractionTests : ZenjectIntegrationTestFixture
	{
		private IGridMap _gridMap;
		private SceneContext _sceneContext1;

		[UnityTest]
		public IEnumerator WhenUserClicksOnTile_ThenTileSelected()
		{
			Runtime.Gameplay.Battle.Prototype.GridMap.GridArray = new[,]
			{
				{ 1 }
			};

			var camera = new GameObject().AddComponent<Camera>();
			camera.orthographic = true;
			camera.transform.eulerAngles = new Vector3(90, 0, 0);
			camera.transform.position = new Vector3(0, 5, 0);

			PreInstall();

			Container.BindFactory<CustomTile, TilemapFactory>().AsSingle();
			Container.Bind<IGridMap>().To<Runtime.Gameplay.Battle.Prototype.GridMap>().FromNew().AsSingle();
			Container.Bind<IMovementService>().To<TileMovementService>().FromNew().AsSingle();
			Container.Bind<Camera>().FromInstance(camera).AsSingle();

			PostInstall();

			_gridMap = Container.Resolve<IGridMap>();
			_gridMap.GenerateMap();

			CustomTile tile0 = _gridMap.GetTile(0, 0);

			Debug.Log(tile0, tile0);

			var success = false;

			_gridMap.OnTileSelection += tile =>
			{
				if (tile.Equals(tile0))
					success = true;
			};

			while (true)
			{
				if (success)
					Assert.Pass();
				yield return null;
			}
		}
	}
}