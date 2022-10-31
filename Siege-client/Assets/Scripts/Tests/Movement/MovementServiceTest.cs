using System.Collections;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Movement;
using Kulinaria.Siege.Runtime.Infrastructure.Constants;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using Zenject;

namespace Kulinaria.Siege.Tests.Movement
{
	[TestFixture]
	public class MovementServiceTest
	{
		private SceneContext Context => Object.FindObjectOfType<SceneContext>();

		[UnityTest]
		public IEnumerator WhenBootSceneLoaded_ThenMovementServiceResolved()
		{
			yield return LoadBootScene();
			yield return new WaitForSeconds(2);
			
			var movementService = Context.Container.Resolve<IMovementService>();
			Assert.NotNull(movementService);
		}
		
		private AsyncOperation LoadBootScene() => 
			SceneManager.LoadSceneAsync(SceneNames.BootScene, LoadSceneMode.Single);
	}
}