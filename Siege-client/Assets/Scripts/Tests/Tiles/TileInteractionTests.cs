using System.Collections;
using Kulinaria.Siege.Runtime.Gameplay.Battle;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Grid;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Path;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Selection;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Tiles;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Tiles.Rendering;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Movement;
using Kulinaria.Siege.Runtime.Infrastructure.Inputs;
using Kulinaria.Siege.Runtime.Infrastructure.ZenjectInstallers;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;
using Zenject;

namespace Kulinaria.Siege.Tests.Tiles
{
	public class TileInteractionTests : ZenjectIntegrationTestFixture
	{
		private IGridMap _gridMap;
		private SceneContext _sceneContext1;
		private bool _clickRegistered;

		[UnityTest]
		public IEnumerator WhenUserClicksOnTile_ThenTileSelected()
		{
			GameInstaller.Testing = true;

			Runtime.Gameplay.Battle.Prototype.ArrayGridMap.GridArray = new[,]
			{
				{ 1 }
			};

			var cameraMover = AssetDatabase.LoadAssetAtPath<CameraMover>("Assets/Prefabs/Battle/CameraMover.prefab");
			var cube = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Cube.prefab");
			Object.Instantiate(cube);

			PreInstall();

			Container.BindFactory<CustomTile, TilemapFactory>().AsSingle();
			Container.Bind<IGridMap>().To<Runtime.Gameplay.Battle.Prototype.ArrayGridMap>().FromNew().AsSingle();
			Container.Bind<IMovementService>().To<TileMovementService>().FromNew().AsSingle();
			Container.Bind<CameraMover>().FromComponentInNewPrefab(cameraMover).AsSingle();
			Container.Bind<ITilesRenderingAggregator>().To<TilesRenderingAggregator>().FromNew().AsSingle();
			Container.Bind<IPathFinder>().To<BellmanFordPathFinder>().FromNew().AsSingle();
			Container.BindInterfacesTo<CustomTileSelector>().FromNew().AsSingle();

			PostInstall();

			_gridMap = Container.Resolve<IGridMap>();
			_gridMap.GenerateMap();

			Container.Resolve<IInputService>().OnClick += clickPos =>
				RegisterClick(Container.Resolve<CameraMover>(), clickPos);

			yield return new WaitForSeconds(0.1f);

			while (true)
			{
				if (_clickRegistered)
				{
					Assert.Pass();
					break;
				}

				yield return null;
			}
		}

		private void RegisterClick(CameraMover cameraMover, Vector2 clickPos)
		{
			Ray ray = cameraMover.Camera.ScreenPointToRay(clickPos);
			if (Physics.Raycast(ray, out RaycastHit hit))
			{
				if (hit.transform.GetComponent<ITileSelectable>() != null)
					_clickRegistered = true;
			}
		}
	}
}