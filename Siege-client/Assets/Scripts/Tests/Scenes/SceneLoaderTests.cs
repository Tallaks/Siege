using System.Collections;
using Kulinaria.Siege.Runtime.Infrastructure.Scenes;
using NUnit.Framework;
using UnityEngine.TestTools;
using Zenject;

namespace Kulinaria.Siege.Tests.Scenes
{
	[TestFixture]
	public class SceneLoaderTests : ZenjectIntegrationTestFixture
	{
		[UnityTest]
		public IEnumerator WhenSceneLoaderBound_ThenItIsNotNull()
		{
			PreInstall();

			Container
				.Bind<ISceneLoader>()
				.To<SceneLoader>()
				.AsSingle();
			
			PostInstall();

			Assert.NotNull(Container.Resolve<ISceneLoader>());
			
			yield break;
		}
	}
}