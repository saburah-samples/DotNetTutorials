using System;
using System.Collections.Generic;
using System.Deployment.Application;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            if (ApplicationDeployment.IsNetworkDeployed)
            {
                // если приложение было установлено
                Console.WriteLine("ClickOnce Enabled");
                var deployment = ApplicationDeployment.CurrentDeployment;
                Console.WriteLine("Deployment version: {0}", deployment.CurrentVersion);
            }
            else
            {
                // если приложение не было установлено
                Console.WriteLine("ClickOnce Disabled");
            }

            Console.WriteLine("BaseDirectory={0}", AppDomain.CurrentDomain.BaseDirectory);
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }
}
