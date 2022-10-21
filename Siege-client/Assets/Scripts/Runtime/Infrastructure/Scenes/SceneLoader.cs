using System;
using System.Collections;
using Kulinaria.Siege.Runtime.Infrastructure.Coroutines;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Kulinaria.Siege.Runtime.Infrastructure.Scenes
{
	public class SceneLoader : ISceneLoader
	{
		private readonly ICoroutineRunner _runner;

		public SceneLoader(ICoroutineRunner runner) => 
			_runner = runner;

		public void LoadSceneAsync(string sceneName, Action onLoad = null) => 
			_runner.StartCoroutine(LoadRoutine(sceneName, onLoad));

		private IEnumerator LoadRoutine(string sceneName, Action onLoad)
		{
			yield return SceneManager.LoadSceneAsync(sceneName);
			onLoad?.Invoke();
		}
	}
}