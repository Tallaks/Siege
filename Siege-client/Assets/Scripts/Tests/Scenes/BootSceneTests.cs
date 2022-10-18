using System.Collections;
using Kulinaria.Siege.Runtime.Infrastructure.Constants;
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
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(SceneNames.BootScene, LoadSceneMode.Single);
            // Act
            yield return asyncOperation;
            // Assert
            Assert.IsTrue(asyncOperation.isDone);
            Assert.IsTrue(SceneManager.GetActiveScene().name == SceneNames.BootScene);
        }
    }
}
