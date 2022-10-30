# InputService Class

Namespace: Kulinaria.Siege.Runtime.Infrastructure.Inputs

Base class: MonoBehaviour

Инпут-сервис, регистрирующий инпут-сигналы от клавиатуры и мыши

При активации сцены запускает новую инпут-систему Unity, через апдейт региструрует события инпута
## Properties

| Name | Type | Summary                                                                       |
|---|---|-------------------------------------------------------------------------------|
| **OnClick**  | Action\<Vector2\> | Событие нажатия кнопки мыши<br/>Передает позицию мыши в экранных координатах                                                                        |
| **OnMove**   | Action\<Vector2\> | Событие перемещения камеры в сторону<br/>Передает нормализованный вектор с расчетом того, куда перемещается камера |
| **OnRotate** | Action\<Vector2\> | Событие вращения камеры<br/>Передает вектор с расчетом перемещения мыши в экранных координатах                                      |
| **OnZoom**   | Action\<Vector2\> | Событие приближения камеры<br/>           Передает вектор с расчетом вращения колесика мыши                                                     |
| **useGUILayout** | bool |                                                                               |
| **enabled** | bool |                                                                               |
| **isActiveAndEnabled** | bool |                                                                               |
| **transform** | Transform |                                                                               |
| **gameObject** | GameObject |                                                                               |
| **tag** | string |                                                                               |
| **name** | string |                                                                               |
| **hideFlags** | HideFlags |                                                                               |