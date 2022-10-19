using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Kulinaria.Siege.Tests.InputService
{
	public class InputSystemTests : InputTestFixture
	{
		[Test]
		public void WhenMouseKeyboardSchemeUsed_ThenTheyAddedToInputSystem()
		{
			Assert.NotNull(InputSystem.AddDevice<Mouse>());
			Assert.NotNull(InputSystem.AddDevice<Keyboard>());
		}

		[Test]
		public void WhenObjectWithInputSystemLoaded_ThenInputSystemExists()
		{
			// Arrange
			var keyboard = InputSystem.AddDevice<Keyboard>();
			var mouse = InputSystem.AddDevice<Mouse>();

			var prefabPlayerInput = new GameObject();
			prefabPlayerInput.gameObject.SetActive(false);
			prefabPlayerInput.AddComponent<PlayerInput>().actions =
				AssetDatabase.LoadAssetAtPath<InputActionAsset>("Assets/Settings/GameControls.inputactions");

			// Act
			PlayerInput player = PlayerInput.Instantiate(prefabPlayerInput, controlScheme: "Keyboard&Mouse");

			// Assert
			Assert.That(player.devices, Is.EquivalentTo(new InputDevice[] { keyboard, mouse }));
			Assert.That(player.currentControlScheme, Is.EqualTo("Keyboard&Mouse"));
		}

		public override void TearDown()
		{
			base.TearDown();
		}
	}
}