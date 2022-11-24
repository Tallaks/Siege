using UnityEngine;

namespace Kulinaria.Siege.Runtime.Infrastructure.Assets
{
	public class ResourcesAssetsProvider : IAssetsProvider
	{
		public T LoadAsset<T>(string arg) where T : Object =>
			Resources.Load<T>(arg);
	}
}