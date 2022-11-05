using UnityEditor;

namespace Kulinaria.Siege.Editor.TileRenderingRules
{
	public class RulesWindow : EditorWindow
	{
		[MenuItem("Kulinaria/TileRules")]
		public static void Init()
		{
			var window = GetWindow<RulesWindow>( "Tile Rules Editor", focus: true);
			window.Show();
		}
	}
}