using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Tiles;
using Zenject;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Path
{
	public interface IPathRenderer : IInitializable
	{
		void DrawPathFromSelectedTileTo(CustomTile tile);
		void Clear();
	}
}