using System.Collections.Generic;
using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Maps
{
  [RequireComponent(typeof(SpriteRenderer))]
  public class Tile : MonoBehaviour
  {
    [SerializeField] private Color _selectedColor;
    [SerializeField] private Color _neighbourColor;
    private readonly List<Tile> _neighbours = new();
    private MapNetwork _mapNetwork;
    public Vector2Int Coords { get; private set; }

    private void OnMouseDown()
    {
      _mapNetwork.Refresh();
      ChangeColor(_selectedColor);
      foreach (Tile neighbour in _neighbours)
        neighbour.ChangeColor(_neighbourColor);
      Debug.Log($"Tile with coords {Coords.x}; {Coords.y} was clicked");
    }

    public void Initialize(int col, int row, MapNetwork mapNetwork)
    {
      _mapNetwork = mapNetwork;
      Coords = new Vector2Int(col, row);
    }

    public void AddNeighbour(Tile otherTile) =>
      _neighbours.Add(otherTile);

    public void ChangeColor(Color color) =>
      GetComponent<SpriteRenderer>().color = color;
  }
}