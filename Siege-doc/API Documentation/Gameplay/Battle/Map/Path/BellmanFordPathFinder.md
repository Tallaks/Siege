# BellmanFordPathFinder Class

Namespace: Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Path


## Constructors

| Name | Summary |
|---|---|
| **BellmanFordPathFinder(IGridMap map)** |  |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **Distance(CustomTile tileB)** | int |  |
| **FindDistancesToAllTilesFrom(CustomTile startTile)** | void |  |
| **GetAvailableTilesByDistance(int distance)** | IEnumerable\<[CustomTile](../Tiles/CustomTile.md)\> |  |
| **GetShortestPath(CustomTile tile)** | LinkedList\<[CustomTile](../Tiles/CustomTile.md)\> |  |
