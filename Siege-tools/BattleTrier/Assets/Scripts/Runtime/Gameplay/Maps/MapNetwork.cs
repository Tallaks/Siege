using System.Collections.Generic;
using System.Linq;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Maps.Data;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.UI;
using Sirenix.OdinInspector;
using Unity.Netcode;
using UnityEngine;
using Zenject;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Maps
{
  public class MapNetwork : NetworkBehaviour
  {
    private readonly List<Tile> _tiles = new();

    [SerializeField] [Required] [AssetSelector(Paths = "Assets/Pefabs/", Filter = "t:GameObject")]
    private Tile _tilePrefab;

    public IEnumerable<Tile> Tiles => _tiles;

    private BoardConfig _config;
    private DiContainer _container;
    private int[,] _mapBoard;

    private GameplayMediator _mediator;

    [Inject]
    private void Construct(DiContainer container, GameplayMediator mediator)
    {
      _container = container;
      _mediator = mediator;
    }

    [ServerRpc(RequireOwnership = false)]
    public void InitMapBoardServerRpc()
    {
      TileType[,] mapTiles = _config.MapTiles;
      _mapBoard = new int[mapTiles.GetLength(0), mapTiles.GetLength(1)];

      for (var col = 0; col < mapTiles.GetLength(0); col++)
      for (var row = 0; row < mapTiles.GetLength(1); row++)
        _mapBoard[col, row] = mapTiles[col, row] switch
        {
          TileType.None => -1,
          TileType.Default => 0,
          TileType.Obstacle => 1,
          TileType.WeakCover => 2,
          TileType.StrongCover => 3,
          _ => _mapBoard[col, row]
        };
    }

    [ClientRpc]
    public void SpawnTilesClientRpc(string configName)
    {
      _config = Resources.Load<BoardConfig>("Configs/Boards/" + configName);
      TileType[,] mapTiles = _config.MapTiles;
      Debug.Log("Spawn Tiles");
      InstantiateTiles(mapTiles);
      AssignTilesWithNeighbours();
    }

    public void OnTileSelected(Tile selectedTile)
    {
      if (_mediator.CharacterPlacementUiIsActive)
      {
        Refresh();
        selectedTile.ChangeToSelectedColor();
        Debug.Log($"Tile with coords {selectedTile.Coords.x}; {selectedTile.Coords.y} was clicked");
      }
    }

    public void MoveCharacterTo(Tile newTile, Character character)
    {
      Refresh();
      Tile previousTile = _tiles.FirstOrDefault(k => k.IsOccupied && k.Coords == character.Position);
      previousTile.UnOccupy();
      newTile.ChangeToSelectedColor();
    }

    private void Refresh()
    {
      foreach (Tile tile in _tiles.Where(k => !k.IsOccupied))
        tile.ChangeToUnselectedColor();
    }

    private void AssignTilesWithNeighbours()
    {
      foreach (Tile tile in _tiles)
      foreach (Tile otherTile in _tiles)
      {
        if (otherTile == tile)
          continue;
        if (otherTile.Coords.x <= tile.Coords.x + 1 &&
            otherTile.Coords.x >= tile.Coords.x - 1 &&
            otherTile.Coords.y <= tile.Coords.y + 1 &&
            otherTile.Coords.y >= tile.Coords.y - 1)
          tile.AddNeighbour(otherTile);
      }
    }

    private void InstantiateTiles(TileType[,] mapTiles)
    {
      for (var col = 0; col < mapTiles.GetLength(0); col++)
      for (var row = 0; row < mapTiles.GetLength(1); row++)
        if (mapTiles[col, row] == TileType.Default)
        {
          var tile = _container.InstantiatePrefabForComponent<Tile>(
            _tilePrefab,
            new Vector3(-4f, -1.5f, 0) + new Vector3(col, row) * 0.7f,
            Quaternion.identity,
            null);
          tile.Initialize(col, row);
          _tiles.Add(tile);
        }
    }
  }
}