# GridExtensions Class

Namespace: Kulinaria.Siege.Runtime.Extensions

Методы расширения для работы с сеткой.

## Methods

| Name                                                          | Returns                   | Summary                                                                                             |
|---------------------------------------------------------------|---------------------------|-----------------------------------------------------------------------------------------------------|
| **IsDiagonalPositionTo(Vector2Int point, Vector2Int target)** | bool?                     | Проверка, является ли позиция диагональной данной. Возвращает null, если тайлы не являются соседями |
| **IsDiagonalPositionTo(CustomTile a, CustomTile b)**          | bool?                     | Проверка, является ли тайл диагональной данному. Возвращает null, если тайлы не являются соседями   |
| **MissingNeighboursPositions(CustomTile tile)**               | IEnumerable\<Vector2Int\> | Возвращает коллекцию позиций, по которым отсутствуют соседи у выбранного тайла                      |
| **ToCell(Vector3 worldPosition)**                             | Vector2Int                | Возвращает значение мировых координат в виде координат на сетке                                     |
| **ToWorld(Vector2Int cellPosition)**                          | Vector3                   | Возвращает значение координат на сетке в виде мировых координат                                     |
