using Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Tiles;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Path
{
	public interface IPathRenderer
	{
		void DrawPathFromSelectedTileTo(CustomTile tile);
		void Clear();
	}
}