using Kulinaria.Siege.Runtime.Infrastructure.Constants;
using Kulinaria.Siege.Runtime.Infrastructure.Scenes;
using UnityEngine;
using Zenject;

namespace Kulinaria.Siege.Runtime.Infrastructure
{
	public class Bootstrapper : MonoBehaviour
	{
		[Inject] private ISceneLoader _sceneLoader;

		private void Awake() => 
			_sceneLoader.LoadSceneAsync(SceneNames.BattleScene);
	}
}