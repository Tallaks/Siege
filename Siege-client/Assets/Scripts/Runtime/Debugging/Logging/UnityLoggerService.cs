using UnityEngine;

namespace Kulinaria.Siege.Runtime.Debugging.Logging
{
	public class UnityLoggerService : ILoggerService
	{
		public static int DefaultPriority;
		
		public void Log(object message, LoggerLevel level, int priority = 10)
		{
			if(priority >= DefaultPriority)
				Debug.Log($"{LoggerPart(level)}: {message}");
		}

		public void Log(object message, int priority = 10)
		{
			if(priority >= DefaultPriority)
				Log(message, LoggerLevel.None, priority);
		}

		private string LoggerPart(LoggerLevel level)
		{
			switch (level)
			{
				case LoggerLevel.Application:
					return "<color=cyan>Application</color>";
				case LoggerLevel.Battle:
					return "<color=green>Battle</color>";
				case LoggerLevel.None:
					return "Other";
			}

			return string.Empty;
		}
	}
}