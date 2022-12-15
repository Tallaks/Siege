# ArrayGridMap Class

Namespace: Kulinaria.Siege.Runtime.Gameplay.Battle.Prototype

## Properties


| Name           | Type                                                    | Summary |
| -------------- | ------------------------------------------------------- | ------- |
| **AllTiles**   | IEnumerable\<[CustomTile](../Map/Tiles/CustomTile.md)\> |         |
| **EmptyTiles** | IEnumerable\<[CustomTile](../Map/Tiles/CustomTile.md)\> |         |

## Constructors


| Name                                            | Summary |
| ----------------------------------------------- | ------- |
| **ArrayGridMap(TilemapFactory tilemapFactory)** |         |

## Methods


| Name                      | Returns                                  | Summary |
| ------------------------- | ---------------------------------------- | ------- |
| **Clear()**               | void                                     |         |
| **GenerateMap()**         | void                                     |         |
| **GetTile(int x, int y)** | [CustomTile](../Map/Tiles/CustomTile.md) |         |

## Fields


| Name          | Type   | Summary |
| ------------- | ------ | ------- |
| **GridArray** | int[,] |         |
