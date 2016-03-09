using System;
using System.Collections.Generic;
using System.Configuration.Install;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Duv.ServiceHost
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		static void Main(string[] args)
		{
			try
			{
				if (args.Length == 0)
				{
					var service = new ServiceHost();
					if (Environment.UserInteractive)
					{
						service.RunInDebugMode(args);
					}
					else
					{
						ServiceBase.Run(service);
					}
				}
				else
				{
					switch (args[0].ToLower())
					{
						case "-install":
							InstallServiceHost();
							StartServiceHost();
							break;
						case "-uninstall":
							StopServiceHost();
							UninstallServiceHost();
							break;
						case "-start":
							StartServiceHost();
							break;
						case "-stop":
							StopServiceHost();
							break;
						default:
							throw new NotSupportedException("Unknown command-line argument: " + args[0]);
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				Console.ReadKey();
			}
		}

		private static void InstallServiceHost()
		{
			var controller = GetServiceController();
			if (controller != null)
			{
				Console.WriteLine("Service is already installed.");
				return;
			}

			ManagedInstallerClass.InstallHelper(new string[] { Assembly.GetExecutingAssembly().Location });
		}

		private static void UninstallServiceHost()
		{
			var controller = GetServiceController();
			if (controller == null)
			{
				Console.WriteLine("Service is not installed.");
				return;
			}

			ManagedInstallerClass.InstallHelper(new string[] { "/u", Assembly.GetExecutingAssembly().Location });
		}

		private static void StartServiceHost()
		{
			var service = ServiceController.GetServices().FirstOrDefault(s => s.ServiceName == ServiceHostDescriptor.Name);
			if (service == null)
			{
				Console.WriteLine("Service is not installed.");
				return;
			}

			try
			{
				service.Start();
				service.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromSeconds(ServiceHostDescriptor.StartupTimeout));
				Console.WriteLine("Service has been started successfully.");
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		private static void StopServiceHost()
		{
			ServiceController service = GetServiceController();
			if (service == null)
			{
				Console.WriteLine("Service is not installed.");
				return;
			}

			try
			{
				service.Stop();
				service.WaitForStatus(ServiceControllerStatus.Stopped, TimeSpan.FromSeconds(ServiceHostDescriptor.StartupTimeout));
				Console.WriteLine("Service has been stopped successfully.");
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		private static ServiceController GetServiceController()
		{
			return ServiceController.GetServices().FirstOrDefault(s => s.ServiceName == ServiceHostDescriptor.Name);
		}
	}
}
