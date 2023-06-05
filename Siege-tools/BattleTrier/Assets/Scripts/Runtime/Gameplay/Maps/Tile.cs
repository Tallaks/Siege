using System.Collections.Generic;
using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Maps
{
  [RequireComponent(typeof(SpriteRenderer))]
  public class Tile : MonoBehaviour
  {
    private readonly List<Tile> _neighbours = new();
    [SerializeField] private Color _selectedColor;
    [SerializeField] private Color _neighbourColor;
    public Vector2Int Coords { get; private set; }
    private MapNetwork _mapNetwork;

    public void Initialize(int col, int row, MapNetwork mapNetwork)
    {
      _mapNetwork = mapNetwork;
      Coords = new Vector2Int(col, row);
    }

    private void OnMouseDown() =>
      _mapNetwork.OnTileSelected(this);

    public void AddNeighbour(Tile otherTile) =>
      _neighbours.Add(otherTile);

    public void ChangeToSelectedColor()
    {
      ChangeColor(_selectedColor);
      foreach (Tile neighbour in _neighbours)
        neighbour.ChangeColor(_neighbourColor);
    }

    public void ChangeToUnselectedColor() =>
      ChangeColor(Color.white);

    private void ChangeColor(Color color) =>
      GetComponent<SpriteRenderer>().color = color;
  }
}