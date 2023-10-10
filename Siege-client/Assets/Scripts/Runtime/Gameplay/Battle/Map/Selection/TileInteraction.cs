using Kulinaria.Siege.Runtime.Debugging.Logging;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Characters;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Path;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Tiles;
using UnityEngine;
using Zenject;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Selection
{
	[RequireComponent(typeof(CustomTile)), RequireComponent(typeof(Collider))]
	public class TileInteraction : MonoBehaviour, IInteractable
	{
		public CustomTile Tile => GetComponent<CustomTile>();
		public BaseCharacter Visitor => Tile.Visitor;
		private IDeselectService _deselectService;
		private ILoggerService _loggerService;
		private IPathFinder _pathFinder;
		private IPathSelector _pathSelector;

		[Inject]
		private void Construct(ILoggerService loggerService, IPathFinder pathFinder, IPathSelector pathSelector,
			IDeselectService deselectService)
		{
			_pathFinder = pathFinder;
			_deselectService = deselectService;
			_loggerService = loggerService;
			_pathSelector = pathSelector;
		}

		public void Interact()
		{
			if (_pathSelector.HasFirstSelectedTile && !_pathSelector.HasPath)
			{
				_loggerService.Log("Draw path between two tiles", LoggerLevel.Map);
				if (_pathFinder.GetShortestPath(Tile).Count != 0)
					_pathSelector.SelectSecondTile(Tile);
				else
					Tile.Interaction.Interact();
			}
			else if (Tile.HasVisitor)
			{
				Visitor.Interaction.Interact();
			}
			else
			{
				_deselectService.Deselect();
			}
		}
	}
}