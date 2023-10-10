using Kulinaria.Siege.Runtime.Debugging.Logging;
using UnityEngine;

namespace Kulinaria.Siege.Runtime.Infrastructure.Assets
{
	public class ResourcesAssetsProvider : IAssetsProvider
	{
		private readonly ILoggerService _loggerService;

		public ResourcesAssetsProvider(ILoggerService loggerService) =>
			_loggerService = loggerService;

		public T LoadAsset<T>(string arg) where T : Object
		{
			_loggerService.Log($"Loading asset with {arg}", LoggerLevel.Application, 9);
			return Resources.Load<T>(arg);
		}
	}
}