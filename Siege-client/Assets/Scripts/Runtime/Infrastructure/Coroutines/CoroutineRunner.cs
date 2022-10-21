using UnityEngine;

namespace Kulinaria.Siege.Runtime.Infrastructure.Coroutines
{
	public class CoroutineRunner : MonoBehaviour, ICoroutineRunner
	{
		private void Awake() => 
			DontDestroyOnLoad(gameObject);
	}
}