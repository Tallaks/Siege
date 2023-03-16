using System;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Runtime.Data
{
  [CreateAssetMenu(menuName = "Kulinaria/Board")]
  [Serializable]
  public class BoardConfig : SerializedScriptableObject
  {
    private int _cols;
    private int _rows;

    [Button]
    public void SetMap(int cols, int rows)
    {
      Cols = cols;
      Rows = rows;
      OnValidate();
    }

    public int Cols
    {
      get
      {
        if (_cols == default)
          _cols = PlayerPrefs.GetInt("Cols" + name, -1);
        return _cols;
      }
      set
      {
        _cols = value;
        PlayerPrefs.SetInt("Cols" + name, value);
        PlayerPrefs.Save();
      }
    }

    public int Rows
    {
      get
      {
        if (_rows == default)
          _rows = PlayerPrefs.GetInt("Rows" + name, -1);
        return _rows;
      }
      set
      {
        _rows = value;
        PlayerPrefs.SetInt("Rows" + name, value);
        PlayerPrefs.Save();
      }
    }

    public string Name;
    public Sprite Icon;

    public BoardConfig() =>
      MapTiles = new TileType[1, 1];

    private void OnValidate()
    {
      int cols = PlayerPrefs.GetInt("Cols" + name, -1);
      int rows = PlayerPrefs.GetInt("Rows" + name, -1);

      if (cols == Cols && rows == Rows && MapTiles.GetLength(1) == rows && MapTiles.GetLength(0) == cols)
        return;

      if (cols == -1 && rows == -1)
        return;
      MapTiles = new TileType[Cols, Rows];
    }

    [Space] [TableMatrix(RowHeight = 50, ResizableColumns = false, DrawElementMethod = "DrawColoredEnumElement")]
    public TileType[,] MapTiles;

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