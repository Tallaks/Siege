# CoroutineRunner Class

Namespace: Kulinaria.Siege.Runtime.Infrastructure.Coroutines

Base class: MonoBehaviour

Класс для использования корутин в не-MonoBehaviour

Работает как заранеее созданный <i>GameObject</i>, который не удаляется при переключении сцен. Корутины запускаются через
/// интерфейс <i>MonoBehaviour</i>, через него же и останавливаются

## Example

Есть сервис загрузки сцен <b>SceneLoader</b>. Он не наследует <i>MonoBehaviour</i>, однако через него
/// осуществляется асинхронная загрузка сцен, реализованная через корутины. Чтобы этот сервис заработал, корутина
/// загрузки или выгрузки сцены подцепляеется к <i>MonoBehaviour</i> данного класса <b>CoroutineRunner</b>

## Properties

| Name | Type | Summary |
|---|---|---|
| **useGUILayout** | bool |  |
| **enabled** | bool |  |
| **isActiveAndEnabled** | bool |  |
| **transform** | Transform |  |
| **gameObject** | GameObject |  |
| **tag** | string |  |
| **name** | string |  |
| **hideFlags** | HideFlags |  |