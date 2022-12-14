using System.Collections;
using Kulinaria.Siege.Runtime.Infrastructure.Constants;
using Kulinaria.Siege.Runtime.Infrastructure.Inputs;
using Kulinaria.Siege.Runtime.Infrastructure.ZenjectInstallers;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
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
			ApplicationInstaller.Testing = true;
			if (SceneManager.GetActiveScene().name != SceneNames.BootScene)
				yield return SceneManager.LoadSceneAsync(SceneNames.BootScene);
		}

		[UnityTest]
		public IEnumerator WhenGameInstallerInstallsBindings_ThenInputServiceIsBound()
		{
			yield return null;
			Assert.NotNull(InputService);
		}

		[UnityTest]
		public IEnumerator WhenMouseInputsUsed_ThenThisTestPasses()
		{
			var clicked = false;
			var rotated = false;
			var zoomed = false;
			InputService.OnClick += _ => clicked = true;
			InputService.OnClick += _ => Debug.Log("Click");
			InputService.OnRotate += _ => rotated = true;
			InputService.OnRotate += _ => Debug.Log("Rotate");
			InputService.OnZoom += _ => zoomed = true;
			InputService.OnZoom += _ => Debug.Log("Zoom");

			while (true)
			{
				yield return new WaitForSeconds(0.5f);
				if (clicked && rotated && zoomed)
				{
					InputService.OnClick = null;
					InputService.OnRotate = null;
					InputService.OnZoom = null;
					Assert.Pass();
				}
			}
		}

		[UnityTest]
		public IEnumerator WhenAltIsPressed_ThenClicksNotWork()
		{
			var clicked = false;
			var rotated = false;
			InputService.OnClick += _ => clicked = true;
			InputService.OnClick += _ => Debug.Log("Click");
			InputService.OnRotate += _ => rotated = true;
			InputService.OnRotate += _ => Debug.Log("Rotate");

			while (true)
			{
				yield return null;
				if (Keyboard.current.leftAltKey.isPressed && Mouse.current.leftButton.wasReleasedThisFrame)
				{
					InputService.OnClick = null;
					InputService.OnRotate = null;
					InputService.OnZoom = null;
					Assert.IsFalse(clicked);
					yield break;
				}
			}
		}

		[UnityTest]
		public IEnumerator WhenWASDPressed_ThenInputServiceRegistersMovement()
		{
			var movedUp = false;
			var movedDown = false;
			var movedRight = false;
			var movedLeft = false;
			InputService.OnMove += inputMove =>
			{
				if (inputMove.x > 0)
					movedRight = true;
				if (inputMove.x < 0)
					movedLeft = true;
				if (inputMove.y > 0)
					movedUp = true;
				if (inputMove.y < 0)
					movedDown = true;
			};

			while (true)
			{
				yield return new WaitForSeconds(0.5f);
				if (movedDown && movedLeft && movedRight && movedUp)
				{
					InputService.OnMove = null;
					Assert.Pass();
				}
			}
		}
	}
}