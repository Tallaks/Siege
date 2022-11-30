namespace Kulinaria.Siege.Runtime.Debugging.Logging
{
	public interface ILoggerService
	{
		void Log(object message, LoggerLevel level, int priority = 10);
		void Log(object message, int priority = 10);
	}
}