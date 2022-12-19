using Kulinaria.Siege.Runtime.Debugging.Logging;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Path;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Tiles;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Selection
{
	public class DeselectService : IDeselectService
	{
		private readonly ILoggerService _loggerService;
		private readonly IPathSelector _pathSelector;
		private readonly ITileActivator _tileActivator;

		public DeselectService(ILoggerService loggerService, IPathSelector pathSelector, ITileActivator tileActivator)
		{
			_loggerService = loggerService;
			_pathSelector = pathSelector;
			_tileActivator = tileActivator;
		}

		public void Deselect()
		{
			_loggerService.Log("Deselect", LoggerLevel.Map);
			_tileActivator.DeactivateAllTiles();
			_pathSelector.Deselect();
		}
	}
}