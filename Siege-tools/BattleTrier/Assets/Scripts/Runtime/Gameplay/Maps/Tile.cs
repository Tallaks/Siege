using System.Collections.Generic;
using System.Linq;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Selection.Placement;
using Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Services;
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
    public bool Occupied => CharacterOnTile != null;
    private Character CharacterOnTile { get; set; }
    private MapNetwork _mapNetwork;
    private IPlacementSelection _placementSelection;

    public void Initialize(int col, int row, MapNetwork mapNetwork)
    {
      _mapNetwork = mapNetwork;
      _placementSelection = ServiceProvider.GetResolve<IPlacementSelection>();
      Coords = new Vector2Int(col, row);
    }

    private void OnMouseDown()
    {
      if (Occupied == false)
      {
        if (_placementSelection.SelectedPlayerConfig != null)
          _mapNetwork.PlacePlayerFromConfigOn(this, _placementSelection.SelectedPlayerConfig);
        else if (_placementSelection.SelectedCharacter != null)
          _mapNetwork.MoveCharacterTo(this, _placementSelection.SelectedCharacter);
        else
          _mapNetwork.OnTileSelected(this);
      }
      else
      {
        _placementSelection.SelectPlacedCharacter(CharacterOnTile);
        _mapNetwork.OnTileSelected(this);
      }
    }

    public void AddNeighbour(Tile otherTile) =>
      _neighbours.Add(otherTile);

    public void OccupyBy(Character character)
    {
      CharacterOnTile = character;
      ChangeColor(new Color(1, 1, 1, 0.5f));
    }

    public void UnOccupy()
    {
      CharacterOnTile = null;
      ChangeColor(Color.white);
    }

    public void ChangeToSelectedColor()
    {
      if (Occupied == false)
        ChangeColor(_selectedColor);
      foreach (Tile neighbour in _neighbours.Where(k => !k.Occupied))
        neighbour.ChangeColor(_neighbourColor);
    }

    public void ChangeToUnselectedColor() =>
      ChangeColor(Color.white);

    private void ChangeColor(Color color) =>
      GetComponent<SpriteRenderer>().color = color;
  }
}