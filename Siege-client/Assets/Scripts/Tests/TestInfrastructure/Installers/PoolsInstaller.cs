using Kulinaria.Siege.Runtime.Gameplay.Battle.Utilities;
using UnityEditor;
using UnityEngine;
using Zenject;

namespace Kulinaria.Siege.Tests.TestInfrastructure.Installers
{
	public class PoolsInstaller : IInstaller
	{
		private LineRenderer _lineRendererPrefab;

		public void PreInstall(params object[] args)
		{
			_lineRendererPrefab = AssetDatabase.LoadAssetAtPath<LineRenderer>("Assets/Prefabs/Battle/Map/Path.prefab");
		}

		public void Install(DiContainer container)
		{
			var pool = new Pool<LineRenderer>(container, _lineRendererPrefab.gameObject);
			container.Bind<Pool<LineRenderer>>().FromInstance(pool).AsSingle();
		}
	}
}