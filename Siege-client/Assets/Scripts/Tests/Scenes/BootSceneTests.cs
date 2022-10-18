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
        public IEnumerator WhenBootSceneLoaded_ThenProjectContextLoaded()
        {            
            // Act
            yield return LoadBootScene();
            yield return new WaitForSeconds(0.5f);
            
            var projectContextComponent = Object.FindObjectOfType<ProjectContext>();
            // Assert
            Assert.IsNotNull(projectContextComponent);
        }

        [UnityTest]
        public IEnumerator WhenBootSceneLoaded_ThenGameInstallerInstantiated()
        {            
            // Act
            yield return LoadBootScene();
            yield return new WaitForSeconds(0.5f);
            
            var projectInstallerComponent = Object.FindObjectOfType<GameInstaller>();
            // Assert
            Assert.IsNotNull(projectInstallerComponent);
        }
        
        private AsyncOperation LoadBootScene() => 
            SceneManager.LoadSceneAsync(SceneNames.BootScene, LoadSceneMode.Single);
    }
}
