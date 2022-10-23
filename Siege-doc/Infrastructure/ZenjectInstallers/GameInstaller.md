# GameInstaller Class

Namespace: Kulinaria.Siege.Runtime.Infrastructure.ZenjectInstallers

Base class: MonoInstaller

Инсталлятор Zenject для зависимостей, необходимых для ProjectContext-а

## Properties

| Name | Type | Summary |
|---|---|---|
| **Container** | DiContainer |  |
| **IsEnabled** | bool |  |
| **useGUILayout** | bool |  |
| **enabled** | bool |  |
| **isActiveAndEnabled** | bool |  |
| **transform** | Transform |  |
| **gameObject** | GameObject |  |
| **tag** | string |  |
| **name** | string |  |
| **hideFlags** | HideFlags |  |
## Methods

| Name | Returns | Summary                                                                                                   |
|---|---|-----------------------------------------------------------------------------------------------------------|
| **Initialize()** | void | Первичная инициализация после установки зависимостей <br/> В данном случае грузится тестовая сцена боевки |
| **InstallBindings()** | void | Установка сервисов, необходимых для всех модулей игры                                                     |
