# CustomTile Class

Namespace: Kulinaria.Siege.Runtime.Gameplay.Battle.Map.Tiles

Base class: MonoBehaviour


## Properties

| Name | Type                                               | Summary |
|---|----------------------------------------------------|---|
| **NeighboursWithDistances** | IReadOnlyDictionary\<**CustomTile**, int\>         |  |
| **ActiveNeighbours** | IEnumerable\<**CustomTile**\>                      |  |
| **CellPosition** | Vector2Int                                         |  |
| **Active** | bool                                               |  |
| **Visitor** | [BaseCharacter](../../Characters/BaseCharacter.md) |  |
| **HasVisitor** | bool                                               |  |
| **Renderer** | [TileRenderer](Rendering/TileRenderer.md)          |  |
| **Item** | Vector2Int                                         |  |
| **useGUILayout** | bool                                               |  |
| **enabled** | bool                                               |  |
| **isActiveAndEnabled** | bool                                               |  |
| **transform** | Transform                                          |  |
| **gameObject** | GameObject                                         |  |
| **tag** | string                                             |  |
| **name** | string                                             |  |
| **hideFlags** | HideFlags                                          |  |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **Initialize(Vector2Int cellPos)** | void |  |
| **RegisterVisitor(BaseCharacter visitor)** | void |  |
| **UnregisterVisitor()** | void |  |
