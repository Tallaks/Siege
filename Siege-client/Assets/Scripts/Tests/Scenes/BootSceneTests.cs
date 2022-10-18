using System.Collections;
using Kulinaria.Siege.Runtime.Infrastructure.Constants;
using Kulinaria.Siege.Runtime.Infrastructure.ZenjectInstallers;
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
            // Act
            AsyncOperation asyncOperation = LoadBootScene();
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

        [UnityTest]
        public IEnumerator WhenBootSceneLoaded_ThenProjectContextAndGameInstallerExist()
        {
            // Arrange
            yield return LoadBootSceneAndWait();

            // Assert
            Assert.IsNotNull(Object.FindObjectOfType<ProjectContext>());
            Assert.IsNotNull(Object.FindObjectOfType<GameInstaller>());
        }

        private IEnumerator LoadBootSceneAndWait()
        {
            yield return LoadBootScene();
            yield return new WaitForSeconds(0.5f);
        }

        private AsyncOperation LoadBootScene() => 
            SceneManager.LoadSceneAsync(SceneNames.BootScene, LoadSceneMode.Single);
    }
}
