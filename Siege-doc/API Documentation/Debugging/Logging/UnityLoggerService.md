# UnityLoggerService Class

Namespace: Kulinaria.Siege.Runtime.Debugging.Logging
Implements: [ILoggerService](ILoggerService.md)

Сервис логгирования, работающий при помощи встроенного логгера Unity. Расширяет его при помощи возможности фильтрации сообщений по важности и по уровню.
## Methods

| Name                                                                            | Returns | Summary                                                            |
|---------------------------------------------------------------------------------|---------|--------------------------------------------------------------------|
| **Log(Object message, int priority = 10)**                                      | void    | Логгирование сообщения с заданным приоритетом                      |
| **Log(Object message, [LoggerLevel](LoggerLevel.md) level, int priority = 10)** | void    | Логгирование сообщения определенного уровня с заданным приоритетом |
## Fields

| Name                | Type | Summary                                                         |
|---------------------|------|-----------------------------------------------------------------|
| **DefaultPriority** | int  | Минимальный уровень логгирования, при котором отображаются логи |