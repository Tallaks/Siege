using Kulinaria.Siege.Runtime.Debugging.Logging;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Path;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Tiles;
using Zenject;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Characters.Players
{
	public class PlayerInteraction : CharacterInteraction
	{
		private ILoggerService _loggerService;
		private IPathSelector _pathSelector;
		private ITileActivator _tileActivator;
		private IPathFinder _pathFinder;

		[Inject]
		private void Construct(ILoggerService loggerService, IPathFinder pathFinder, IPathSelector pathSelector, ITileActivator tileActivator)
		{
			_pathFinder = pathFinder;
			_tileActivator = tileActivator;
			_loggerService = loggerService;
			_pathSelector = pathSelector;
		}

		public override void Interact()
		{
			if (_pathSelector.HasFirstSelectedTile)
			{
				if (_pathSelector.HasPath)
				{
					SelectPlayer();
					return;
				}

				_loggerService.Log("Draw path between two tiles", LoggerLevel.Map);
				if(_pathFinder.GetShortestPath(to: Tile).Count != 0)
					_pathSelector.SelectSecondTile(Tile);
				else
					SelectPlayer();
				return;
			}

			SelectPlayer();
		}

		private void SelectPlayer()
		{
			_loggerService.Log($"Select player {Visitor.Name} by interaction", LoggerLevel.Characters);
			_pathSelector.Deselect();
			_pathSelector.SelectFirstTile(Tile);
			_tileActivator.ActivateTilesAround(Visitor);
		}
	}
}