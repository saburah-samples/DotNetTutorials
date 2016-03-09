using System;
using System.ServiceProcess;

namespace Duv.ServiceHost
{
	public class ServiceHost : ServiceBase
	{
		public ServiceHost()
		{
			ServiceName = ServiceHostDescriptor.Name;
			AutoLog = true;
		}

		protected override void OnStart(string[] args)
		{
			var message = string.Format("service '{0}' is started", ServiceName);
			EventLog.WriteEntry(message);
			Console.WriteLine(message);
		}

		protected override void OnStop()
		{
			var message = string.Format("service '{0}' is stoped", ServiceName);
			EventLog.WriteEntry(message);
			Console.WriteLine(message);
		}

		internal void RunInDebugMode(string[] args)
		{
			OnStart(args);
			Console.ReadLine();
			OnStop();
		}
	}
}
