using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Duv.RequestManager.Logging
{
	public static class LoggerFactory
	{
		public static ILogger Create(object source)
		{
			var type = source.GetType();
			return new Logger(type.Name);
		}
	}
}
