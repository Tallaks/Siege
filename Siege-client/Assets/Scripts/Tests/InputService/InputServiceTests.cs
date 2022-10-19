using System.Collections;
using Kulinaria.Siege.Runtime.Infrastructure.Constants;
using Kulinaria.Siege.Runtime.Infrastructure.Inputs;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using Zenject;

namespace Kulinaria.Siege.Tests.InputService
{
	[TestFixture]
	public class InputServiceTests
	{
		[UnityTest]
		public IEnumerator WhenGameInstallerInstallsBindings_ThenInputServiceIsBound()
		{
			yield return SceneManager.LoadSceneAsync(SceneNames.BootScene, LoadSceneMode.Single);

			Assert.NotNull(Object.FindObjectOfType<ProjectContext>().Container.Resolve<IInputService>());
		}
	}
}