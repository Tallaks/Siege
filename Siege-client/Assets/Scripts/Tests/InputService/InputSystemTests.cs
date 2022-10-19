using NUnit.Framework;
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
	}
}