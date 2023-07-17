using System.Collections.Generic;
using System.Linq;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Placer;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Selection.Placement;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.UI;
using UnityEngine;
using Zenject;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Maps
{
  [RequireComponent(typeof(SpriteRenderer))]
  public class Tile : MonoBehaviour
  {
    private readonly List<Tile> _neighbours = new();
    [SerializeField] private Color _selectedColor;
    [SerializeField] private Color _neighbourColor;
    public Vector2Int Coords { get; private set; }
    public bool IsOccupied => CharacterOnTile != null;
    private Character CharacterOnTile { get; set; }
    private ICharacterPlacer _characterPlacer;
    private MapNetwork _mapNetwork;
    private GameplayMediator _mediator;
    private IPlacementSelection _placementSelection;

    [Inject]
    private void Construct(
      GameplayMediator mediator,
      IPlacementSelection placementSelection,
      ICharacterPlacer characterPlacer,
      MapNetwork mapNetwork)
    {
      _mediator = mediator;
      _placementSelection = placementSelection;
      _characterPlacer = characterPlacer;
      _mapNetwork = mapNetwork;
    }

    public void Initialize(int col, int row) =>
      Coords = new Vector2Int(col, row);

    private void OnMouseDown()
    {
      if (IsOccupied == false)
      {
        if (_placementSelection.SelectedPlayerConfig != null)
        {
          _characterPlacer.PlaceNewCharacterOnTile(this, _placementSelection.SelectedPlayerConfig);
        }
        else if (_placementSelection.SelectedCharacter != null)
        {
          _mapNetwork.MoveCharacterTo(this, _placementSelection.SelectedCharacter);
          _characterPlacer.PlaceExistingCharacterOnTile(this, _placementSelection.SelectedCharacter);
        }
        else
        {
          _mapNetwork.OnTileSelected(this);
        }
      }
      else
      {
        if (_mediator.CharacterPlacementUiIsActive)
          _placementSelection.SelectCharacter(CharacterOnTile);
        _mapNetwork.OnTileSelected(this);
      }
    }

    public void AddNeighbour(Tile otherTile) =>
      _neighbours.Add(otherTile);

    public void OccupyBy(Character character)
    {
      Debug.Log($"Tile {Coords} occupied by {character.name}");
      CharacterOnTile = character;
      if (character.GetComponent<Enemy>())
        ChangeColor(new Color(1, 0, 0, 0.2f));
      else
        ChangeColor(new Color(0, 1, 0, 0.2f));
    }

    public void UnOccupy()
    {
      CharacterOnTile = null;
      ChangeColor(Color.white);
    }

    public void ChangeToSelectedColor()
    {
      if (IsOccupied == false)
        ChangeColor(_selectedColor);
      foreach (Tile neighbour in _neighbours.Where(k => !k.IsOccupied))
        neighbour.ChangeColor(_neighbourColor);
    }

    public void ChangeToUnselectedColor() =>
      ChangeColor(Color.white);

    private void ChangeColor(Color color) =>
      GetComponent<SpriteRenderer>().color = color;
  }
}