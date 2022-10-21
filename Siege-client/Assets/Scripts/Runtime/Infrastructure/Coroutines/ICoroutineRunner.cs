using System.Collections;
using UnityEngine;

namespace Kulinaria.Siege.Runtime.Infrastructure.Coroutines
{
	public interface ICoroutineRunner
	{
		Coroutine StartCoroutine(IEnumerator routine);
	}
}