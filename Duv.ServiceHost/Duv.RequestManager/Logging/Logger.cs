using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Duv.RequestManager.Logging
{
	class Logger : ILogger
	{
		private readonly TraceSource logger;

		public Logger(string name)
		{
			logger = new TraceSource(name, SourceLevels.Error);
		}

		public void LogError(string message)
		{
			logger.TraceEvent(TraceEventType.Error, 0, message);
		}
	}
}
