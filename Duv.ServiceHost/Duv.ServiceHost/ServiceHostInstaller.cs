using System.ComponentModel;
using System.Configuration.Install;
using System.Diagnostics;
using System.ServiceProcess;
using System.Linq;

namespace Duv.ServiceHost
{
	[RunInstaller(true)]
	public class ServiceHostInstaller : Installer
	{
		private readonly ServiceProcessInstaller process;
		private readonly ServiceInstaller service;

		public ServiceHostInstaller()
		{
			process = new ServiceProcessInstaller();
			process.Account = ServiceAccount.LocalSystem;
			process.Username = null;
			process.Password = null;

			service = new ServiceInstaller();
			service.ServiceName = ServiceHostDescriptor.Name;
			service.DisplayName = ServiceHostDescriptor.DisplayName;
			service.Description = ServiceHostDescriptor.Description;
			service.StartType = ServiceStartMode.Automatic;

			var eventLog = service.Installers.OfType<EventLogInstaller>().Single();
			eventLog.Source = ServiceHostDescriptor.Name;
			eventLog.Log = ServiceHostDescriptor.LogName;

			Installers.AddRange(new Installer[] { process, service });
		}
	}
}
