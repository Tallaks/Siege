using Kulinaria.Siege.Runtime.Debugging.Logging;
using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Path;
using UnityEngine;
using Zenject;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Characters.Enemies
{
	[RequireComponent(typeof(BaseEnemy))]
	public class EnemyInteraction : CharacterInteraction
	{
		private ILoggerService _loggerService;
		private IPathSelector _pathSelector;

		[Inject]
		private void Construct(ILoggerService loggerService, IPathSelector pathSelector)
		{
			_loggerService = loggerService;
			_pathSelector = pathSelector;
		}

		public override void Interact()
		{
			if (_pathSelector.HasFirstSelectedTile)
			{
				if (_pathSelector.HasPath)
				{
					SelectEnemy();
					return;
				}

				_loggerService.Log("Draw path between two tiles", LoggerLevel.Map);
				_pathSelector.SelectSecondTile(Tile);
				return;
			}

			SelectEnemy();
		}

		private void SelectEnemy() => 
			_loggerService.Log($"Enemy {Visitor.Name} is interacted", LoggerLevel.Characters);
	}
}