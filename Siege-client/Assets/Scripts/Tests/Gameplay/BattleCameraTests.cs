using System.Collections;
using Kulinaria.Siege.Runtime.Gameplay.Battle;
using Kulinaria.Siege.Runtime.Infrastructure.Constants;
using Kulinaria.Siege.Runtime.Infrastructure.Coroutines;
using Kulinaria.Siege.Runtime.Infrastructure.Inputs;
using Kulinaria.Siege.Runtime.Infrastructure.ZenjectInstallers;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using Zenject;

namespace Kulinaria.Siege.Tests.Gameplay
{
	[TestFixture]
	public class BattleCameraTests
	{
		private CameraMover _cameraMover;
		private Vector3 _startPosition;
		private Camera _camera;
		private float _startDistance;
		private Quaternion _startRotation;

		[UnitySetUp]
		public IEnumerator SetUp()
		{
			yield return LoadBootScene();
			Object.FindObjectOfType<ApplicationInstaller>()?.Initialize();
			yield return new WaitForSeconds(0.5f);

			_camera = Object.FindObjectOfType<Camera>();
			_cameraMover = Object.FindObjectOfType<SceneContext>().Container.Resolve<CameraMover>();

			_startPosition = _cameraMover.transform.position;
			_startRotation = _cameraMover.transform.rotation;
			_startDistance = _camera.transform.localPosition.magnitude;
		}

		[UnityTest]
		public IEnumerator WhenBattleSceneLoaded_ThenCameraExists()
		{
			Assert.NotNull(_camera);
			yield break;
		}

		[UnityTest]
		public IEnumerator WhenBattleSceneLoaded_ThenCameraMoverComponentExists()
		{
			Assert.NotNull(_cameraMover);
			yield break;
		}

		[UnityTest]
		public IEnumerator WhenMovementForwardCommandSent_ThenCameraMovesForward()
		{
			float startPosZ = _startPosition.z;

			while (true)
			{
				yield return null;
				if (_cameraMover.transform.position.z > startPosZ)
					Assert.Pass();
			}
		}

		[UnityTest]
		public IEnumerator WhenMovementBackCommandSent_ThenCameraMovesBack()
		{
			float startPosZ = _startPosition.z;

			while (true)
			{
				yield return null;
				if (_cameraMover.transform.position.z < startPosZ)
					Assert.Pass();
			}
		}

		[UnityTest]
		public IEnumerator WhenMovementLeftCommandSent_ThenCameraMovesLeft()
		{
			float startPosX = _startPosition.x;

			while (true)
			{
				yield return null;
				if (_cameraMover.transform.position.x < startPosX)
					Assert.Pass();
			}
		}

		[UnityTest]
		public IEnumerator WhenMovementRightCommandSent_ThenCameraMovesRight()
		{
			float startPosX = _startPosition.x;

			while (true)
			{
				yield return null;
				if (_cameraMover.transform.position.x > startPosX)
					Assert.Pass();
			}
		}

		[UnityTest]
		public IEnumerator WhenMovementZoomCommandSent_ThenCameraDoesZoom()
		{
			var zoomedIn = false;
			var zoomedOut = false;

			while (true)
			{
				yield return null;
				if (_camera.transform.localPosition.magnitude > _startDistance)
					zoomedOut = true;
				if (_camera.transform.localPosition.magnitude < _startDistance)
					zoomedIn = true;

				if (zoomedIn && zoomedOut)
					Assert.Pass();
			}
		}

		[UnityTest]
		public IEnumerator WhenMovementRotateCommandSent_ThenCameraRotates()
		{
			while (true)
			{
				yield return null;
				if (_cameraMover.transform.rotation != _startRotation)
					Assert.Pass();
			}
		}

		[UnityTest]
		public IEnumerator WhenCameraRotated_ThenCameraMovesForwardRight()
		{
			while (true)
			{
				_cameraMover.transform.rotation = Quaternion.Euler(00, 180, 0);
				yield return null;
				if (_camera.transform.position.z <= -5)
					Assert.Pass();
			}
		}

		[UnityTest]
		public IEnumerator WhenCameraRotated_ThenCameraMovesLeftRightWay()
		{
			while (true)
			{
				_cameraMover.transform.rotation = Quaternion.Euler(00, 90, 0);
				yield return null;
				if (_camera.transform.position.z >= 5)
					Assert.Pass();
			}
		}

		[UnityTearDown]
		public IEnumerator TearDown()
		{
			var resolvedRunner = Object.FindObjectOfType<ProjectContext>().Container.Resolve<ICoroutineRunner>();
			var resolvedInputService = Object.FindObjectOfType<ProjectContext>().Container.Resolve<IInputService>();

			foreach (CoroutineRunner runner in Object.FindObjectsOfType<CoroutineRunner>())
			{
				if (ReferenceEquals(runner, resolvedRunner))
					continue;

				Object.Destroy(runner.gameObject);
			}

			foreach (Runtime.Infrastructure.Inputs.InputService inputService in Object.
				         FindObjectsOfType<Runtime.Infrastructure.Inputs.InputService>())
			{
				if (ReferenceEquals(inputService, resolvedInputService))
					continue;

				Object.Destroy(inputService.gameObject);
			}

			yield break;
		}

		private AsyncOperation LoadBootScene() => SceneManager.LoadSceneAsync(SceneNames.BootScene, LoadSceneMode.Single);
	}
}