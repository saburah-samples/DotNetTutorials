using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

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

			Installers.AddRange(new Installer[] { process, service });
		}
	}
}
