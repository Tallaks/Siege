using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Kulinaria.Siege.Tests.Scenes
{
    [TestFixture]
    public class BootSceneTests
    {
        [UnityTest]
        public IEnumerator WhenBootSceneLoadCalled_ThenItLoads()
        {
            // Arrange
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("BootScene", LoadSceneMode.Single);
            // Act
            yield return asyncOperation;
            // Assert
            Assert.IsTrue(asyncOperation.isDone);
            Assert.IsTrue(SceneManager.GetActiveScene().name == "BootScene");
        }
    }
}
