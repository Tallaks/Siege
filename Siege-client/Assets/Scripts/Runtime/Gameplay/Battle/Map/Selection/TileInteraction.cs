using Kulinaria.Siege.Runtime.Debugging.Logging;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Characters;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Path;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Tiles;
using UnityEngine;
using Zenject;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Selection
{
	[RequireComponent(typeof(CustomTile))]
	[RequireComponent(typeof(Collider))]
	public class TileInteraction : MonoBehaviour, IInteractable
	{
		private IPathSelector _pathSelector;
		private ILoggerService _loggerService;
		private IDeselectService _deselectService;

		public CustomTile Tile => GetComponent<CustomTile>();
		public BaseCharacter Visitor => Tile.Visitor;

		[Inject]
		private void Construct(ILoggerService loggerService, IPathSelector pathSelector, IDeselectService deselectService)
		{
			_deselectService = deselectService;
			_loggerService = loggerService;
			_pathSelector = pathSelector;
		}

		public void Interact()
		{
			if (_pathSelector.HasFirstSelectedTile && !_pathSelector.HasPath)
			{
				_loggerService.Log("Draw path between two tiles", LoggerLevel.Map);
				_pathSelector.SelectSecondTile(Tile);
			}
			else if (Tile.HasVisitor)
				Visitor.Interaction.Interact();
			else
				_deselectService.Deselect();
		}
	}
}