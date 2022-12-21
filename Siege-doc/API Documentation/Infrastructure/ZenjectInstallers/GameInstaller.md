# GameInstaller Class

Namespace: Kulinaria.Siege.Runtime.Infrastructure.ZenjectInstallers

Base class: MonoInstaller

Инсталлятор Zenject для зависимостей, необходимых для всего приложения:

- [Логгер](../../Debugging/Logging/ILoggerService.md)
- [Обработчик корутин вне монобехов](../Coroutines/ICoroutineRunner.md)
- [Доставщик ассетов](../Assets/IAssetsProvider.md)
- [Загрузчик сцен](../Scenes/ISceneLoader.md)
- [Инпут-сервис](../Inputs/IInputService.md)

## Properties

| Name                   | Type        | Summary |
|------------------------|-------------|---------|
| **Container**          | DiContainer |         |
| **IsEnabled**          | bool        |         |
| **useGUILayout**       | bool        |         |
| **enabled**            | bool        |         |
| **isActiveAndEnabled** | bool        |         |
| **transform**          | Transform   |         |
| **gameObject**         | GameObject  |         |
| **tag**                | string      |         |
| **name**               | string      |         |
| **hideFlags**          | HideFlags   |         |

## Methods

| Name                  | Returns | Summary                                                                                                                                                    |
|-----------------------|---------|------------------------------------------------------------------------------------------------------------------------------------------------------------|
| **Initialize()**      | void    | Первичная инициализация после установки зависимостей<br/> В данном случае:<br/> грузится тестовая сцена боевки<br/> Устанавливается приоритет логгирования |
| **InstallBindings()** | void    | Установка сервисов, необходимых для всех модулей игры                                                                                                      |

## Fields

| Name        | Type | Summary                                             |
|-------------|------|-----------------------------------------------------|
| **Testing** | bool | Грузится ли данный инсталлер в интеграционные тесты |
