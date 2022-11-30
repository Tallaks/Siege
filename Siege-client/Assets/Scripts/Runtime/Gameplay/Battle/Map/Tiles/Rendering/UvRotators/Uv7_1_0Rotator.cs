using System.ComponentModel;
using UnityEngine;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Tiles.Rendering.UvRotators
{
	public class Uv7_1_0Rotator : IUvRotator
	{
		private readonly Vector2Int _missingTilePos;

		public Uv7_1_0Rotator(Vector2Int missingTilePos) => 
			_missingTilePos = missingTilePos;

		public float AngleDeg(CustomTile sourceTile)
		{
			if (sourceTile[-1, -1] == _missingTilePos)
				return 90f;
			if (sourceTile[-1, 1] == _missingTilePos)
				return 0f;
			if (sourceTile[1, 1] == _missingTilePos)
				return 270f;
			if (sourceTile[1, -1] == _missingTilePos)
				return 180f;

			throw new InvalidEnumArgumentException("Некорректный тайл для определения!");
		}
	}
}