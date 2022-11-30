using UnityEngine;

namespace Kulinaria.Siege.Runtime.Infrastructure.Assets
{
	public interface IAssetsProvider
	{
		T LoadAsset<T>(string arg) where T : Object;
	}
}