using System.Collections;
using Kulinaria.Siege.Runtime.Infrastructure.Coroutines;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Zenject;

namespace Kulinaria.Siege.Tests
{
	[TestFixture]
	public class CoroutineRunnerTests : ZenjectIntegrationTestFixture
	{
		[UnityTest]
		public IEnumerator WhenCoroutineRunnerBound_ThenCoroutineRuns()
		{
			var coroutineRunner = new GameObject().AddComponent<CoroutineRunner>();
			
			PreInstall();

			Container
				.Bind<ICoroutineRunner>()
				.To<CoroutineRunner>()
				.FromInstance(coroutineRunner)
				.AsSingle();
			
			PostInstall();
			
			Assert.NotNull(Container.Resolve<ICoroutineRunner>());
			Coroutine runningRoutine = Container.Resolve<ICoroutineRunner>().StartCoroutine(RoutineExample());

			Assert.NotNull(runningRoutine);
			yield break;
		}

		private IEnumerator RoutineExample()
		{
			yield return new WaitForSeconds(10);
		}
	}
}