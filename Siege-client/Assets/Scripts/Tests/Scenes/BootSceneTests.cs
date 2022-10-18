using System.Collections;
using Kulinaria.Siege.Runtime.Infrastructure.Constants;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using Zenject;

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

        [Test]
        public void WhenProjectContextResourceLoadingCalled_ThenProjectContextLoads()
        {
            // Arrange
            // Act
            var projectContextPrefab = Resources.Load<ProjectContext>("ProjectContext");
            // Assert
            Assert.IsNotNull(projectContextPrefab);
        }
    }
}
