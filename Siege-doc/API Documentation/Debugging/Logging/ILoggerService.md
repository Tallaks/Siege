# ILoggerService Interface

Namespace: Kulinaria.Siege.Runtime.Debugging.Logging

Сервис, предназначенный для записи игровых логов.
## Methods

| Name                                                     | Returns | Summary                                                            |
|----------------------------------------------------------|---------|--------------------------------------------------------------------|
| **Log(Object message, int priority)**                    | void    | Логгирование сообщения с заданным приоритетом                      |
| **Log(Object message, LoggerLevel level, int priority)** | void    | Логгирование сообщения определенного уровня с заданным приоритетом |
