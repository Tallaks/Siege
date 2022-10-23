# ISceneLoader Interface

Namespace: Kulinaria.Siege.Runtime.Infrastructure.Scenes

Интерфейс сервиса загрузчика сцен Unity
## Methods

| Name | Returns | Summary                                                                                      |
|---|---|----------------------------------------------------------------------------------------------|
| **LoadSceneAsync(string sceneName, Action onLoad)** | void | Асинхронно загрузить сцену используя ее название, и выполнить действие onLoad после загрузки |
