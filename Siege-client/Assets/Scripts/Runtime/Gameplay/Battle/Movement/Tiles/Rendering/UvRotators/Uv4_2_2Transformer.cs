using System;
using System.ComponentModel;
using UnityEngine;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Movement.Tiles.Rendering.UvRotators
{
	public class Uv4_2_2Transformer : IUvRotator, IUvFlipper
	{
		private readonly Vector2Int _missingDiagonal;
		private readonly Vector2Int _missingSide;

		private bool _angleCalculationMade;
		private int _flip;

		public Uv4_2_2Transformer(Vector2Int missingDiagonal, Vector2Int missingSide)
		{
			_missingDiagonal = missingDiagonal;
			_missingSide = missingSide;

			_angleCalculationMade = false;
		}
		
		public float AngleDeg(CustomTile sourceTile)
		{
			_angleCalculationMade = true;
			
			_flip = 0;
			if (sourceTile[1, -1] == _missingDiagonal && sourceTile[-1, 0] == _missingSide)
				return 0f;

			if (sourceTile[1, 1] == _missingDiagonal && sourceTile[0, -1] == _missingSide)
				return 90f;
			
			if (sourceTile[-1, 1] == _missingDiagonal && sourceTile[1, 0] == _missingSide)
				return 180f;
			
			if (sourceTile[-1, -1] == _missingDiagonal && sourceTile[0, 1] == _missingSide)
				return 270f;

			_flip = 1;
			if (sourceTile[1, 1] == _missingDiagonal && sourceTile[-1, 0] == _missingSide)
				return 0f;

			if (sourceTile[-1, 1] == _missingDiagonal && sourceTile[0, -1] == _missingSide)
				return 90f;
			
			if (sourceTile[-1, -1] == _missingDiagonal && sourceTile[1, 0] == _missingSide)
				return 180f;
			
			if (sourceTile[1, -1] == _missingDiagonal && sourceTile[0, 1] == _missingSide)
				return 270f;
			
			throw new InvalidEnumArgumentException("Некорректный тайл для определения!");
		}

		public int GetFlip(CustomTile sourceTile)
		{
			if(_angleCalculationMade)
				return _flip;
			
			throw new Exception("Перед измерением флипа не проведено измерение угла");
		}
	}
}