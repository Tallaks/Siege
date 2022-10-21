using System;

namespace Kulinaria.Siege.Runtime.Infrastructure.Scenes
{
	public interface ISceneLoader
	{
		void LoadSceneAsync(string sceneName, Action onLoad = null);
	}
}