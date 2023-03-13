using System;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Runtime.Data
{
  [CreateAssetMenu(menuName = "Kulinaria/Board")]
  [Serializable]
  public class BoardData : SerializedScriptableObject
  {
    private int _previousCols;
    private int _previousRows;

    [ShowInInspector] public int Cols;
    [ShowInInspector] public int Rows;

    public string Name;
    public Sprite Icon;

    private void OnValidate()
    {
      if(_previousCols == Cols && _previousRows == Rows)
        return;
      MapTiles = new TileType[Cols, Rows];
      _previousCols = Cols;
      _previousRows = Rows;
    }

    [Space]
    [TableMatrix(RowHeight = 50, ResizableColumns = false, DrawElementMethod = nameof(DrawColoredEnumElement))]
    public TileType[,] MapTiles = new TileType[10, 15];

    [Button]
    public void SetAllTilesDefault()
    {
      for (int i = 0; i < MapTiles.GetLength(0); i++)
      for (int j = 0; j < MapTiles.GetLength(1); j++)
        MapTiles[i, j] = TileType.Default;
    }
    
#if UNITY_EDITOR

    private static TileType DrawColoredEnumElement(Rect rect, TileType value)
    {
      if (Event.current.type == EventType.MouseDown && rect.Contains(Event.current.mousePosition))
      {
        if (value == TileType.Obstacle)
          value = TileType.None;
        else
          value = (TileType)((int)value + 1);
        GUI.changed = true;
        Event.current.Use();
      }

      switch (value)
      {
        case TileType.None:
          UnityEditor.EditorGUI.DrawRect(rect.Padding(1), Color.black);
          break;
        case TileType.Default:
          UnityEditor.EditorGUI.DrawRect(rect.Padding(1), new Color(1, 1, 1, 0.5f));
          break;
        case TileType.WeakCover:
          UnityEditor.EditorGUI.DrawRect(rect.Padding(1), new Color(1, 0.4f, 0.1f, 0.7f));
          break;
        case TileType.StrongCover:
          UnityEditor.EditorGUI.DrawRect(rect.Padding(1), new Color(1, 0, 1, 0.5f));
          break;
        case TileType.Obstacle:
          UnityEditor.EditorGUI.DrawRect(rect.Padding(1), new Color(0, 0, 0, 0.25f));
          break;
      }

      return value;
    }

#endif
  }

  public enum TileType
  {
    None = 0,
    Default = 1,
    WeakCover = 2,
    StrongCover = 3,
    Obstacle = 4
  }
}