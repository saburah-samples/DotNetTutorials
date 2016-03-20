using System;
using System.ServiceProcess;

namespace Duv.ServiceHost
{
	public class ServiceHost : ServiceBase
	{
		public ServiceHost()
		{
			AutoLog = false;
			ServiceName = ServiceHostDescriptor.Name;

			EventLog = new System.Diagnostics.EventLog();
			EventLog.Source = ServiceHostDescriptor.Name;
			EventLog.Log = ServiceHostDescriptor.LogName;
			if (!System.Diagnostics.EventLog.SourceExists(ServiceHostDescriptor.Name))
			{
				System.Diagnostics.EventLog.CreateEventSource(ServiceHostDescriptor.Name, ServiceHostDescriptor.LogName);
			}
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
