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
		private IInputService InputService => 
			Object.FindObjectOfType<ProjectContext>().Container.Resolve<IInputService>();

		[UnitySetUp]
		public IEnumerator SetUp()
		{
			if(SceneManager.GetActiveScene().name != SceneNames.BootScene)
				yield return SceneManager.LoadSceneAsync(SceneNames.BootScene);
		}
		
		[UnityTest]
		public IEnumerator WhenGameInstallerInstallsBindings_ThenInputServiceIsBound()
		{
			yield return null;
			Assert.NotNull(InputService);
		}

		[UnityTest]
		public IEnumerator WhenLeftClickPressed_ThenInputServiceRegistersClick()
		{
			var clicked = false;
			InputService.OnClick += (_) => clicked = true;

			for (var i = 0; i < 10; i++)
			{
				yield return new WaitForSeconds(0.5f);
				if(clicked)
					Assert.Pass();
			}

			Assert.Fail();
		}
	}
}