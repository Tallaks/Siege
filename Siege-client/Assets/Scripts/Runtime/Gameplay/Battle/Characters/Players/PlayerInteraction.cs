using Kulinaria.Siege.Runtime.Debugging.Logging;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Path;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Tiles;
using UnityEngine;
using Zenject;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Characters.Players
{
	[RequireComponent(typeof(BasePlayer))]
	public class PlayerInteraction : CharacterInteraction
	{
		private ILoggerService _loggerService;
		private IPathSelector _pathSelector;
		private ITileActivator _tileActivator;

		[Inject]
		private void Construct(ILoggerService loggerService, IPathSelector pathSelector, ITileActivator tileActivator)
		{
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
				_pathSelector.SelectSecondTile(Tile);
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