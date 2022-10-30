# SceneLoader Class

Namespace: Kulinaria.Siege.Runtime.Infrastructure.Scenes

Загрузчик сцен через SceneManager от Unity
## Constructors

| Name | Summary                                                                                                        |
|---|----------------------------------------------------------------------------------------------------------------|
| **SceneLoader(ICoroutineRunner runner)** | Так как для асинхронной загрузки сцены используются корутины, данный сервис должен использовать сервис корутин |
## Methods

| Name | Returns | Summary                                                                                  |
|---|---|------------------------------------------------------------------------------------------|
| **LoadSceneAsync(string sceneName, Action onLoad)** | void | Асинхронно загрузить сцену с именем sceneName и выполнить действие onLoad после загрузки |
