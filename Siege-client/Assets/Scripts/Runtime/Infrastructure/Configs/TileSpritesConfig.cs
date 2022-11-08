using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace Kulinaria.Siege.Runtime.Infrastructure.Configs
{
	[CreateAssetMenu(fileName = "TileSpritesConfig", menuName = "Siege/TilesConfig", order = 10)]
	public class TileSpritesConfig : ScriptableObject
	{
		[BoxGroup("Tiles"), LabelText("Тайлы для установки правил")]
		[PreviewField(128), HideLabel, HorizontalGroup("Tiles/8_0_0")]
		public Texture2D Tile8_0_0;
		
		[OnInspectorGUI]
		[PropertyOrder(-10), HorizontalGroup("Tiles/8_0_0")]
		private void ShowImage8_0_0()
		{
			GUILayout.Label(
				AssetDatabase.LoadAssetAtPath<Texture2D>(
					"Assets/Art/EditorTools/TileRenderer/Sprites/TileTemplate8_0_0.png"));
		}
		
		[PreviewField(128), HideLabel, HorizontalGroup("Tiles/6_2_0a")]
		public Texture2D Tile6_2_0a;
		
		[OnInspectorGUI]
		[PropertyOrder(-10), HorizontalGroup("Tiles/6_2_0a")]
		private void ShowImage6_2_0a()
		{
			GUILayout.Label(
				AssetDatabase.LoadAssetAtPath<Texture2D>(
					"Assets/Art/EditorTools/TileRenderer/Sprites/TileTemplate6_2_0a.png"));
		}
		
		[PreviewField(128), HideLabel, HorizontalGroup("Tiles/6_2_0b")]
		public Texture2D Tile6_2_0b;
		
		[OnInspectorGUI]
		[PropertyOrder(-10), HorizontalGroup("Tiles/6_2_0b")]
		private void ShowImage6_2_0b()
		{
			GUILayout.Label(
				AssetDatabase.LoadAssetAtPath<Texture2D>(
					"Assets/Art/EditorTools/TileRenderer/Sprites/TileTemplate6_2_0b.png"));
		}
		
		[PreviewField(128), HideLabel, HorizontalGroup("Tiles/0_4_4")]
		public Texture2D Tile0_4_4;
		
		[OnInspectorGUI]
		[PropertyOrder(-10), HorizontalGroup("Tiles/0_4_4")]
		private void ShowImage0_4_4()
		{
			GUILayout.Label(
				AssetDatabase.LoadAssetAtPath<Texture2D>(
					"Assets/Art/EditorTools/TileRenderer/Sprites/TileTemplate0_4_4.png"));
		}
		
		
		[PreviewField(128), HideLabel, HorizontalGroup("Tiles/5_3_0")]
		public Texture2D Tile5_3_0;
		
		[OnInspectorGUI]
		[PropertyOrder(-10), HorizontalGroup("Tiles/5_3_0")]
		private void ShowImage5_3_0()
		{
			GUILayout.Label(
				AssetDatabase.LoadAssetAtPath<Texture2D>(
					"Assets/Art/EditorTools/TileRenderer/Sprites/TileTemplate5_3_0.png"));
		}
		
		[PreviewField(128), HideLabel, HorizontalGroup("Tiles/7_1_0")]
		public Texture2D Tile7_1_0;
		
		[OnInspectorGUI]
		[PropertyOrder(-10), HorizontalGroup("Tiles/7_1_0")]
		private void ShowImage7_1_0()
		{
			GUILayout.Label(
				AssetDatabase.LoadAssetAtPath<Texture2D>(
					"Assets/Art/EditorTools/TileRenderer/Sprites/TileTemplate7_1_0.png"));
		}

		[PreviewField(128), HideLabel, HorizontalGroup("Tiles/2_2_4")]
		public Texture2D Tile2_2_4;
		
		[OnInspectorGUI]
		[PropertyOrder(-10), HorizontalGroup("Tiles/2_2_4")]
		private void ShowImage2_2_4()
		{
			GUILayout.Label(
				AssetDatabase.LoadAssetAtPath<Texture2D>(
					"Assets/Art/EditorTools/TileRenderer/Sprites/TileTemplate2_2_4.png"));
		}
		
		[PreviewField(128), HideLabel, HorizontalGroup("Tiles/4_2_2a")]
		public Texture2D Tile4_2_2a;

		[OnInspectorGUI]
		[PropertyOrder(-10), HorizontalGroup("Tiles/4_2_2a")]
		private void ShowImage4_2_2a()
		{
			GUILayout.Label(
				AssetDatabase.LoadAssetAtPath<Texture2D>(
					"Assets/Art/EditorTools/TileRenderer/Sprites/TileTemplate4_2_2a.png"));
		}

		[PreviewField(128), HideLabel, HorizontalGroup("Tiles/4_2_2b")]
		public Texture2D Tile4_2_2b;

		[OnInspectorGUI]
		[PropertyOrder(-10), HorizontalGroup("Tiles/4_2_2b")]
		private void ShowImage4_2_2b()
		{
			GUILayout.Label(
				AssetDatabase.LoadAssetAtPath<Texture2D>(
					"Assets/Art/EditorTools/TileRenderer/Sprites/TileTemplate4_2_2b.png"));
		}
		
		[PreviewField(128), HideLabel, HorizontalGroup("Tiles/3_3_2")]
		public Texture2D Tile3_3_2;
		
		[OnInspectorGUI]
		[PropertyOrder(-10), HorizontalGroup("Tiles/3_3_2")]
		private void ShowImage3_3_2()
		{
			GUILayout.Label(
				AssetDatabase.LoadAssetAtPath<Texture2D>(
					"Assets/Art/EditorTools/TileRenderer/Sprites/TileTemplate3_3_2.png"));
		}
		
		[PreviewField(128), HideLabel, HorizontalGroup("Tiles/5_1_2")]
		public Texture2D Tile5_1_2;
		
		[OnInspectorGUI]
		[PropertyOrder(-10), HorizontalGroup("Tiles/5_1_2")]
		private void ShowImage5_1_2()
		{
			GUILayout.Label(
				AssetDatabase.LoadAssetAtPath<Texture2D>(
					"Assets/Art/EditorTools/TileRenderer/Sprites/TileTemplate5_1_2.png"));
		}
		
		[PreviewField(128), HideLabel, HorizontalGroup("Tiles/2_3_3")]
		public Texture2D Tile2_3_3;

		[OnInspectorGUI]
		[PropertyOrder(-10), HorizontalGroup("Tiles/2_3_3")]
		private void ShowImage2_3_3()
		{
			GUILayout.Label(
				AssetDatabase.LoadAssetAtPath<Texture2D>(
					"Assets/Art/EditorTools/TileRenderer/Sprites/TileTemplate2_3_3.png"));
		}
		
		[PreviewField(128), HideLabel, HorizontalGroup("Tiles/3_2_3")]
		public Texture2D Tile3_2_3;

		[OnInspectorGUI]
		[PropertyOrder(-10), HorizontalGroup("Tiles/3_2_3")]
		private void ShowImage3_2_3()
		{
			GUILayout.Label(
				AssetDatabase.LoadAssetAtPath<Texture2D>(
					"Assets/Art/EditorTools/TileRenderer/Sprites/TileTemplate3_2_3.png"));
		}
		
		[PreviewField(128), HideLabel, HorizontalGroup("Tiles/4_4_0")]
		public Texture2D Tile4_4_0;
		
		[OnInspectorGUI]
		[PropertyOrder(-10), HorizontalGroup("Tiles/4_4_0")]
		private void ShowImage4_4_0()
		{
			GUILayout.Label(
				AssetDatabase.LoadAssetAtPath<Texture2D>(
					"Assets/Art/EditorTools/TileRenderer/Sprites/TileTemplate4_4_0.png"));
		}
		
		[PreviewField(128), HideLabel, HorizontalGroup("Tiles/1_3_4")]
		public Texture2D Tile1_3_4;
		
		[OnInspectorGUI]
		[PropertyOrder(-10), HorizontalGroup("Tiles/1_3_4")]
		private void ShowImage1_3_4()
		{
			GUILayout.Label(
				AssetDatabase.LoadAssetAtPath<Texture2D>(
					"Assets/Art/EditorTools/TileRenderer/Sprites/TileTemplate1_3_4.png"));
		}
	}
}