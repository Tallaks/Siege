using System.ComponentModel;
using UnityEngine;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Movement.Tiles.Rendering.UvRotators
{
	public class Uv5_1_2Rotator : IUvRotator
	{
		private readonly Vector2Int _missingTilePos;

		public Uv5_1_2Rotator(Vector2Int missingTilePos) =>
			_missingTilePos = missingTilePos;

		public float AngleDeg(CustomTile sourceTile)
		{
			if (sourceTile[0, 1] == _missingTilePos)
				return 270f;
			if (sourceTile[0, -1] == _missingTilePos)
				return 90f;
			if (sourceTile[-1, 0] == _missingTilePos)
				return 0f;
			if (sourceTile[1, 0] == _missingTilePos)
				return 180f;

			throw new InvalidEnumArgumentException("Некорректный тайл для определения!");
		}
	}
}